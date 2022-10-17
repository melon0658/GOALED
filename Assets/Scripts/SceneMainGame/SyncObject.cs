using UnityEngine;

public class SyncObject : MonoBehaviour
{
  public string ObjectId;
  private const float InterpolationPeriod = 1f / Settings.RECV_FPS;
  private Vector3 positionBefore;
  private Vector3 positionAfter;
  private float elapsedTime;

  void Start()
  {
    positionBefore = transform.position;
    positionAfter = positionBefore;
    // rotationBefore = transform.rotation.eulerAngles;
    // rotationAfter = rotationBefore;
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
    elapsedTime = 0f;
  }

  public string getID()
  {
    return ObjectId;
  }
}
