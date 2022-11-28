using System.Collections;
using UnityEngine;
using TMPro;


public class Event70 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;


  //イベント固有
  private GameObject text; 
  private TextMeshProUGUI eventText;
  private PlayMovieVP pv;
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
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
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

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    int event_money = 60000;
    textDialogManegerScript.SetdialogText("UFOの撮影に成功 \n"+ event_money +"$");
    pv = canvas.transform.Find("EventVideo").GetComponent<PlayMovieVP>();
    pv.showVideoPlayer("67_UFO.mp4");
    StartCoroutine("sleep");

    
    playerScript.Money = playerScript.Money+event_money;
    Debug.Log(playerScript.Money);
  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(5); 
    Debug.Log("イベント終了");
    //text.SetActive(false);
    textDialogManegerScript.HiddentextDialogBox();

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}