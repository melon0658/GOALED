using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event82 : MonoBehaviour
{
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private TextMeshProUGUI countText;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;


  // Start is called before the first frame update
  void Start()
  {
    canvas = GameObject.Find("Canvas");

  }

  public void execution()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();

    //Debug.Log(turnSystemScript.GetnowTurnPlayerNum());
    //現在のターンが誰かを取得して、それに応じてプレイヤースクリプトを取得
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        break;
      default:
        break;
    }
    //Debug.Log(playerScript);
    //イベント固有

    if (playerScript.CheckGoal)
    {
      //既にゴールしているならルーレットのマス目×1000ドル所持金プラス
      countText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
      int count = int.Parse(countText.text);
      playerScript.Money = playerScript.Money + (count * 1000);
      //テキスト表示
      //Debug.Log("A");

    }
    else
    {
      playerScript.CheckGoal = true;
      //ついた順番に応じて所持金プラス
      switch (turnSystemScript.GetgoalPlayerNum())
      {
        case 0:
          playerScript.Money = playerScript.Money + 100000;
          break;
        case 1:
          playerScript.Money = playerScript.Money + 80000;
          break;
        case 2:
          playerScript.Money = playerScript.Money + 50000;
          break;
        case 3:
          playerScript.Money = playerScript.Money + 10000;
          break;
        default:
          break;
      }
      //ゴールした人数を増やす
      int goalPlayerNum = turnSystemScript.GetgoalPlayerNum() + 1;
      turnSystemScript.SetgoalPlayerNum(goalPlayerNum);
      //テキスト表示
      //Debug.Log("B");

    }

    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("ここにイベントテキストを貼り付け"); 

    textDialogManegerScript.HiddentextDialogBox();

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
}

