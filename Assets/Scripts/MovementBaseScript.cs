using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseScript : MonoBehaviour
{
    //[SerializeField]
    private PathCreator pathCreator;

    public Roulette rCountScript;

    float speed = 1f;
    Vector3 endPos;

    float moveDistance;

    private bool arrival = false;

    //車が目的地についてるかの判定を返す
    public bool GetArrival()
    {
        return arrival;
    }
    public void SetPathCreator(PathCreator pc)
    {
        //使用するルートパスを指定
        this.pathCreator = pc;
        Debug.Log(pathCreator.path.NumPoints);


        int rCount = rCountScript.Rcount();
        Debug.Log(rCount);
        //60,53,46,
        endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
        //endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - (pathCreator.path.NumPoints / 10 * (11-rCount)) + 1);
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
    }

    void repeat()
    {
        AutoMove();

        //目的地に着いたらrepeatを止める
        if (this.GetArrival() == true)
        {
            CancelInvoke("repeat");
            Debug.Log("終わり");
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
        moveDistance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);
        
        if ((this.transform.position.x >= endPos.x - 0.4 && this.transform.position.x <= endPos.x + 0.4) && (this.transform.position.z >= endPos.z - 0.4 && this.transform.position.z <= endPos.z + 0.4))
        {
            arrival = true;
        }
    }

    
}
