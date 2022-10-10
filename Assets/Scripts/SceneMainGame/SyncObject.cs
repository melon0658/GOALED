using UnityEngine;

public class SyncObject : MonoBehaviour
{
  public string ObjectId;
  private const float InterpolationPeriod = 1 / 20f;
  private Vector3 p1;
  private Vector3 p2;
  private float elapsedTime;

  void Start()
  {
    p1 = transform.position;
    p2 = p1;
    elapsedTime = 0f;
  }


  void Update()
  {
    elapsedTime += Time.deltaTime;
    transform.position = Vector3.Lerp(p1, p2, elapsedTime / InterpolationPeriod);
  }

  public void Sync(Vector3 pos)
  {
    p1 = transform.position;
    p2 = pos;
    elapsedTime = 0f;
  }

  public string getID()
  {
    return ObjectId;
  }
}
