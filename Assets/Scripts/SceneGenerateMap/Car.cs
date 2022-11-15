using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Car : MonoBehaviour
{
  [SerializeField] public GameObject TileManager;
  [SerializeField] private GameObject Button;
  [SerializeField] private GameObject LeftButton;
  [SerializeField] private GameObject RightButton;
  [SerializeField] private GameObject CenterButton;
  private Tile tile;
  private const float InterpolationPeriod = 0.5f;
  private Vector3 positionBefore;
  private Vector3 positionAfter;
  private Vector3 rotateBefore;
  private Vector3 rotateAfter;
  private Vector3 NextPoint;
  private float elapsedTime;
  private bool isMoving = false;
  private Direction? chooseDirection;
  void Start()
  {
    positionBefore = transform.position;
    positionAfter = transform.position;
    rotateBefore = transform.eulerAngles;
    rotateAfter = transform.eulerAngles;
    Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    LeftButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirection(-1));
    CenterButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirection(0));
    RightButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirection(1));
    tile = TileManager.GetComponent<GenerateMap>().StartTile;
    LeftButton.SetActive(false);
    CenterButton.SetActive(false);
    RightButton.SetActive(false);
    ToggleButton(tile);
  }

  void Update()
  {
    elapsedTime += Time.deltaTime;
    if (Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod) - transform.position != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod) - transform.position, Vector3.up);
    }
    transform.position = Vector3.Lerp(positionBefore, positionAfter, elapsedTime / InterpolationPeriod);
  }

  private Direction CalcDirection(int direction)
  {
    var carDirection = GetDirection();
    if (direction == -1)
    {
      carDirection = carDirection - 1;
      if (carDirection < 0)
      {
        carDirection = (Direction)3;
      }
    }
    else if (direction == 1)
    {
      carDirection = carDirection + 1;
      if (carDirection > (Direction)3)
      {
        carDirection = 0;
      }
    }
    return carDirection;
  }

  private void OnClickDirection(int direction)
  {
    chooseDirection = CalcDirection(direction);
    Debug.Log("OnClickDirection:" + chooseDirection);
    LeftButton.SetActive(false);
    CenterButton.SetActive(false);
    RightButton.SetActive(false);
  }

  private Direction GetDirection()
  {
    if (transform.eulerAngles.y == 0)
    {
      return Direction.POSITIVE_Z;
    }
    else if (transform.eulerAngles.y == 90)
    {
      return Direction.POSITIVE_X;
    }
    else if (transform.eulerAngles.y == 180)
    {
      return Direction.NEGATIVE_Z;
    }
    else if (transform.eulerAngles.y == 270)
    {
      return Direction.NEGATIVE_X;
    }
    return Direction.POSITIVE_X;
  }

  private void ToggleButton(Tile t)
  {
    if (t.GetTileType() == TileType.LARGE_JUNCTION)
    {
      foreach (var tile in t.GetPassebleNeighbors())
      {
        if (tile.Key == CalcDirection(-1))
        {
          LeftButton.SetActive(true);
        }
        else if (tile.Key == CalcDirection(0))
        {
          CenterButton.SetActive(true);
        }
        else if (tile.Key == CalcDirection(1))
        {
          RightButton.SetActive(true);
        }
      }
    }
  }

  private async void Move(List<Vector3> path, List<Vector3> rotations)
  {
    isMoving = true;
    Button.SetActive(false);
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
    Button.SetActive(true);
    ToggleButton(tile);
  }

  public void OnClick()
  {
    if (isMoving)
    {
      return;
    }
    List<Vector3> path, rotate;
    var step = 1;
    if (tile != null)
    {
      if (tile.GetTileType() == TileType.LARGE_JUNCTION)
      {
        if (chooseDirection == null)
        {
          return;
        }
      }
    }
    (tile, path, rotate) = TileManager.GetComponent<GenerateMap>().WayToTile(tile, step, (Direction?)chooseDirection);
    Move(path, rotate);
    chooseDirection = null;
    Debug.Log(tile.Event.GetEventName());
  }
}
