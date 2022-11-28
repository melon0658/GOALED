using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
  public Player playerScript;
  public GameObject eventScriptsMaster;
  private TurnSystem turnSystemScript;

  //各イベントのスクリプト
  #region
  public Event1 event1Script;
  public Event2 event2Script;

  public Event85 event85Script;
  #endregion

  void Start()
  {

  }

  public void EventExecutionManager()
  {
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    int nowPosIndex = playerScript.NowPosIndex;

    //switch構文で書くしかない　ゴミ
    //eventScriptsMaster.GetComponent<EventEndGame>().EndGame();

    //車があるマスのイベントを実行
    switch (nowPosIndex)
    {
      case 1: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 2: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 3: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 4: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 5: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 6: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 7: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 8: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 9: turnSystemScript.CheckPayDay(); break;
      case 10: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 11: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 12: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 13: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 14: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 15: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 16: turnSystemScript.CheckPayDay(); break;
      case 17: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 18: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 19: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 20: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 21: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 22: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 23: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 24: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 25: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 26: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 27: eventScriptsMaster.GetComponent<EventTest100>().execution(); break;
      case 28: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 29: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 30: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 31: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 32: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 33: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 34: turnSystemScript.CheckPayDay(); break;
      case 35: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 36: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 37: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 38: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 39: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 40: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 41: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 42: turnSystemScript.CheckPayDay(); break;
      case 43: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 44: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 45: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 46: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 47: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 48: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 49: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 50: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 51: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 52: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 53: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 54: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 55: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 56: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 57: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 58: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 59: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 60: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 61: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 62: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 63: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 64: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 65: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 66: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 67: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 68: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 69: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 70: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 71: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 72: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 73: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 74: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 75: turnSystemScript.CheckPayDay(); break;
      case 76: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 77: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 78: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 79: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 80: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 81: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 82: turnSystemScript.CheckPayDay(); break;
      case 83: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      case 84: eventScriptsMaster.GetComponent<Event2>().execution(); break;
      case 85: eventScriptsMaster.GetComponent<Event1>().execution(); break;
      default: break;
    }

  }
}
