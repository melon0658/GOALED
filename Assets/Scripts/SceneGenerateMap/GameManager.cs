using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameObject carPrefab;
  [SerializeField] private GameObject tileManager;
  [SerializeField] private Manager manager;
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  private Dictionary<string, GameObject> cars = new Dictionary<string, GameObject>();
  async void Start()
  {
    var res = await matchingServer.client.GetRoomDetailAsync(new MatchingService.GetRoomDetailRequest
    {
      RoomId = playerInfo.player.RoomId,
      Password = playerInfo.password
    });
    if (res.Room == null)
    {
      Debug.LogError("Room not found");
      return;
    }

    var room = res.Room;
    if (room.Owner == playerInfo.player.Id)
    {
      playerInfo.isRoomOwner = true;
      var players = room.Players;
      foreach (var player in players)
      {
        var car = Instantiate(carPrefab, new Vector3(10 * cars.Count, 0, 0), Quaternion.identity);
        cars.Add(player.Id, car);
        cars[player.Id].GetComponent<Car>().TileManager = tileManager;
      }
      InitPlayerData(room.Players);
    }
    tileManager.GetComponent<GenerateMap>().GenerateTiles();
  }

  private void InitPlayerData(Google.Protobuf.Collections.RepeatedField<MatchingService.Player> players)
  {
    foreach (var player in players)
    {
      var stetus = new PlayerStetus(player.Name, 1000, 0, "red", "Job", false, 0, 0, false);
      var slstetus = stetus.Serialize();
      slstetus.Id = player.Id;
      manager.AddPlayerData(slstetus);
    }
    var roomStetus = new GameService.PlayerData();
    roomStetus.Id = playerInfo.player.RoomId;
    roomStetus.Key.Add("turn");
    roomStetus.Value.Add(players[0].Id);
    manager.AddPlayerData(roomStetus);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void GameUpdate(Dictionary<string, Dictionary<string, string>> datas)
  {
    if (playerInfo.isRoomOwner)
    {
      foreach (var data in datas)
      {
        Debug.Log(data.Key);
        foreach (var d in data.Value)
        {
          Debug.Log(d.Key + " : " + d.Value);
        }
        if (data.Key == playerInfo.player.RoomId)
        {
          var turn = data.Value["turn"];
          var car = cars[turn];
          car.GetComponent<SendObject>().setRPC("ActiveCamera", new Dictionary<string, string>());
        }
      }
    }
  }
}
