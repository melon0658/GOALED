using System.Collections;
using UnityEngine;
using TMPro;


public class Event23 : MonoBehaviour
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
    GameObject Player = makePlayerPrefabScript.GetPlayers()[nowTrunPlayerNum];

    //プレイヤースクリプトを取得
    playerScript = Player.GetComponent<Player>();

    //どのイベントにも必要なやつここまで

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();

    textDialogManegerScript.SetdialogText("婚約指輪購入\n給料分の金額を支払う");

    switch (playerScript.Job)
    {
      case "プロゲーマー":
        playerScript.Money =playerScript.Money - 20000;
        
        break;
      case "エンジニア":
        playerScript.Money =playerScript.Money - 25000;
        
        break;
      case "作家":
        playerScript.Money =playerScript.Money - 30000;
        
        break;
      case "アスリート":
        playerScript.Money =playerScript.Money - 35000;
        
        break;
      case "パイロット":
        playerScript.Money =playerScript.Money - 40000;
        
        break;
      case "パティシエ":
        playerScript.Money =playerScript.Money - 45000;
        
        break;
      case "科学者":
        playerScript.Money =playerScript.Money - 50000;
        
        break;
      case "俳優":
        playerScript.Money =playerScript.Money - 55000;
        
        break;
      case "建築家":
        playerScript.Money =playerScript.Money - 60000;
        
        break;
      case "弁護士":
        playerScript.Money =playerScript.Money - 65000;
        
        break;
      case "医者":
        playerScript.Money =playerScript.Money - 70000;
        
        break;
      case "無し":
        playerScript.Money =playerScript.Money - 10000;
        
        break;
      default:
        playerScript.Money =playerScript.Money - 10000;
        break;
    }
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