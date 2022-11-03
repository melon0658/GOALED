using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Car : MonoBehaviour
{
  [SerializeField] private GameObject TileManager;
  private Tile tile;
  private const float InterpolationPeriod = 0.5f;
  private Vector3 positionBefore;
  private Vector3 positionAfter;
  private Vector3 rotateBefore;
  private Vector3 rotateAfter;
  private Vector3 NextPoint;
  private float elapsedTime;
  private bool isMoving = false;
  void Start()
  {
    Debug.Log("Car Start");
    positionBefore = transform.position;
    positionAfter = transform.position;
    rotateBefore = transform.eulerAngles;
    rotateAfter = transform.eulerAngles;
  }
  void Update()
  {
    elapsedTime += Time.deltaTime;
    if (Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod) - transform.position != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod) - transform.position, Vector3.up);
    }
    transform.position = Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod);
    // transform.rotation = Quaternion.Lerp(Quaternion.Euler(rotateBefore), Quaternion.Euler(rotateAfter), elapsedTime / InterpolationPeriod);
  }

  private async void Move(List<Vector3> path, List<Vector3> rotations)
  {
    isMoving = true;
    for (int i = 0; i < path.Count; i++)
    {
      positionBefore = transform.position;
      positionAfter = path[i];
      rotateBefore = transform.eulerAngles;
      rotateAfter = rotations[i];
      elapsedTime = 0;
      await Task.Delay((int)(InterpolationPeriod * 1000));
    }
    isMoving = false;
  }

  public void OnClick()
  {
    if (isMoving)
    {
      return;
    }
    var step = 100;
    Debug.Log("Step: " + step);
    List<Vector3> path, rotate;
    (tile, path, rotate) = TileManager.GetComponent<GenerateMap>().WayToTile(tile, step);
    Move(path, rotate);
  }
}
