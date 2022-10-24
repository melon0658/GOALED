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

public class Manager : MonoBehaviour
{
  private CancellationToken ct;
  [SerializeField] private GameServer gameServer;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] public PlayerInfo playerInfo;
  [SerializeField] private bool isDebugMode = false;
  private AsyncClientStreamingCall<GameService.SendObjectRequest, GameService.SendObjectResponse> sendCall;
  private AsyncClientStreamingCall<GameService.SendPlayerDataRequest, GameService.SendPlayerDataResponse> playerDataSendCall;
  private ConcurrentDictionary<string, GameObject> sendObjects = new ConcurrentDictionary<string, GameObject>();
  private ConcurrentDictionary<string, GameObject> recvObjects = new ConcurrentDictionary<string, GameObject>();
  private ConcurrentDictionary<string, GameService.Object> removeObjects = new ConcurrentDictionary<string, GameService.Object>();
  private Queue<GameService.PlayerData> playerDataQueue = new Queue<GameService.PlayerData>();
  private Dictionary<string, Dictionary<string, string>> recvPlayerData = new Dictionary<string, Dictionary<string, string>>();
  [Serializable] public class PlayerDataEvent : UnityEvent<Dictionary<string, Dictionary<string, string>>> { }
  [SerializeField] private PlayerDataEvent onChangePlayerData;
  private bool isFinish = false;

  private void DebugMode()
  {
    var id = matchingServer.client.GetPlayerId(new MatchingService.GetPlayerIdRequest()).PlayerId;
    playerInfo.player = new MatchingService.Player { Id = id, Name = "test", RoomId = "test" };
    gameServer.client.CreateRoom(new GameService.Room { Id = "test", Name = "test", Owner = id });
  }

  void Start()
  {
    if (isDebugMode)
    {
      DebugMode();
    }
    sendCall = gameServer.client.SendObject();
    SendObjectLoop();
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

  async void OnDestroy()
  {
    while (Array.FindAll(sendObjects.Values.ToArray(), x => x != null).Length > 0)
    {
      await Task.Delay(100);
    }
    Close();
  }

  public void Close()
  {
    sendCall.RequestStream.CompleteAsync();
    playerDataSendCall.RequestStream.CompleteAsync();

    foreach (var obj in sendObjects)
    {
      if (obj.Value != null)
      {
        Destroy(obj.Value);
      }
    }
    var close = new GameService.CloseStreamRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId };
    foreach (var obj in removeObjects)
    {
      close.Object.Add(obj.Value);
    }
    gameServer.client.CloseStream(close);
    Debug.Log("Close");

    if (!isDebugMode)
    {
      matchingServer.client.LeaveRoom(new MatchingService.LeaveRoomRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    }
    isFinish = true;
  }

  public void AddSendObjects(GameObject go)
  {
    sendObjects.TryAdd(go.GetComponent<SendObject>().ObjectId, go);
  }

  public void AddRemoveObjects(GameService.Object obj)
  {
    removeObjects.TryAdd(obj.Id, obj);
  }

  private GameService.SendObjectRequest CreateSendRequest()
  {
    var request = new GameService.SendObjectRequest();
    request.PlayerId = playerInfo.player.Id;
    request.RoomId = playerInfo.player.RoomId;
    foreach (var obj in sendObjects)
    {
      if (obj.Value == null)
      {
        continue;
      }
      SendObject sendObject = obj.Value.GetComponent<SendObject>();
      if (sendObject.isTransformChanged())
      {
        request.Object.Add(sendObject.toObject());
        sendObject.setTransform();
      }
    }
    foreach (var obj in removeObjects)
    {
      request.Object.Add(obj.Value);
    }
    removeObjects.Clear();
    return request;
  }

  private async void SendObject(GameService.SendObjectRequest request)
  {
    await sendCall.RequestStream.WriteAsync(request);
  }

  private async void SendObjectLoop()
  {
    while (!isFinish)
    {
      var request = CreateSendRequest();
      if (request.Object.Count > 0)
      {
        SendObject(request);
      }
      await Task.Delay(1000 / (int)Settings.SEND_FPS);
    }
  }

  private GameObject InitiateObject(GameService.Object obj)
  {
    GameObject go = Instantiate(Resources.Load<GameObject>(obj.Prefub));
    go.transform.position = new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z);
    go.transform.rotation = Quaternion.Euler(new Vector3(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z));
    go.transform.localScale = new Vector3(obj.Scale.X, obj.Scale.Y, obj.Scale.Z);
    go.GetComponent<SyncObject>().objectId = obj.Id;
    return go;
  }

  private void ResolveRPC(GameService.Object obj)
  {
    GameObject go = recvObjects[obj.Id];
    var rpcs = obj.Rpc;
    if (rpcs.Count == 0)
    {
      return;
    }
    foreach (var rpc in rpcs)
    {
      Debug.Log(rpc.Method + " " + rpc.Args);
    }
    // アタッチされているものの一覧を取得
    var components = go.GetComponents<MonoBehaviour>();
    foreach (var component in components)
    {
      var methods = component.GetType().GetMethods();
      foreach (var method in methods)
      {
        // CustomRPCがついているメソッドを探す
        var attributes = method.GetCustomAttributes(typeof(CustomRPC), true);
        if (attributes.Length > 0)
        {
          // メソッド名が一致するか
          var rpc = rpcs.Where(r => r.Method == method.Name).FirstOrDefault();
          if (rpc != null)
          {
            // 引数の数が一致するか
            var parameters = method.GetParameters();
            if (parameters.Length == rpc.Args.Count)
            {
              // 引数の型が一致するか
              bool isMatch = true;
              foreach (var parameter in parameters)
              {
                var arg = rpc.Args.ContainsKey(parameter.Name) ? rpc.Args[parameter.Name] : null;
                if (arg == null)
                {
                  isMatch = false;
                  break;
                }
                // 引数の型に変換できるか
                try
                {
                  Convert.ChangeType(arg, parameter.ParameterType);
                }
                catch (Exception)
                {
                  isMatch = false;
                  break;
                }
              }
              if (isMatch)
              {
                // 引数を作成
                var args = new List<object>();
                foreach (var parameter in parameters)
                {
                  var arg = rpc.Args.ContainsKey(parameter.Name) ? rpc.Args[parameter.Name] : null;
                  args.Add(Convert.ChangeType(arg, parameter.ParameterType));
                }
                Debug.Log("RPC: " + rpc.Method);
                // メソッドを実行
                method.Invoke(component, args.ToArray());
              }
            }
          }
        }
      }
    }
  }

  private void SetRecvObject(GameService.Object obj)
  {
    var id = obj.Id;
    if (obj.Owner == playerInfo.player.Id)
    {
      return;
    }
    if (recvObjects.ContainsKey(id))
    {
      var go = recvObjects[id];
      if (go != null)
      {
        go.GetComponent<SyncObject>().Sync(obj);
        ResolveRPC(obj);
      }
    }
    else
    {
      GameObject go = InitiateObject(obj);
      recvObjects.TryAdd(id, go);
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
        SetRecvObject(obj);
      }
    }
  }

  private GameService.SendPlayerDataRequest CreateSendPlayerDataRequest()
  {
    var request = new GameService.SendPlayerDataRequest();
    request.RoomId = playerInfo.player.Id;
    var items = playerDataQueue.Count;
    for (int i = 0; i < items; i++)
    {
      request.PlayerData.Add(playerDataQueue.Dequeue());
    }
    return request;
  }

  private async void SendPlayerData()
  {
    playerDataSendCall = gameServer.client.SendPlayerData();
    while (!isFinish)
    {
      var request = CreateSendPlayerDataRequest();
      if (request.PlayerData.Count > 0)
      {
        await playerDataSendCall.RequestStream.WriteAsync(request);
      }
      await Task.Delay(1000 / (int)Settings.SEND_FPS);
    }
  }

  private void SetRecvPlayerData(GameService.PlayerData playerData)
  {
    var player_id = playerData.Id;
    if (!recvPlayerData.ContainsKey(player_id))
    {
      recvPlayerData.TryAdd(player_id, new Dictionary<string, string>());
    }
    foreach ((string key, string value) in playerData.Key.Zip(playerData.Value, (k, v) => (k, v)))
    {
      recvPlayerData[player_id][key] = value;
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
        SetRecvPlayerData(data);
      }
      onChangePlayerData.Invoke(recvPlayerData);
    }
  }
}
