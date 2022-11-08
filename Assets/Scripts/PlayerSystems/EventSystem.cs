using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
  public Player playerScript;
  public GameObject eventScriptsMaster;

  //各イベントのスクリプト
  #region
  public Event1 event1Script;
  public Event2 event2Script;

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

  public void EventExecutionManager()
  {
    int nowPosIndex = playerScript.NowPosIndex;

    //車があるマスのイベントを実行
    switch (nowPosIndex)
    {
      case 1: event1Script.execution(); break;
      case 2: event2Script.execution(); break;
      case 3: event1Script.execution(); break;
      case 4: event2Script.execution(); break;
      case 5: event1Script.execution(); break;
      case 6: event2Script.execution(); break;
      case 7: event1Script.execution(); break;
      case 8: event2Script.execution(); break;
      case 9: event1Script.execution(); break;
      case 10: event2Script.execution(); break;
      case 11: event1Script.execution(); break;
      case 12: event2Script.execution(); break;
      case 13: event1Script.execution(); break;
      case 14: event2Script.execution(); break;
      case 15: event1Script.execution(); break;
      case 16: event2Script.execution(); break;
      case 17: event1Script.execution(); break;
      case 18: event2Script.execution(); break;
      case 19: event1Script.execution(); break;
      case 20: event2Script.execution(); break;
      case 21: event1Script.execution(); break;
      case 22: event2Script.execution(); break;
      case 23: event1Script.execution(); break;
      case 24: event2Script.execution(); break;
      case 25: event1Script.execution(); break;
      case 26: event2Script.execution(); break;
      case 27: event1Script.execution(); break;
      case 28: event2Script.execution(); break;
      case 29: event1Script.execution(); break;
      case 30: event2Script.execution(); break;
      case 31: event1Script.execution(); break;
      case 32: event2Script.execution(); break;
      case 33: event1Script.execution(); break;
      case 34: event2Script.execution(); break;
      case 35: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      case 37: event1Script.execution(); break;
      case 38: event2Script.execution(); break;
      case 39: event1Script.execution(); break;
      case 40: event2Script.execution(); break;
      case 41: event1Script.execution(); break;
      case 42: event2Script.execution(); break;
      case 43: event1Script.execution(); break;
      case 44: event2Script.execution(); break;
      case 45: event1Script.execution(); break;
      case 46: event2Script.execution(); break;
      case 47: event1Script.execution(); break;
      case 48: event2Script.execution(); break;
      case 49: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      case 35: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      case 35: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      case 35: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      case 35: event1Script.execution(); break;
      case 36: event2Script.execution(); break;
      default: break;
    }

  }
}
