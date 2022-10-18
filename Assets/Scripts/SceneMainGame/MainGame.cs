using UnityEngine;
using UnityEngine.Events;
using Grpc.Core;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

public class MainGame : MonoBehaviour
{
  private CancellationToken ct;
  [SerializeField] private GameServer gameServer;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] public PlayerInfo playerInfo;
  private Dictionary<string, GameObject> syncObjects = new Dictionary<string, GameObject>();
  private ConcurrentDictionary<string, GameObject> sendObjects = new ConcurrentDictionary<string, GameObject>();
  private Dictionary<string, Dictionary<string, string>> playerData = new Dictionary<string, Dictionary<string, string>>();
  private Queue<GameService.PlayerData> playerDataQueue = new Queue<GameService.PlayerData>();
  private AsyncClientStreamingCall<GameService.SendObjectRequest, GameService.SendObjectResponse> sendCall;
  private AsyncClientStreamingCall<GameService.SendPlayerDataRequest, GameService.SendPlayerDataResponse> playerDataSendCall;
  [Serializable] public class PlayerDataEvent : UnityEvent<Dictionary<string, Dictionary<string, string>>> { }
  [SerializeField] private PlayerDataEvent onChangePlayerData;
  private bool syncEnd = false;

  void Start()
  {
    SendObject();
    RecvObject();
    SendPlayerData();
    RecvPlayerData();
  }


  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      SceneManager.LoadScene("SelectRoomScene");
    }
  }

  void OnDestroy()
  {
    Close();
  }

  public void Close()
  {
    sendCall.RequestStream.CompleteAsync();
    playerDataSendCall.RequestStream.CompleteAsync();
    gameServer.client.CloseStream(new GameService.CloseStreamRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    matchingServer.client.LeaveRoom(new MatchingService.LeaveRoomRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    syncEnd = true;
  }


  public void AddSendObjects(GameObject go)
  {
    sendObjects.TryAdd(go.GetComponent<SendObject>().getID(), go);
  }

  public void AddPlayerData(GameService.PlayerData playerData)
  {
    playerDataQueue.Enqueue(playerData);
  }

  private async void SendObject()
  {
    sendCall = gameServer.client.SendObject();
    while (!syncEnd)
    {
      var request = new GameService.SendObjectRequest();
      request.PlayerId = playerInfo.player.Id;
      request.RoomId = playerInfo.player.RoomId;
      foreach (var obj in sendObjects)
      {
        SendObject sendObject = obj.Value.GetComponent<SendObject>();
        if (sendObject.isTransformChanged())
        {
          request.Object.Add(sendObject.toObject());
          sendObject.setTransform();
        }
      }
      if (request.Object.Count > 0)
      {
        await sendCall.RequestStream.WriteAsync(request);
      }
      await Task.Delay(1000 / (int)Settings.SEND_FPS);
    }
  }

  private async void RecvObject()
  {
    var recvCall = gameServer.client.SyncObject(new GameService.SyncObjectRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    while (await recvCall.ResponseStream.MoveNext(ct))
    {
      var response = recvCall.ResponseStream.Current;
      foreach (var obj in response.Object)
      {
        if (obj.Owner == playerInfo.player.Id)
        {
          continue;
        }

        if (syncObjects.ContainsKey(obj.Id))
        {
          var go = syncObjects[obj.Id];
          if (go != null)
          {
            go.GetComponent<SyncObject>().Sync(obj);
          }
        }
        else
        {
          GameObject go = Instantiate(Resources.Load<GameObject>(obj.Prefub));
          go.transform.position = new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z);
          go.transform.rotation = Quaternion.Euler(new Vector3(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z));
          go.transform.localScale = new Vector3(obj.Scale.X, obj.Scale.Y, obj.Scale.Z);
          go.GetComponent<SyncObject>().ObjectId = obj.Id;
          syncObjects.Add(obj.Id, go);
        }
      }
    }
  }

  private async void SendPlayerData()
  {
    playerDataSendCall = gameServer.client.SendPlayerData();
    while (!syncEnd)
    {
      var items = playerDataQueue.Count;
      if (items > 0)
      {
        var request = new GameService.SendPlayerDataRequest();
        request.RoomId = playerInfo.player.RoomId;
        for (int i = 0; i < items; i++)
        {
          request.PlayerData.Add(playerDataQueue.Dequeue());
        }
        await playerDataSendCall.RequestStream.WriteAsync(request);
      }
      await Task.Delay(1000 / (int)Settings.SEND_FPS);
    }
  }

  private async void RecvPlayerData()
  {
    var recvCall = gameServer.client.SyncPlayerData(new GameService.SyncPlayerDataRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    while (await recvCall.ResponseStream.MoveNext(ct))
    {
      var response = recvCall.ResponseStream.Current;
      foreach (var data in response.PlayerData)
      {
        if (!playerData.ContainsKey(data.Id))
        {
          playerData.Add(data.Id, new Dictionary<string, string>());
        }
        foreach ((string key, string value) in data.Key.Zip(data.Value, (k, v) => (k, v)))
        {
          playerData[data.Id][key] = value;
        }
      }
      onChangePlayerData.Invoke(playerData);
    }
  }
}
