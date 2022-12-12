using System.Collections;
using UnityEngine;
using TMPro;

public class Event24 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private Player otherplayer1;
  private Player otherplayer2;
  private Player otherplayer3;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;


  //イベント固有
  private GameObject text; 
  private TextMeshProUGUI eventText;

  void Start()
  {
    //どのイベントにも必要なやつ
    canvas = GameObject.Find("Canvas");


    //イベント固有

  }

    // Update is called once per frame
  void Update()
  {
        
  }

  public void execution()
  {

    int marry_mony = 3000;
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    //現在のターンが誰かを取得して、それに応じてプレイヤースクリプトを取得
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        otherplayer1 = GameObject.Find("defaultCar2").GetComponent<Player>();
        otherplayer2 = GameObject.Find("defaultCar3").GetComponent<Player>();
        otherplayer3 = GameObject.Find("defaultCar4").GetComponent<Player>();

        playerScript.Money = playerScript.Money + (marry_mony*3);
        otherplayer1.Money = otherplayer1.Money - marry_mony;
        otherplayer2.Money = otherplayer2.Money - marry_mony;
        otherplayer3.Money = otherplayer3.Money - marry_mony;
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        otherplayer1 = GameObject.Find("defaultCar1").GetComponent<Player>();
        otherplayer2 = GameObject.Find("defaultCar3").GetComponent<Player>();
        otherplayer3 = GameObject.Find("defaultCar4").GetComponent<Player>();

        playerScript.Money = playerScript.Money + (marry_mony*3);
        otherplayer1.Money = otherplayer1.Money - marry_mony;
        otherplayer2.Money = otherplayer2.Money - marry_mony;
        otherplayer3.Money = otherplayer3.Money - marry_mony;
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        otherplayer1 = GameObject.Find("defaultCar2").GetComponent<Player>();
        otherplayer2 = GameObject.Find("defaultCar1").GetComponent<Player>();
        otherplayer3 = GameObject.Find("defaultCar4").GetComponent<Player>();

        playerScript.Money = playerScript.Money + (marry_mony*3);
        otherplayer1.Money = otherplayer1.Money - marry_mony;
        otherplayer2.Money = otherplayer2.Money - marry_mony;
        otherplayer3.Money = otherplayer3.Money - marry_mony;
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        otherplayer1 = GameObject.Find("defaultCar2").GetComponent<Player>();
        otherplayer2 = GameObject.Find("defaultCar3").GetComponent<Player>();
        otherplayer3 = GameObject.Find("defaultCar1").GetComponent<Player>();

        playerScript.Money = playerScript.Money + (marry_mony*3);
        otherplayer1.Money = otherplayer1.Money - marry_mony;
        otherplayer2.Money = otherplayer2.Money - marry_mony;
        otherplayer3.Money = otherplayer3.Money - marry_mony;
        break;
      default:
        break;
    }

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("結婚する\n全員からお祝い金(3000$x3)をもらう");
    playerScript.Spouse = true;
    StartCoroutine("sleep");

  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(1f);  //10秒待つ
    Debug.Log("イベント終了");
    //text.SetActive(false);
    textDialogManegerScript.HiddentextDialogBox();

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}