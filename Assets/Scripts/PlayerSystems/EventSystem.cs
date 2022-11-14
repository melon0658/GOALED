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
  public Event01 event1Script;
  public Event02 event2Script;

  public Event85 event85Script;
  #endregion

  void Start()
  {

  }

  public void EventExecutionManager()
  {
    int nowPosIndex = playerScript.NowPosIndex;

    //switch構文で書くしかない　ゴミ

    //車があるマスのイベントを実行
    switch (nowPosIndex)
    {
      case 1: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 2: eventScriptsMaster.GetComponent<Event02>().execution(); break;
      case 3: eventScriptsMaster.GetComponent<Event03>().execution(); break;
      case 4: eventScriptsMaster.GetComponent<Event04>().execution(); break;
      case 5: eventScriptsMaster.GetComponent<Event05>().execution(); break;
      case 6: eventScriptsMaster.GetComponent<Event06>().execution(); break;
      case 7: eventScriptsMaster.GetComponent<Event07>().execution(); break;
      case 8: eventScriptsMaster.GetComponent<Event08>().execution(); break;
      case 9: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 10: eventScriptsMaster.GetComponent<Event10>().execution(); break;
      case 11: eventScriptsMaster.GetComponent<Event11>().execution(); break;
      case 12: eventScriptsMaster.GetComponent<Event12>().execution(); break;
      case 13: eventScriptsMaster.GetComponent<Event13>().execution(); break;
      case 14: eventScriptsMaster.GetComponent<Event14>().execution(); break;
      case 15: eventScriptsMaster.GetComponent<Event15>().execution(); break;
      case 16: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 17: eventScriptsMaster.GetComponent<Event17>().execution(); break;
      case 18: eventScriptsMaster.GetComponent<Event18>().execution(); break;
      case 19: eventScriptsMaster.GetComponent<Event19>().execution(); break;
      case 20: eventScriptsMaster.GetComponent<Event20>().execution(); break;
      case 21: eventScriptsMaster.GetComponent<Event21>().execution(); break;
      case 22: eventScriptsMaster.GetComponent<Event22>().execution(); break;
      case 23: eventScriptsMaster.GetComponent<Event23>().execution(); break;
      case 24: eventScriptsMaster.GetComponent<Event24>().execution(); break;
      case 25: eventScriptsMaster.GetComponent<Event25>().execution(); break;
      case 26: eventScriptsMaster.GetComponent<Event26>().execution(); break;
      case 27: eventScriptsMaster.GetComponent<Event27>().execution(); break;
      case 28: eventScriptsMaster.GetComponent<Event28>().execution(); break;
      case 29: eventScriptsMaster.GetComponent<Event29>().execution(); break;
      case 30: eventScriptsMaster.GetComponent<Event30>().execution(); break;
      case 31: eventScriptsMaster.GetComponent<Event31>().execution(); break;
      case 32: eventScriptsMaster.GetComponent<Event32>().execution(); break;
      case 33: eventScriptsMaster.GetComponent<Event33>().execution(); break;
      case 34: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 35: eventScriptsMaster.GetComponent<Event35>().execution(); break;
      case 36: eventScriptsMaster.GetComponent<Event36>().execution(); break;
      case 37: eventScriptsMaster.GetComponent<Event37>().execution(); break;
      case 38: eventScriptsMaster.GetComponent<Event38>().execution(); break;
      case 39: eventScriptsMaster.GetComponent<Event39>().execution(); break;
      case 40: eventScriptsMaster.GetComponent<Event40>().execution(); break;
      case 41: eventScriptsMaster.GetComponent<Event41>().execution(); break;
      case 42: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 43: eventScriptsMaster.GetComponent<Event43>().execution(); break;
      case 44: eventScriptsMaster.GetComponent<Event44>().execution(); break;
      case 45: eventScriptsMaster.GetComponent<Event45>().execution(); break;
      case 46: eventScriptsMaster.GetComponent<Event46>().execution(); break;
      case 47: eventScriptsMaster.GetComponent<Event47>().execution(); break;
      case 48: eventScriptsMaster.GetComponent<Event48>().execution(); break;
      case 49: eventScriptsMaster.GetComponent<Event49>().execution(); break;
      case 50: eventScriptsMaster.GetComponent<Event50>().execution(); break;
      case 51: eventScriptsMaster.GetComponent<Event51>().execution(); break;
      case 52: eventScriptsMaster.GetComponent<Event52>().execution(); break;
      case 53: eventScriptsMaster.GetComponent<Event53>().execution(); break;
      case 54: eventScriptsMaster.GetComponent<Event54>().execution(); break;
      case 55: eventScriptsMaster.GetComponent<Event55>().execution(); break;
      case 56: eventScriptsMaster.GetComponent<Event56>().execution(); break;
      case 57: eventScriptsMaster.GetComponent<Event57>().execution(); break;
      case 58: eventScriptsMaster.GetComponent<Event58>().execution(); break;
      case 59: eventScriptsMaster.GetComponent<Event59>().execution(); break;
      case 60: eventScriptsMaster.GetComponent<Event60>().execution(); break;
      case 61: eventScriptsMaster.GetComponent<Event61>().execution(); break;
      case 62: eventScriptsMaster.GetComponent<Event62>().execution(); break;
      case 63: eventScriptsMaster.GetComponent<Event63>().execution(); break;
      case 64: eventScriptsMaster.GetComponent<Event64>().execution(); break;
      case 65: eventScriptsMaster.GetComponent<Event65>().execution(); break;
      case 66: eventScriptsMaster.GetComponent<Event66>().execution(); break;
      case 67: eventScriptsMaster.GetComponent<Event67>().execution(); break;
      case 68: eventScriptsMaster.GetComponent<Event68>().execution(); break;
      case 69: eventScriptsMaster.GetComponent<Event69>().execution(); break;
      case 70: eventScriptsMaster.GetComponent<Event70>().execution(); break;
      case 71: eventScriptsMaster.GetComponent<Event71>().execution(); break;
      case 72: eventScriptsMaster.GetComponent<Event72>().execution(); break;
      case 73: eventScriptsMaster.GetComponent<Event73>().execution(); break;
      case 74: eventScriptsMaster.GetComponent<Event74>().execution(); break;
      case 75: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 76: eventScriptsMaster.GetComponent<Event76>().execution(); break;
      case 77: eventScriptsMaster.GetComponent<Event77>().execution(); break;
      case 78: eventScriptsMaster.GetComponent<Event78>().execution(); break;
      case 79: eventScriptsMaster.GetComponent<Event79>().execution(); break;
      case 80: eventScriptsMaster.GetComponent<Event80>().execution(); break;
      case 81: eventScriptsMaster.GetComponent<Event81>().execution(); break;
      case 82: eventScriptsMaster.GetComponent<Event01>().execution(); break;
      case 83: eventScriptsMaster.GetComponent<Event83>().execution(); break;
      case 84: eventScriptsMaster.GetComponent<Event84>().execution(); break;
      case 85: eventScriptsMaster.GetComponent<Event85>().execution(); break;
      default: break;
    }

  }
}
