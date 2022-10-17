using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseScript : MonoBehaviour
{
    //[SerializeField]
    private PathCreator pathCreator;

    public Roulette rScript;

    float speed = 1f;
    Vector3 endPos;

    float moveDistance;

    private bool arrival = false;

    private int nowPosIndex;
    private int nextPosIndex;


    private int endPosIndex = 0;


    public int[] nextSquare1 = new int[] {0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 
                                          18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 0, 0, 30, 31, 32, 12};
    public Vector3[] coordinate = new[] { new Vector3(0f, 0f, 0f), new Vector3(-23.43f, 0f, -1.25f), new Vector3(-38.10f, 0f, 2.14f), new Vector3(-40.72f, 0f, 16.20f), new Vector3(-36.36f, 0f, 29.82f),
                                           new Vector3(-22.43f, 0f, 33.06f), new Vector3(-6.91f, 0f, 33.14f), new Vector3(9.57f, 0f, 33.14f), new Vector3(25.48f, 0f, 33.10f), new Vector3(41.79f, 0f, 33.06f),
                                           new Vector3(57.53f, 0f, 33.05f), new Vector3(70.80f, 0f, 29.94f), new Vector3(75.21f, 0f, 15.76f), new Vector3(89.15f, 0f, 15.61f), new Vector3(103.07f, 0f, 18.63f),
                                           new Vector3(105.38f, 0f, 32.96f), new Vector3(105.39f, 0f, 49.01f), new Vector3(105.37f, 0f, 64.90f), new Vector3(105.37f, 0f, 80.76f), new Vector3(101.95f, 0f, 94.16f),
                                           new Vector3(88.44f, 0f, 96.45f), new Vector3(72.49f, 0f, 96.46f), new Vector3(58.03f, 0f, 99.36f), new Vector3(56.40f, 0f, 114.00f), new Vector3(59.30f, 0f, 128.22f), 
                                           new Vector3(73.50f, 0f, 129.40f), new Vector3(89.50f, 0f, 129.30f), new Vector3(102.90f, 0f, 132.40f), new Vector3(104.40f, 0f, 149.20f), 
                                           new Vector3(25.89f, 0f, -1.57f), new Vector3(41.46f, 0f, -1.43f), new Vector3(57.05f, 0f, -1.30f), new Vector3(71.40f, 0f, 1.90f) };

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

        this.SetEndPoth();
        
    }

    public void SetEndPoth()
    {
        
        int rCount = rScript.Rcount();
        if(this.transform.localPosition.x == 0 && this.transform.localPosition.z == 0)
        {
            if (pathCreator.ToString().IndexOf("StartRight") != -1)
            {
                nowPosIndex = 29;
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
        if(rCount != 1)
        {
            for (int i = 0; i < rCount-1; i++)
            {
                if (nowPosIndex != 0)
                {
                    nextPosIndex = nextSquare1[nowPosIndex];
                    nowPosIndex = nextPosIndex;
                    Debug.Log(nowPosIndex);
                }
                else
                {
                    nowPosIndex = 28;
                    break;
                }

            }
        }
        

        endPosIndex = nowPosIndex;

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
        }
    }

    //目的地まで自動で移動
    public void AutoMove()
    {
        //(int)this.transform.position.x != (int)endPos.x
        //if ((this.transform.position.x > endPos.x - 0.05 && this.transform.position.x < endPos.x + 0.05) || (this.transform.position.z > endPos.z - 0.05 && this.transform.position.z < endPos.z + 0.05))
        //{
        //    //Debug.Log(this.transform.position.z);
        //    //Debug.Log(endPos.z);
        //    moveDistance += speed * Time.deltaTime;
        //    transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
        //    transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);

        //}
        //else
        //{
        //    arrival = true;
        //}
        //Debug.Log(this.transform.position.z);
        //Debug.Log(endPos.z);
        moveDistance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);
        
        if (this.transform.localPosition.x >= endPos.x - 1.0f && this.transform.localPosition.x <= endPos.x + 1.0 && this.transform.localPosition.z >= endPos.z - 1.0f && this.transform.localPosition.z <= endPos.z + 1.0f)
        {
            arrival = true;
        }
    }

    
}
