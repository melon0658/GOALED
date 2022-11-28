using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventTest100 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;

  //イベント固有
  private MovementBaseScript moveScript;

  // Start is called before the first frame update
  void Start()
  {
    //どのイベントにも必要なやつ
    canvas = GameObject.Find("Canvas");
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void execution()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    //現在のターンが誰かを取得して、それに応じてプレイヤースクリプトを取得
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar1").GetComponent<MovementBaseScript>();
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar2").GetComponent<MovementBaseScript>();
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar3").GetComponent<MovementBaseScript>();
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar4").GetComponent<MovementBaseScript>();
        break;
      default:
        break;
    }

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("ジョブ決定");

    StartCoroutine("sleep");

    //playerのお金が取得して変更したい！(getもsetもこの書き方)
    //playerScript.Money = playerScript.Money + 10000;
  }

  private IEnumerator sleep()
  {
    //イベント固有
    yield return new WaitForSeconds(1f);  //1秒待つ
    textDialogManegerScript.HiddentextDialogBox();

    moveScript.jobEventAfterMove();

    //どのイベントにも必要なやつ
    //turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
}
