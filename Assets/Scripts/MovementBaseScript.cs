using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseScript : MonoBehaviour
{
    [SerializeField]
    PathCreator pathCreator;

    float speed = 1f;
    Vector3 endPos;

    float moveDistance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(pathCreator.path.NumPoints);
        endPos = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 10);
    }

    // Update is called once per frame
    void Update()
    {
        //moveDistance += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);
    }
    
    //目的地まで自動で移動
    public　void AutoMove()
    {
        //(int)this.transform.position.x != (int)endPos.x
        if ((int)this.transform.position.x != (int)endPos.x || (int)this.transform.position.z != (int)endPos.z)
        {
            Debug.Log(this.transform.position.z);
            Debug.Log(endPos.z);
            moveDistance += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(moveDistance, EndOfPathInstruction.Stop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(moveDistance, EndOfPathInstruction.Stop);
        }
    }
}
