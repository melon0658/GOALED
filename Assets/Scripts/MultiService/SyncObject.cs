using UnityEngine;

public class SyncObject : MonoBehaviour
{
  public string objectId { get; set; }
  private const float InterpolationPeriod = 1f / Settings.RECV_FPS;
  private Vector3 positionBefore;
  private Vector3 positionAfter;
  private float elapsedTime;

  void Start()
  {
    positionBefore = transform.position;
    positionAfter = positionBefore;
    elapsedTime = 0f;
  }


  void Update()
  {
    elapsedTime += Time.deltaTime;
    transform.position = Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod);
  }

  public void Sync(GameService.Object obj)
  {
    if (obj.Position != null)
    {
      positionBefore = transform.position;
      positionAfter = new Vector3(obj.Position.X, obj.Position.Y, obj.Position.Z);
    }
    if (obj.Rotation != null)
    {
      transform.eulerAngles = new Vector3(obj.Rotation.X, obj.Rotation.Y, obj.Rotation.Z);
    }
    if (obj.Scale != null)
    {
      transform.localScale = new Vector3(obj.Scale.X, obj.Scale.Y, obj.Scale.Z);
    }
    if (obj.AnimatorParam != null)
    {
      foreach (var param in obj.AnimatorParam)
      {
        switch (param.Value.ValueCase)
        {
          case GameService.ParamValue.ValueOneofCase.BoolValue:
            GetComponent<Animator>().SetBool(param.Name, param.Value.BoolValue);
            break;
          case GameService.ParamValue.ValueOneofCase.FloatValue:
            GetComponent<Animator>().SetFloat(param.Name, param.Value.FloatValue);
            break;
          case GameService.ParamValue.ValueOneofCase.IntValue:
            GetComponent<Animator>().SetInteger(param.Name, param.Value.IntValue);
            break;
          case GameService.ParamValue.ValueOneofCase.TriggerValue:
            GetComponent<Animator>().SetBool(param.Name, param.Value.TriggerValue);
            break;
        }
      }
    }
    elapsedTime = 0f;
  }

  [CustomRPC]
  public void Delete()
  {
    Destroy(gameObject);
  }
}
