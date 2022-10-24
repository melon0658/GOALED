using UnityEngine;
using System.Collections.Generic;

public class SendObject : MonoBehaviour
{
  private string objectId;
  public string ObjectId
  {
    get { return objectId; }
  }
  [SerializeField] private string prefub;
  [SerializeField] private bool isSyncPosition = true;
  [SerializeField] private float syncPositionPeriod = 0.001f;
  [SerializeField] private bool isSyncRotation = true;
  [SerializeField] private float syncRotationPeriod = 0.001f;
  [SerializeField] private bool isSyncScale = true;
  [SerializeField] private float syncScalePeriod = 0.001f;
  private Vector3 positionBefore = Vector3.zero;
  private Vector3 rotationBefore = Vector3.zero;
  private Vector3 scaleBefore = Vector3.zero;
  private GameService.Object go = new GameService.Object();
  public List<GameService.RPC> rpcs = new List<GameService.RPC>();
  private Manager manager;

  void Start()
  {
    objectId = System.Guid.NewGuid().ToString();
    manager = GameObject.Find("MainGameManager").GetComponent<Manager>();
    go.Id = objectId;
    go.Prefub = prefub;
    go.Owner = manager.playerInfo.player.Id;
    manager.AddSendObjects(gameObject);
  }

  void OnDestroy()
  {
    manager.AddRemoveObjects(toObject());
  }

  public void setTransform()
  {
    positionBefore = transform.position;
    rotationBefore = transform.rotation.eulerAngles;
    scaleBefore = transform.localScale;
  }

  public void setRPC(string rpc, Dictionary<string, string> args)
  {
    var rpcObj = new GameService.RPC();
    rpcObj.Method = rpc;
    rpcObj.Args.Add(args);
    rpcs.Add(rpcObj);
  }

  public bool isTransformChanged()
  {
    return (positionBefore - transform.position).magnitude > syncPositionPeriod || (rotationBefore - transform.rotation.eulerAngles).magnitude > syncRotationPeriod || (scaleBefore - transform.localScale).magnitude > syncScalePeriod;
  }

  public GameService.Object toObject()
  {
    if (isSyncPosition)
    {
      go.Position = new GameService.Vec3 { X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
    }
    if (isSyncRotation)
    {
      go.Rotation = new GameService.Vec3 { X = transform.rotation.eulerAngles.x, Y = transform.rotation.eulerAngles.y, Z = transform.rotation.eulerAngles.z };
    }
    if (isSyncScale)
    {
      go.Scale = new GameService.Vec3 { X = transform.localScale.x, Y = transform.localScale.y, Z = transform.localScale.z };
    }
    go.Rpc.AddRange(rpcs);
    rpcs.Clear();
    return go;
  }
}
