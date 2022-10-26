using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public string objName;

    private bool checkPoint = false;
    private string checkPointName;

    public bool GetCheckPoint()
    {
        return checkPoint;
    }

    public string GetCheckPointName()
    {
        return checkPointName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ゲームオブジェクト同士が接触したタイミングで実行
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        // 衝突した相手オブジェクトのタグが"CheckPoint"
        if (other.tag == "CheckPoint")
        {
            // 分岐判定をtrueにする
            Debug.Log("true");
            checkPointName = other.name;
            checkPoint = true;
        }
        else
        {
            Debug.Log("false");
            checkPoint = false;
        }
    }

}
