using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IventSystem : MonoBehaviour
{
  public Player playerScript;
  public GameObject iventScriptsMaster;

  //各イベントのスクリプト
  #region
  public Ivent1 ivent1Script;
  public Ivent2 ivent2Script;

  #endregion

  void Start()
  {
    
    //Ivents.Add(ivent1Script);
    //var tes = iventScriptsMaster.GetComponent<>;
    //Type aa = Ivents[0].GetType();
    //aa.GetMethod("execution");
    //SetIventScripts();
    //iventFunctions[] aa = new iventFunctions[3];
    //aa[0].function = ivent1Script.execution();
    //aa[1].function = ONO;
    //aa[2].function = YARI;


    //switch構文で書くしかない　ゴミ

  }

  // Update is called once per frame
  void Update()
  {
        
  }

  public void IventExecutionManager()
  {
    int nowPosIndex = playerScript.NowPosIndex;

    //車があるマスのイベントを実行
    switch (nowPosIndex)
    {
      case 1: ivent1Script.execution(); break;
      case 2: ivent2Script.execution(); break;

      default: break;
    }

  }
}
