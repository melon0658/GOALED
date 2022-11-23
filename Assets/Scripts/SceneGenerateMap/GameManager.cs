using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private GameObject carPrefub;
  private Dictionary<string, PlayerStetus> players = new Dictionary<string, PlayerStetus>();
  private Dictionary<string, GameObject> cars = new Dictionary<string, GameObject>();

  private async Task<bool> IsRoomOwner()
  {
    var res = await matchingServer.client.GetRoomDetailAsync(new MatchingService.GetRoomDetailRequest() { RoomId = playerInfo.player.RoomId });
    return res.Room.Owner == playerInfo.player.Id;
  }

  private async Task<Dictionary<string, PlayerStetus>> GetPlayers()
  {
    var res = await matchingServer.client.GetRoomDetailAsync(new MatchingService.GetRoomDetailRequest() { RoomId = playerInfo.player.RoomId });
    var players = new Dictionary<string, PlayerStetus>();
    foreach (var player in res.Room.Players)
    {
      players.Add(player.Id, new PlayerStetus(player.Name, 10000, 0, "red", "job", false, 0, 0, false));
    }
    return players;
  }

  private void InitializeCar()
  {
    var position = new Vector3(0, 0, 0);
    foreach (var player in players)
    {
      var car = Instantiate(carPrefub, position, Quaternion.identity);
      position += new Vector3(20, 0, 0);
      cars.Add(player.Key, car);
    }
    // for (int i = 0; i < 4; i++)
    // {
    //   var car = Instantiate(carPrefub, position, Quaternion.identity);
    //   position += new Vector3(20, 0, 0);
    //   cars.Add(i.ToString(), car);
    // }
  }


  async void Start()
  {
    players = await GetPlayers();
    if (playerInfo.isRoomOwner)
    {
      Debug.Log("owner");
      InitializeCar();
      gameObject.GetComponent<SendObject>().setRPC("ActiveCamera", new Dictionary<string, string>() { { "id", playerInfo.player.Id } });
    }
    MapManager.instance.GenerateTiles();
  }

  [CustomRPC]
  public void ActiveCamera(string id)
  {
    Debug.Log(id);
    cars[id].GetComponentInChildren<Camera>().enabled = true;
  }
}
