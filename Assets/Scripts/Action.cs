using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public string objName;

    private bool checkPoint = false;

    public bool GetCheckPoint()
    {
        return checkPoint;
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
        // もし衝突した相手オブジェクトの名前が"Cube"ならば
        if (other.tag == "CheckPoint")
        {
            // 衝突した相手オブジェクトのRendererコンポーネントのmaterialの色を黒に変更する
            Debug.Log("true");
            checkPoint = true;
        }
        else
        {
            Debug.Log("false");
            
            checkPoint = false;
            Debug.Log(checkPoint);
        }
    }

}
