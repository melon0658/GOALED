using UnityEngine;
using Grpc.Core;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

public class MainGame : MonoBehaviour
{
  private CancellationToken ct;
  [SerializeField] private GameServer gameServer;
  [SerializeField] private PlayerInfo playerInfo;
  private Dictionary<string, GameObject> syncObjects = new Dictionary<string, GameObject>();
  private ConcurrentDictionary<string, GameObject> sendObjects = new ConcurrentDictionary<string, GameObject>();
  private bool syncEnd = false;
  private Fps sendFps = new Fps();
  private Fps recvFps = new Fps();


  void Start()
  {
    SendObject();
    RecvObject();
  }


  void Update()
  {
  }

  void OnDestroy()
  {
    syncEnd = true;
  }

  public void AddSendObjects(GameObject go)
  {
    sendObjects.TryAdd(go.GetComponent<SendObject>().getID(), go);
    Debug.Log("AddSendObjects " + go.GetComponent<SendObject>().getID());
  }

  private GameService.Object ToObject(GameObject gameObject)
  {
    var go = new GameService.Object();
    go.Id = gameObject.GetComponent<SendObject>().getID();
    go.Owner = playerInfo.player.Id;
    go.Prefub = gameObject.GetComponent<SendObject>().getPrefub();
    go.Position = new GameService.Vec3 { X = gameObject.transform.position.x, Y = gameObject.transform.position.y, Z = gameObject.transform.position.z };
    go.Rotation = new GameService.Vec3 { X = gameObject.transform.rotation.x, Y = gameObject.transform.rotation.y, Z = gameObject.transform.rotation.z };
    go.Scale = new GameService.Vec3 { X = gameObject.transform.localScale.x, Y = gameObject.transform.localScale.y, Z = gameObject.transform.localScale.z };
    go.Rpc = "";
    return go;
  }

  private async void SendObject()
  {
    var sendCall = gameServer.client.SendObject();
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
          request.Object.Add(ToObject(obj.Value));
          sendObject.setTransform();
        }
      }
      if (request.Object.Count > 0)
      {
        await sendCall.RequestStream.WriteAsync(request);
        sendFps.Update();
      }
      await Task.Delay(1000 / 30);
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
          go.GetComponent<SyncObject>().Sync(new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z));
          go.transform.rotation = Quaternion.Euler(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z);
          go.transform.localScale = new Vector3(obj.Scale.X, obj.Scale.Y, obj.Scale.Z);
        }
        else
        {
          GameObject go = Instantiate(Resources.Load<GameObject>(obj.Prefub));
          go.transform.position = new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z);
          go.transform.rotation = Quaternion.Euler(new Vector3(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z));
          go.transform.localScale = new Vector3(obj.Scale.X, obj.Scale.Y, obj.Scale.Z);
          // go.AddComponent<SyncObject>();
          go.GetComponent<SyncObject>().ObjectId = obj.Id;
          // Instantiate(go);
          syncObjects.Add(obj.Id, go);
        }
      }
      recvFps.Update();
      // Debug.Log("Recv FPS:" + recvFps.GetFPS() + " Flames:" + recvFps.GetFlames());
    }
  }
}
