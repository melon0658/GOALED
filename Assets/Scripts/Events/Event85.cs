using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event85 : MonoBehaviour
{
  private TurnSystem turnSystemScript;

  // Start is called before the first frame update
  void Start()
  {
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
  }

  // Update is called once per frame
  void Update()
  {
        
  }
  public void execution()
  {
    //イベント固有

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
}
