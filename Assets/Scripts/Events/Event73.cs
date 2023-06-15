using System.Collections;
using UnityEngine;
using TMPro;

public class Event73 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private MakePlayerPrefab makePlayerPrefabScript;

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
    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    //現在のターンが誰かを取得
    int nowTrunPlayerNum = turnSystemScript.GetnowTurnPlayerNum();

    //それに応じてプレイヤーを取得
    GameObject player = makePlayerPrefabScript.GetPlayers()[nowTrunPlayerNum];

    //プレイヤースクリプトを取得
    playerScript = player.GetComponent<Player>();

    //どのイベントにも必要なやつここまで

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    int event_money = -10000;
    textDialogManegerScript.SetdialogText("交通事故に巻き込まれる \n"+ event_money +"$");
    pv = canvas.transform.Find("EventVideo").GetComponent<PlayMovieVP>();
    pv.showVideoPlayer("12_交通事故.mp4");


    StartCoroutine("sleep");
    playerScript.Money = playerScript.Money+event_money;
    Debug.Log(playerScript.Money);
  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(3);  //10秒待つ
    Debug.Log("イベント終了");
    //text.SetActive(false);
    textDialogManegerScript.HiddentextDialogBox();

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}