using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using DG.Tweening;

public class CarMove : MonoBehaviour
{
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
    positionBefore = transform.position;
    positionAfter = transform.position;
    rotateBefore = transform.eulerAngles;
    rotateAfter = transform.eulerAngles;
  }

  void Update()
  {
  }

  public async Task Move(List<Vector3> path, List<Vector3> rotations)
  {
    foreach (var item in path.Zip(rotations, (a, b) => new { a, b }))
    {
      Debug.Log(item.a + " " + item.b);
    }
    isMoving = true;
    await transform.DOPath(path.ToArray(), InterpolationPeriod * path.Count, PathType.CatmullRom, PathMode.Full3D, 10, Color.red).SetEase(Ease.Linear).SetLookAt(0.01f, Vector3.forward).AsyncWaitForCompletion();
    isMoving = false;
    return;
  }

  public (Tile, List<Vector3>, List<Vector3>) calcPath(Tile tile, int step, Direction? direction = null)
  {
    if (tile == null)
    {
      return (null, null, null);
    }
    if (tile.GetTileType() == TileType.LARGE_JUNCTION && direction == null)
    {
      return (null, null, null);
    }
    List<Vector3> path, rotate;
    (tile, path, rotate) = MapManager.instance.WayToTile(tile, step, (Direction?)direction);
    return (tile, path, rotate);
  }
}
