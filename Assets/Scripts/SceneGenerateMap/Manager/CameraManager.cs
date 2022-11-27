using UnityEngine;
using System.Threading.Tasks;

public class CameraManager : MonoBehaviour
{
  public async Task MoveTo(Vector3 position, Vector3 rotation)
  {
    Vector3 positionBefore = transform.position;
    Vector3 positionAfter = position;
    Vector3 rotateBefore = transform.eulerAngles;
    Vector3 rotateAfter = rotation;
    float elapsedTime = 0;
    float InterpolationPeriod = 0.5f;
    while (transform.position != positionAfter || transform.eulerAngles != rotateAfter)
    {
      transform.position = Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod);
      transform.eulerAngles = Vector3.Lerp(rotateBefore, rotateAfter, elapsedTime / InterpolationPeriod);
      elapsedTime += Time.deltaTime;
      if (elapsedTime >= InterpolationPeriod)
      {
        transform.position = positionAfter;
        transform.eulerAngles = rotateAfter;
        break;
      }
      await Task.Delay(1);
    }
    return;
  }
}
