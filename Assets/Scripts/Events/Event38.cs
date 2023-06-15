using System.Collections;
using UnityEngine;
using TMPro;

public class Event38 : MonoBehaviour
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
    textDialogManegerScript.SetdialogText("子供が生まれる");

    //子供の数を増やす
    playerScript.Child = playerScript.Child + 1;

    //子供1人目
    if (playerScript.Child == 0)
    {
      //子供を乗せる
      player.transform.Find("K1").gameObject.SetActive(true);
    }
    //子供2人目
    else if (playerScript.Child == 1)
    {
      //子供を乗せる
      player.transform.Find("K2").gameObject.SetActive(true);
    }

    StartCoroutine("sleep");

    Debug.Log(playerScript.Job);
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