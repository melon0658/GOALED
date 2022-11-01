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
  [SerializeField] private bool isSyncAnimation = false;
  [SerializeField] private bool isRoomObject = false;
  private Vector3 positionBefore = Vector3.zero;
  private Vector3 rotationBefore = Vector3.zero;
  private Vector3 scaleBefore = Vector3.zero;
  private GameService.Object go = new GameService.Object();
  public List<GameService.RPC> rpcs = new List<GameService.RPC>();
  private Manager manager;
  private Animator animator;
  private CharacterController controller;

  void OnEnable()
  {

  }

  void Start()
  {
    objectId = System.Guid.NewGuid().ToString();
    manager = GameObject.Find("MainGameManager").GetComponent<Manager>();
    go.Id = objectId;
    go.Prefub = prefub;
    go.Owner = manager.playerInfo.player.Id;
    if (isSyncAnimation)
    {
      animator = GetComponent<Animator>();
      controller = GetComponent<CharacterController>();
      foreach (var param in animator.parameters)
      {
        var paramData = new GameService.AnimatorParam();
        paramData.Name = param.name;
        if (param.type == AnimatorControllerParameterType.Bool)
        {
          paramData.Value = new GameService.ParamValue();
          paramData.Value.BoolValue = animator.GetBool(param.name);
        }
        else if (param.type == AnimatorControllerParameterType.Float)
        {
          paramData.Value = new GameService.ParamValue();
          paramData.Value.FloatValue = animator.GetFloat(param.name);
        }
        else if (param.type == AnimatorControllerParameterType.Int)
        {
          paramData.Value = new GameService.ParamValue();
          paramData.Value.IntValue = animator.GetInteger(param.name);
        }
        else if (param.type == AnimatorControllerParameterType.Trigger)
        {
          paramData.Value = new GameService.ParamValue();
          paramData.Value.TriggerValue = false;
        }
      }
    }
    if (isRoomObject)
    {
      if (manager.playerInfo.isRoomOwner)
      {
        manager.AddSendObjects(gameObject);
      }
      else
      {
        gameObject.SetActive(false);
      }
    }
    else
    {
      manager.AddSendObjects(gameObject);
    }
  }

  public void delete()
  {
    setRPC("Delete", new Dictionary<string, string>());
    manager.AddRemoveObjects(toObject());
  }

  void OnDestroy()
  {
    delete();
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

  private void setAnimatorParam()
  {
    go.AnimatorParam.Clear();
    var animatorParams = new List<GameService.AnimatorParam>();
    if (animator == null)
    {
      return;
    }
    foreach (var param in animator.parameters)
    {
      var paramData = new GameService.AnimatorParam();
      paramData.Name = param.name;
      if (param.type == AnimatorControllerParameterType.Bool)
      {
        paramData.Value = new GameService.ParamValue();
        paramData.Value.BoolValue = animator.GetBool(param.name);
      }
      else if (param.type == AnimatorControllerParameterType.Float)
      {
        paramData.Value = new GameService.ParamValue();
        paramData.Value.FloatValue = animator.GetFloat(param.name);
      }
      else if (param.type == AnimatorControllerParameterType.Int)
      {
        paramData.Value = new GameService.ParamValue();
        paramData.Value.IntValue = animator.GetInteger(param.name);
      }
      else if (param.type == AnimatorControllerParameterType.Trigger)
      {
        paramData.Value = new GameService.ParamValue();
        paramData.Value.TriggerValue = animator.GetBool(param.name);
      }
      if (paramData.Value != null)
      {
        animatorParams.Add(paramData);
      }
    }
    go.AnimatorParam.AddRange(animatorParams);
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
      go.Scale = new GameService.Vec3 { X = transform.lossyScale.x, Y = transform.lossyScale.y, Z = transform.lossyScale.z };
    }
    if (isSyncAnimation)
    {
      setAnimatorParam();
    }
    go.Rpc.AddRange(rpcs);
    rpcs.Clear();
    return go;
  }
}
