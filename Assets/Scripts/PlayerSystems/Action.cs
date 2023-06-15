using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

  private bool checkPoint = true;

  private string checkPointName = "CheckPosition";

  public bool GetCheckPoint()
  {
    return checkPoint;
  }

  public void SetCheckPoint(bool checkPoint)
  {
    this.checkPoint = checkPoint;
  }


  public string GetCheckPointName()
  {
    return checkPointName;
  }


  void Start()
  {
    this.checkPointName = "CheckPosition";
  }


  // ゲームオブジェクト同士が接触したタイミングで実行
  void OnTriggerEnter(Collider other)
  {
    // 衝突した相手オブジェクトのタグが"CheckPoint"
    if (other.tag == "CheckPoint")
    {
      // 分岐判定をtrueにする
      checkPointName = other.name;
      checkPoint = true;
    }
  }
}
