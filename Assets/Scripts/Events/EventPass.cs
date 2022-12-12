using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPass : MonoBehaviour
{
  private TurnSystem turnSystemScript;
  public void execution()
  {
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    turnSystemScript.TurnEndSystemMaster(); //ƒ^[ƒ“‚ğI—¹
  }
}
