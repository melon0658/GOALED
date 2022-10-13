using UnityEngine;

public class SyncObject : MonoBehaviour
{
  public string ObjectId;
  private const float InterpolationPeriod = 1 / 20f;
  private Vector3 positionBefore;
  private Vector3 positionAfter;
  private Vector3 rotationBefore;
  private Vector3 rotationAfter;
  private float elapsedTime;

  void Start()
  {
    positionBefore = transform.position;
    positionAfter = positionBefore;
    rotationBefore = transform.rotation.eulerAngles;
    rotationAfter = rotationBefore;
    elapsedTime = 0f;
  }


  void Update()
  {
    elapsedTime += Time.deltaTime;
    transform.position = Vector3.Lerp(positionBefore, positionAfter, elapsedTime / Settings.RECV_FPS);
    transform.rotation = Quaternion.Euler(Vector3.Lerp(rotationBefore, rotationAfter, elapsedTime / Settings.RECV_FPS));
  }

  public void Sync(Vector3 pos, Vector3 rot, Vector3 scale)
  {
    positionBefore = transform.position;
    positionAfter = pos;
    rotationBefore = transform.rotation.eulerAngles;
    rotationAfter = rot;
    transform.localScale = scale;
    elapsedTime = 0f;
  }

  public string getID()
  {
    return ObjectId;
  }
}
