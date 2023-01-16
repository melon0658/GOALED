using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;


public class GameManager : MonoSingleton<GameManager>
{
  [SerializeField] public PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private GameObject carPrefub;
  [SerializeField] private RPCManager rpcManager;
  [SerializeField] private UIManager uiManager;
  private Dictionary<string, PlayerStetus> players = new Dictionary<string, PlayerStetus>();
  private Dictionary<string, List<Tile>> playerPathHistory = new Dictionary<string, List<Tile>>();
  private List<string> playerIds = new List<string>();
  private Dictionary<string, GameObject> cars = new Dictionary<string, GameObject>();

  public GameObject GetCar(string id)
  {
    return cars[id];
  }

  async void Start()
  {
    players = await GetPlayersAsync();
    MapManager.instance.GenerateTiles();
    if (playerInfo.isRoomOwner)
    {
      Debug.Log("owner");
      InitializeCar();
      await Task.Delay(5000);
      rpcManager.GetComponent<SendObject>().setRPC("ActiveTurn", new Dictionary<string, string>() { { "id", playerInfo.player.Id } });
    }
    else
    {
      await Task.Delay(1000);
      rpcManager.GetComponent<SendObject>().setRPC("Refresh", new Dictionary<string, string>());
    }
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

  private async Task<Dictionary<string, PlayerStetus>> GetPlayersAsync()
  {
    var res = await matchingServer.client.GetRoomDetailAsync(new MatchingService.GetRoomDetailRequest() { RoomId = playerInfo.player.RoomId });
    var players = new Dictionary<string, PlayerStetus>();
    foreach (var player in res.Room.Players)
    {
      players.Add(player.Id, new PlayerStetus(player.Name, 10000, 0, "red", "job", false, 0, 0, false));
      playerPathHistory.Add(player.Id, new List<Tile>());
    }
    playerIds = new List<string>(players.Keys);
    return players;
  }

  private void InitializeCar()
  {
    var position = MapManager.instance.StartTile.Position;
    foreach (var player in players)
    {
      var car = Instantiate(carPrefub, position, Quaternion.identity);
      position += new Vector3(20, 0, 0);
      cars.Add(player.Key, car);
      GameService.PlayerData playerData = new GameService.PlayerData();
      playerData.Id = player.Key;
      playerData.Key.Add("carID");
      playerData.Value.Add(player.Key);
      car.GetComponent<SendObject>().setPreObjectID(player.Key);
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
    var display = GameObject.Find("Display");
    display.transform.parent = car.transform;
    var backword = car.transform.TransformDirection(Vector3.back);
    var position = car.transform.position + (backword * 120) + new Vector3(0, 30, 0);
    var rotation = Quaternion.LookRotation(car.transform.position - position);
    await display.GetComponent<CameraManager>().MoveTo(position, rotation.eulerAngles);
  }

  public async void ResolveTurn(string id, string step, Direction direction)
  {
    if (!playerInfo.isRoomOwner)
    {
      return;
    }
    var car = cars[id];
    var carMove = car.GetComponent<CarMove>();
    var (tiles, path, rotate) = carMove.calcPath(MapManager.instance.GetTile(players[id].NowPosIndex.ToString()), int.Parse(step), direction);
    playerPathHistory[id].AddRange(tiles);
    await carMove.Move(path, rotate);
    var events = tiles.Where(x => x.Event != null && x.Event.GetEventType() == EventType.EVENT_PASS).ToList();
    if (tiles.Last().Event != null && tiles.Last().Event.GetEventType() == EventType.EVENT_STOP)
    {
      events.Add(tiles.Last());
    }
    String tileIDs = events.Select(x => x.id).Aggregate((x, y) => x + "," + y);

    rpcManager.GetComponent<SendObject>().setRPC("EventStart", new Dictionary<string, string>() { { "tileIDs", tileIDs }, { "playerID", id } });
  }

  public async void EventStart(string tileIDs, string playerID)
  {
    var tileIDList = tileIDs.Split(',');
    foreach (var tileID in tileIDList)
    {
      Tile tile = MapManager.instance.GetTile(tileID);
      TileEvent tileEvent = tile.Event;
      if (playerInfo.isRoomOwner)
      {
        tileEvent.OnEventChangeStetus(players, playerID);
        players[playerID].NowPosIndex = int.Parse(tileID);
        if (tileEvent.GetEventEffectType() == EventEffectType.EventEffectType_Move)
        {
          var car = cars[playerID];
          // cast to MoveEvent
          var moveEvent = tileEvent as MoveEvent;
          var step = moveEvent.getStep();
          var moveTile = playerPathHistory[playerID][playerPathHistory[playerID].Count - step];
          players[playerID].NowPosIndex = int.Parse(moveTile.id);
          car.transform.DOMove(moveTile.Position, 0.5f);
        }
        EncodePlayerStetus();
      }
      await tileEvent.OnEventAnimation();
    }
    if (playerInfo.isRoomOwner)
    {
      rpcManager.GetComponent<SendObject>().setRPC("ActiveTurn", new Dictionary<string, string>() { { "id", GetNextPlayerId(playerID) } });
    }
  }

  private void EncodePlayerStetus()
  {
    players.Select(x => x.Value.Serialize(x.Key)).ToList().ForEach(x => SyncManager.instance.AddPlayerData(x));
  }

  public void ActiveTurn(string id)
  {
    MoveCamera(id);
    if (playerInfo.player.Id == id)
    {
      uiManager.ActiveDiceButton();
      uiManager.ActiveDirectionButton(MapManager.instance.GetTile(players[id].NowPosIndex.ToString()));
    }
  }

  public void UpdatePlayerStetus(Dictionary<string, Dictionary<string, string>> playerStetus)
  {
    foreach (var player in playerStetus)
    {
      players[player.Key].Deserialize(player.Value);
    }
    uiManager.UpdatePlayerStetus(players);
  }
}
