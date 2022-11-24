using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoSingleton<GameManager>
{
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private GameObject carPrefub;
  [SerializeField] private RPCManager rpcManager;
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
  }

  public void onClick()
  {
    rpcManager.GetComponent<SendObject>().setRPC("MoveCamera", new Dictionary<string, string>() { { "id", playerInfo.player.Id } });
  }

  public void onClickMove()
  {
    rpcManager.GetComponent<SendObject>().setRPC("MoveCar", new Dictionary<string, string>() { { "id", playerInfo.player.Id }, { "step", "1" } });
  }

  public void MoveCamera(string id)
  {
    if (!playerInfo.isRoomOwner)
    {
      return;
    }
    var car = cars[id];
    var camera = GameObject.Find("Camera");
    var backword = car.transform.TransformDirection(Vector3.back);
    camera.transform.position = car.transform.position + backword * 120 + new Vector3(0, 40, 0);
    camera.transform.LookAt(car.transform.position);
    camera.transform.parent = car.transform;
  }

  public void MoveCar(string id, string step)
  {
    if (!playerInfo.isRoomOwner)
    {
      return;
    }
    var car = cars[id];
    var carMove = car.GetComponent<CarMove>();
    var (tile, path, rotate) = carMove.calcPath(MapManager.instance.GetTile(players[id].NowPosIndex.ToString()), int.Parse(step), Direction.NEGATIVE_X);
    carMove.Move(path, rotate);
    players[id].NowPosIndex = int.Parse(tile.id);
  }

  async void Start()
  {
    players = await GetPlayers();
    if (playerInfo.isRoomOwner)
    {
      Debug.Log("owner");
      InitializeCar();
    }
    MapManager.instance.GenerateTiles();
  }
}
