using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseScript : MonoBehaviour
{
  //[SerializeField]
  private PathCreator pathCreator;
  public Roulette rScript;
  public Action actionScript;

  public GameObject upButton;
  public GameObject rightButton;
  public GameObject leftButton;

  public GameObject car1;
  public Material clearMaterial;


  float speed = 1f;
  Vector3 endPos;

  float moveDistance;

  private bool arrival = false;

  private int nowPosIndex;
  private int nextPosIndex;


  private int endPosIndex = 0;


  public int[] nextSquare1 = new int[] {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
                                          18, 19, 20, 21, 22, 23, 24, 0, 26, 27, 9};
  public Vector3[] coordinate = new[] { new Vector3(0f, 0f, 0f), new Vector3(-23.43f, 0f, -1.25f), new Vector3(-38.10f, 0f, 2.14f), new Vector3(-42.2f, 0f, 16.20f), new Vector3(-36.36f, 0f, 29.82f),
                                           new Vector3(-23.50f, 0f, 33.06f), new Vector3(-6.91f, 0f, 33.14f), new Vector3(9.57f, 0f, 33.14f), new Vector3(25.48f, 0f, 33.10f), new Vector3(44.08f, 0f, 32.61f),
                                           new Vector3(56.23f, 0f, 34.87f), new Vector3(58.22f, 0f, 49.39f), new Vector3(58.15f, 0f, 66.18f), new Vector3(58.10f, 0f, 83.00f), new Vector3(58.20f, 0f, 100.20f),
                                           new Vector3(54.73f, 0f, 114.31f), new Vector3(41.10f, 0f, 115.80f), new Vector3(24.62f, 0f, 115.69f), new Vector3(10.39f, 0f, 117.97f), new Vector3(8.20f, 0f, 133.60f),
                                           new Vector3(9.99f, 0f, 147.31f), new Vector3(25.93f, 0f, 149.77f), new Vector3(42.30f, 0f, 149.80f), new Vector3(57.26f, 0f, 152.13f), new Vector3(59.18f, 0f, 173.21f),
                                           new Vector3(25.50f, 0f, -0.50f), new Vector3(40.24f, 0f, 1.59f), new Vector3(41.76f, 0f, 16.30f)};

  //車が目的地についてるかの判定を返す
  public bool GetArrival()
  {
    return arrival;
  }
  public void SetPathCreator(PathCreator pc)
  {
    //使用するルートパスを指定
    this.pathCreator = pc;
    //Debug.Log(pathCreator.path.NumPoints);

    //this.SetEndPoth();

  }

  public void SetEndPoth()
  {

    int rCount = rScript.Rcount();
    if (this.transform.localPosition.x == 0 && this.transform.localPosition.z == 0)
    {
      if (pathCreator.ToString().IndexOf("StartRight") != -1)
      {
        nowPosIndex = 25;
      }
      else
      {
        nowPosIndex = 1;
      }
    }
    Debug.Log("rcount " + rCount);
    Debug.Log("nowPosIndex" + nowPosIndex);

    //if(rCount == 1)
    //{
    //    rCount--;
    //}
    if (rCount != 1)
    {
      for (int i = 0; i < rCount - 1; i++)
      {
        if (nowPosIndex != 0)
        {
          nextPosIndex = nextSquare1[nowPosIndex];
          nowPosIndex = nextPosIndex;
          Debug.Log(nowPosIndex);
        }
        else
        {
          nowPosIndex = 24;
          break;
        }

      }
    }


    endPosIndex = nowPosIndex;
    //endPosIndex = 9;

    //endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
    //int aa = pathCreator.path.NumPoints - (pathCreator.path.NumPoints / 10 * (11 - rCount)) + 1;
    //endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - (pathCreator.path.NumPoints / 10 * (11-rCount)) + 1 + strPosNum);
    //endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 50 + strPosNum);
    endPos = coordinate[endPosIndex];


    nowPosIndex = nextSquare1[endPosIndex];
    //strPosNum += 15;
    Debug.Log(endPos);
  }

  public PathCreator GetNowPath()
  {
    return pathCreator;
  }

  // Start is called before the first frame update
  void Start()
  {
    //Debug.Log(pathCreator.path.NumPoints);
    //endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
  }

  // Update is called once per frame
  void Update()
  {
    //moveDistance += speed * Time.deltaTime;
    //transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
    //transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);
  }

  public void moveStart()
  {
    InvokeRepeating("repeat", 0.0f, 0.015f);
    //InvokeRepeating("repeat", 0.0f, 0.03f);
  }

  void repeat()
  {
    AutoMove();

    //目的地に着いたらrepeatを止める
    if (arrival == true)
    {
      CancelInvoke("repeat");
      Debug.Log("終わり");
      arrival = false;
      rScript.SetisClicked();
      // マテリアルの付け替え
      car1.GetComponent<Renderer>().material = this.clearMaterial;



      Debug.Log("Name " + actionScript.GetCheckPointName());
      if (actionScript.GetCheckPoint())
      {
        if (actionScript.GetCheckPointName() == "CheckPosition")
        {
          rightButton.SetActive(true);
          leftButton.SetActive(true);
        }
        else
        {
          leftButton.SetActive(true);
          upButton.SetActive(true);
        }
      }
      else
      {
        rScript.PowerBarStart();
      }
    }
  }

  //目的地まで自動で移動
  public void AutoMove()
  {
    moveDistance += speed * Time.deltaTime;
    transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
    transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);

    if (this.transform.localPosition.x >= endPos.x - 1.0f && this.transform.localPosition.x <= endPos.x + 1.0 && this.transform.localPosition.z >= endPos.z - 1.0f && this.transform.localPosition.z <= endPos.z + 1.0f)
    {
      arrival = true;
    }
  }
}
