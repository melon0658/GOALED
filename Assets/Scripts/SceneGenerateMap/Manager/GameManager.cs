using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System;
using UniRx;
using UnityEngine.UI;


public class GameManager : MonoSingleton<GameManager>
{
  [SerializeField] public PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private GameObject carPrefub;
  [SerializeField] private RPCManager rpcManager;
  [SerializeField] private UIManager uiManager;
  private Dictionary<string, PlayerStetus> players = new Dictionary<string, PlayerStetus>();
  public Dictionary<string, PlayerStetus> Players = new Dictionary<string, PlayerStetus>();
  private List<string> playerIds = new List<string>();
  public Dictionary<string, GameObject> cars = new Dictionary<string, GameObject>();
  private Subject<Dictionary<string, PlayerStetus>> _onUpdatePlayerStetus = new Subject<Dictionary<string, PlayerStetus>>();
  public IObservable<Dictionary<string, PlayerStetus>> OnUpdatePlayerStetus
  {
    get { return _onUpdatePlayerStetus; }
  }

  async void Start()
  {
    players = await GetPlayers();
    if (playerInfo.isRoomOwner)
    {
      Debug.Log("owner");
      InitializeCar();
      rpcManager.GetComponent<SendObject>().setRPC("ActiveTurn", new Dictionary<string, string>() { { "id", playerInfo.player.Id } });
    }
    else
    {
      await Task.Delay(5000);
      rpcManager.GetComponent<SendObject>().setRPC("Refresh", new Dictionary<string, string>());
    }
    // OnUpdatePlayerStetus.Subscribe(x => ).AddTo(this);

    MapManager.instance.GenerateTiles();
  }

  public string getMyUserId()
  {
    if (!cars.ContainsKey(playerInfo.player.Id))
    {
      var car = GameObject.FindObjectsOfType<SyncObject>().Where(x => x.GetComponent<SyncObject>().objectId == SyncManager.instance.PlayerData[playerInfo.player.Id]["carID"]).FirstOrDefault();
      if (car != null)
      {
        cars.Add(playerInfo.player.Id, car.gameObject);
      }
      else
      {
        Debug.Log("car is null");
      }
    }
    return playerInfo.player.Id;
  }

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
    playerIds = new List<string>(players.Keys);
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
      GameService.PlayerData playerData = new GameService.PlayerData();
      playerData.Id = player.Key;
      playerData.Key.Add("carID");
      car.GetComponent<SendObject>().setPreObjectID(player.Key);
      playerData.Value.Add(car.GetComponent<SendObject>().ObjectId);
      SyncManager.instance.AddPlayerData(playerData);
    }
  }

  private string GetNextPlayerId(string id)
  {
    var index = playerIds.IndexOf(id);
    if (index == playerIds.Count - 1)
    {
      return playerIds[0];
    }
    else
    {
      return playerIds[index + 1];
    }
  }

  public void onClickMove(int step, Direction direction)
  {
    rpcManager.GetComponent<SendObject>().setRPC("ResolveTurn", new Dictionary<string, string>() { { "id", playerInfo.player.Id }, { "step", step.ToString() }, { "direction", ((int)direction).ToString() } });
  }

  public async void MoveCamera(string id)
  {
    if (!playerInfo.isRoomOwner)
    {
      return;
    }
    var car = cars[id];
    var camera = GameObject.Find("Camera");
    camera.transform.parent = car.transform;
    var backword = car.transform.TransformDirection(Vector3.back);
    var position = car.transform.position + (backword * 120) + new Vector3(0, 30, 0);
    var rotation = Quaternion.LookRotation(car.transform.position - position);
    await camera.GetComponent<CameraManager>().MoveTo(position, rotation.eulerAngles);
  }

  public async void ResolveTurn(string id, string step, Direction direction)
  {
    if (!playerInfo.isRoomOwner)
    {
      return;
    }
    var car = cars[id];
    var carMove = car.GetComponent<CarMove>();
    var (tile, path, rotate) = carMove.calcPath(MapManager.instance.GetTile(players[id].NowPosIndex.ToString()), int.Parse(step), direction);
    await carMove.Move(path, rotate);
    players[id].NowPosIndex = int.Parse(tile.id);
    SyncManager.instance.AddPlayerData(new GameService.PlayerData() { Id = id, Key = { "nowPosIndex" }, Value = { tile.id } });
    var nextPlayerID = GetNextPlayerId(id);
    rpcManager.GetComponent<SendObject>().setRPC("ActiveTurn", new Dictionary<string, string>() { { "id", nextPlayerID } });
  }

  public void ActiveTurn(string id)
  {
    MoveCamera(id);
    if (playerInfo.player.Id == id)
    {
      uiManager.ActiveDiceUI();
      uiManager.ActiveDirectionUI(MapManager.instance.GetTile(players[id].NowPosIndex.ToString()));
    }
  }

  public void UpdatePlayerStetus(Dictionary<string, Dictionary<string, string>> playerStetus)
  {
    foreach (var player in playerStetus)
    {
      players[player.Key].Deserialize(player.Value);
    }
  }
}
