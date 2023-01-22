using System.Collections;
using UnityEngine;
using TMPro;

public class Event24 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private MakePlayerPrefab makePlayerPrefabScript;

  //プレイヤーを格納するための配列
  private GameObject[] players;

  private Player playerScript;
  private Player otherplayer;

  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;

  //イベント固有
  private GameObject text; 
  private TextMeshProUGUI eventText;

  void Start()
  {
    //どのイベントにも必要なやつ
    canvas = GameObject.Find("Canvas");

  }

  public void execution()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    //プレイヤー配列を取得
    players = makePlayerPrefabScript.GetPlayers();

    //現在のターンが誰かを取得
    int nowTrunPlayerNum = turnSystemScript.GetnowTurnPlayerNum();

    //結婚お祝い金
    int marry_mony = 3000;

    //結婚お祝い金の処理
    for (int i = 0; i < makePlayerPrefabScript.GetPlayerNum(); i++)
    {
      //現在のターンのプレイヤーだったら（お金を貰う側）
      if (i == nowTrunPlayerNum)
      {
        //プレイヤースクリプトを取得
        playerScript = players[i].GetComponent<Player>();

        playerScript.Money = playerScript.Money + (marry_mony * makePlayerPrefabScript.GetPlayerNum());
      }
      //それ以外のプレイヤーだったら（お金を払う側）
      else
      {
        otherplayer = players[i].GetComponent<Player>();

        otherplayer.Money = otherplayer.Money - marry_mony;
      }
    }

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("結婚する\n全員からお祝い金(3000$x" + makePlayerPrefabScript.GetPlayerNum() + ")をもらう");
    playerScript.Spouse = true;
    StartCoroutine("sleep");

  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(1f);  //1秒待つ
    Debug.Log("イベント終了");

    textDialogManegerScript.HiddentextDialogBox();

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}