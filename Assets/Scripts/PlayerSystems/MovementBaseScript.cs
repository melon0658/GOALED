using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseScript : MonoBehaviour
{
  private PathCreator pathCreator;
  public Roulette rScript;
  public Action actionScript;
  public Player playerScript;
  public EventSystem eventSystemScript;
  private TurnSystem turnSystemScript;

  public GameObject upButton;
  public GameObject rightButton;
  public GameObject leftButton;

  public GameObject car;
  public Material clearMaterial;

  private float speed = 1f;
  private Vector3 endPos;
  private float moveDistance;
  private bool arrival = false;

  private int nowPosIndex;
  private int nextPosIndex;

  private int endPosIndex = 0;

  //次のマスのIndexを指す配列
  public int[] nextPosIndexes1 = new int[] {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 0, 
                                            26, 27, 9, 
                                            29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 0, 
                                            45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 34, 
                                            63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 0, 
                                            84, 85, 71
                                            };

  //各マスの座標(パブリック)を格納している配列
  public Vector3[] coordinate = new[] { new Vector3(0f, 0f, 0f), new Vector3(42.70f, 0f, -170.80f), new Vector3(21.51f, 0f, -168.24f), new Vector3(17.90f, 0f, -146.30f), new Vector3(21.92f, 0f, -121.91f),
                                        new Vector3(44.40f, 0f, -121.10f), new Vector3(69.20f, 0f, -121.10f), new Vector3(94.77f, 0f, -121.10f), new Vector3(119.60f, 0f, -121.10f), new Vector3(145.80f, 0f, -121.40f),
                                        new Vector3(164.96f, 0f, -118.12f), new Vector3(168.33f, 0f, -94.77f), new Vector3(168.22f, 0f, -68.88f), new Vector3(168.20f, 0f, -45.00f), new Vector3(168.30f, 0f, -20.20f),
                                        new Vector3(162.92f, 0f, 1.61f), new Vector3(142.90f, 0f, 3.70f), new Vector3(117.70f, 0f, 3.50f), new Vector3(95.72f, 0f, 8.31f), new Vector3(93.30f, 0f, 29.50f),
                                        new Vector3(96.09f, 0f, 51.07f), new Vector3(119.35f, 0f, 54.66f), new Vector3(144.52f, 0f, 54.66f), new Vector3(165.53f, 0f, 56.70f), new Vector3(169.77f, 0f, 89.82f), 
                                        new Vector3(119.30f, 0f, -170.77f), new Vector3(142.23f, 0f, -166.22f), new Vector3(143.64f, 0f, -147.19f),
                                        new Vector3(138.39f, 0f, 91.59f), new Vector3(113.23f, 0f, 91.59f), new Vector3(87.81f, 0f, 91.60f), new Vector3(63.20f, 0f, 91.60f), new Vector3(37.74f, 0f, 91.61f),
                                        new Vector3(18.70f, 0f, 107.47f), new Vector3(14.60f, 0f, 128.80f), new Vector3(-7.70f, 0f, 128.80f), new Vector3(-31.91f, 0f, 128.83f), new Vector3(-57.30f, 0f, 128.80f),
                                        new Vector3(-82.10f, 0f, 128.80f), new Vector3(-107.59f, 0f, 128.83f), new Vector3(-131.56f, 0f, 128.83f), new Vector3(-155.56f, 0f, 125.88f), new Vector3(-156.89f, 0f, 103.00f),
                                        new Vector3(-156.89f, 0f, 67.61f), new Vector3(169.10f, 0f, 129.90f), new Vector3(169.09f, 0f, 154.82f), new Vector3(169.10f, 0f, 179.90f), new Vector3(166.78f, 0f, 202.57f),
                                        new Vector3(142.66f, 0f, 203.97f), new Vector3(120.28f, 0f, 201.96f), new Vector3(118.63f, 0f, 178.50f), new Vector3(118.60f, 0f, 153.40f), new Vector3(115.43f, 0f, 129.51f),
                                        new Vector3(93.10f, 0f, 129.00f), new Vector3(71.30f, 0f, 130.44f), new Vector3(68.89f, 0f, 153.54f), new Vector3(68.90f, 0f, 178.50f), new Vector3(65.04f, 0f, 203.32f),
                                        new Vector3(43.40f, 0f, 204.50f), new Vector3(20.22f, 0f, 202.46f), new Vector3(18.41f, 0f, 178.87f), new Vector3(18.42f, 0f, 154.58f), 
                                        new Vector3(-125.64f, 0f, 65.77f), new Vector3(-100.30f, 0f, 65.80f), new Vector3(-81.67f, 0f, 51.69f), new Vector3(-81.60f, 0f, 28.10f), new Vector3(-81.60f, 0f, 3.20f),
                                        new Vector3(-81.60f, 0f, -21.60f), new Vector3(-85.59f, 0f, -44.52f), new Vector3(-107.51f, 0f, -45.93f), new Vector3(-132.10f, 0f, -45.90f),             new Vector3(-155.87f, 0f, -47.95f),
                                        new Vector3(-157.05f, 0f, -72.88f), new Vector3(-157.05f, 0f, -96.53f), new Vector3(-157.05f, 0f, -121.4f), new Vector3(-157.04f, 0f, -147.71f), new Vector3(-153.41f, 0f, -169.12f),
                                        new Vector3(-130.60f, 0f, -170.33f), new Vector3(-105.71f, 0f, -170.33f), new Vector3(-80.10f, 0f, -170.33f), new Vector3(-59.08f, 0f, -168.75f), new Vector3(-55.88f, 0f, -145.50f),
                                        new Vector3(-55.88f, 0f, -124.67f), new Vector3(-156.80f, 0f, 28.20f), new Vector3(-156.80f, 0f, 3.80f), new Vector3(-156.80f, 0f, -21.90f)
                                       };

  public bool GetArrival()
  {
    //車が目的地についてるかの判定を返す
    return arrival;
  }

  public void SetPathCreator(PathCreator pc)
  {
    //使用するルートパスを指定
    this.pathCreator = pc;
    moveDistance = 0.0f;
  }

  public void SetEndPoth()
  {
    int rCount = rScript.Rcount();

    //pathCreatorに合わせてIndexのスタート位置を変える
    if (actionScript.GetCheckPoint())
    {
      if (actionScript.GetCheckPointName() == "CheckPosition")
      {
        //pathCreatorがStartRight
        if (pathCreator.ToString().IndexOf("StartRight") != -1)
        {
          nowPosIndex = 25;
          if (rCount > 4)
          {
            rCount = 4;
          }
        }
        //pathCreatorがStartLeft
        else
        {
          nowPosIndex = 1;
          if (rCount > 9)
          {
            rCount = 9;
          }
        }
      }
      else if (actionScript.GetCheckPointName() == "CheckPosition2")
      {
        //pathCreatorがSecondUp
        if (pathCreator.ToString().IndexOf("SecondUp") != -1)
        {
          nowPosIndex = 44;
        }
        //pathCreatorがSecondLeft
        else
        {
          nowPosIndex = 28;
        }
      }
      else if (actionScript.GetCheckPointName() == "CheckPosition3")
      {
        //pathCreatorがLastUp
        if (pathCreator.ToString().IndexOf("LastUp") != -1)
        {
          nowPosIndex = 83;
        }
        //pathCreatorがLastLeft
        else
        {
          nowPosIndex = 62;
        }
      }


      actionScript.SetCheckPoint(false);
    }
    else
    {
      nowPosIndex = nextPosIndexes1[playerScript.NowPosIndex];
    }


    //Debug.Log(pathCreator.ToString());

    //Debug.Log("rcount " + rCount);
    //Debug.Log("nowPosIndex" + nowPosIndex);

    //デバッグ用
    //if (actionScript.GetCheckPointName() == "CheckPosition3")
    //{
    //  rCount = 4;
    //}
    //else
    //{
    //  rCount = 10;
    //}
    //if (!actionScript.GetCheckPoint())
    //{

    //}


    if (rCount != 1)
    {
      for (int i = 0; i < rCount - 1; i++)
      {
        if (nowPosIndex != 0)
        {
          nextPosIndex = nextPosIndexes1[nowPosIndex];
          nowPosIndex = nextPosIndex;
        }
        else
        {
          break;
        }
      }
    }
    if(nowPosIndex == 0)
    {
      if (actionScript.GetCheckPointName() == "CheckPosition")
      {
        nowPosIndex = 24;
      }
      else if (actionScript.GetCheckPointName() == "CheckPosition2")
      {
        nowPosIndex = 43;
      }
      else if (actionScript.GetCheckPointName() == "CheckPosition3")
      {
        nowPosIndex = 82;
      }
    }
    //Debug.Log(nowPosIndex);

    endPosIndex = nowPosIndex;
    
    //チェック用
    //endPosIndex = 24;

    endPos = coordinate[endPosIndex];

    playerScript.NowPosIndex = endPosIndex;

    //nowPosIndex = nextPosIndexes1[endPosIndex];
    Debug.Log(endPos);
  }

  public PathCreator GetNowPath()
  {
    return pathCreator;
  }

  // Start is called before the first frame update
  void Start()
  {
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
  }

  //仕事決まった時用
  public void jobEventAfterMove()
  {
    endPosIndex = 9;

    //チェック用
    //endPosIndex = 24;

    endPos = coordinate[endPosIndex];

    playerScript.NowPosIndex = endPosIndex;

    moveStart();
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
      //Debug.Log("終わり");
      arrival = false;
      rScript.SetisClicked();

      //イベントの実行に移る
      eventSystemScript.EventExecutionManager();

      //ターンを終了する　
      //turnSystemScript.TurnEndSystemMaster();

    }
  }

  //目的地まで自動で移動
  public void AutoMove()
  {
    if (actionScript.GetCheckPointName() != "CheckPosition")
    {
      moveDistance -= speed * Time.deltaTime;
    }
    else{
      moveDistance += speed * Time.deltaTime;
    }
    
    transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
    transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);

    if (this.transform.position.x >= endPos.x - 1.5f && this.transform.position.x <= endPos.x + 1.5 && this.transform.position.z >= endPos.z - 1.5f && this.transform.position.z <= endPos.z + 1.5f)
    {
      arrival = true;
    }
  }
}
