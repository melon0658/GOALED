using UnityEngine;
using System;


public class ControllerUI : MonoBehaviour
{
  [SerializeField] private GameObject diceButton;
  [SerializeField] private GameObject leftButton;
  [SerializeField] private GameObject centerButton;
  [SerializeField] private GameObject rightButton;
  [NonSerialized] public Direction chooseDirection;

  void Start()
  {
    leftButton.SetActive(false);
    centerButton.SetActive(false);
    rightButton.SetActive(false);
    diceButton.SetActive(false);
    leftButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirectionButton(-1));
    centerButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirectionButton(0));
    rightButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClickDirectionButton(1));
    diceButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickDiceButton);
  }

  private void OnClickDirectionButton(int direction)
  {
    var carDirection = GetCarDirection();
    chooseDirection = CalcDirection(direction, carDirection);
    leftButton.SetActive(false);
    centerButton.SetActive(false);
    rightButton.SetActive(false);
  }

  private void OnClickDiceButton()
  {
    if (leftButton.activeSelf || centerButton.activeSelf || rightButton.activeSelf)
    {
      return;
    }
    diceButton.SetActive(false);
    var dice = UnityEngine.Random.Range(1, 7);
    GameManager.instance.onClickMove(dice, chooseDirection);
  }

  private Direction CalcDirection(int direction, Direction carDirection)
  {
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

  private Direction GetCarDirection()
  {
    var car = GameManager.instance.cars[GameManager.instance.getMyUserId()];
    return DirectionExtensions.GetDirection(car.transform.eulerAngles.y);
  }

  public void ActiveDirectionUI(Tile tile)
  {
    var carDirection = GetCarDirection();
    if (tile.GetTileType() != TileType.LARGE_JUNCTION)
    {
      return;
    }

    foreach (var t in tile.GetPassebleNeighbors())
    {
      if (t.Key == CalcDirection(-1, carDirection))
      {
        leftButton.SetActive(true);
      }
      else if (t.Key == CalcDirection(0, carDirection))
      {
        centerButton.SetActive(true);
      }
      else if (t.Key == CalcDirection(1, carDirection))
      {
        rightButton.SetActive(true);
      }
    }
  }

  public void ActiveDiceUI()
  {
    diceButton.SetActive(true);
  }
}