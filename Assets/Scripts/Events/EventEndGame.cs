using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventEndGame : MonoBehaviour
{
  public Player player1;
  public Player player2;
  public Player player3;
  public Player player4;

  private Player winner;
  private GameObject winnerPlayerParentObj;
  private Material[] winnerPlayerObj;
  private GameObject cylinder;
  private GameObject canvas_end;
  private TextMeshProUGUI moneyText;
  private GameObject rogo;
  private GameObject endButton;
  private Transform moneyTransform;
  private Animator anim;  //Animatorをanimという変数で定義する

  //デバッグ用
  private TurnSystem tsSctript;

  // Start is called before the first frame update
  void Start()
  {
    cylinder = GameObject.Find("Cylinder");
    cylinder.SetActive(false);
    canvas_end = GameObject.Find("Canvas_end");
    rogo = GameObject.Find("Rogo");
    endButton = GameObject.Find("EndButton");
    moneyText = GameObject.Find("MoneyText_End").GetComponent<TextMeshProUGUI>();
    rogo.SetActive(false);
    endButton.SetActive(false);
    canvas_end.SetActive(false);
    moneyTransform = GameObject.Find("MoneyTrigger").GetComponent<Transform>();
  }

  public void EndGame()
  {
    Debug.Log("The End");
    //金額計算して１位を決める

    //デバッグ用
    tsSctript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    tsSctript.OffPlayerCamera();

    //イベントスタート
    StartCoroutine("StartEvevt");
    
  }
  private IEnumerator StartEvevt()
  {
    //勝者を計算
    Calc();

    //いらないオブジェクト非表示(ライトとかカメラとかUIとか)
    OffObjects();

    //必要なオブジェクト表示(Prefabとか)
    OnObjects();

    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight1").gameObject.SetActive(true);
    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight2").gameObject.SetActive(true);
    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight3").gameObject.SetActive(true);

    //所持金表示 + You are a Millionaire !!! or GOALED !!!
    canvas_end.SetActive(true);
    rogo.SetActive(true);
    moneyText.text = "$ " + winner.Money.ToString("N0");
    for (float i = 0.01f; i <= 0.35f; i += 0.01f)
    {
      rogo.transform.localScale = new Vector3(i, i, 1f);
      yield return new WaitForSeconds(0.05f);
    }
    //エモート

    anim.SetBool("onWinAnimation", true);
    yield return new WaitForSeconds(0.6f);
    cylinder.SetActive(true);

    Money_Event.instance.lostMoney(moneyTransform, 2000000000);


    yield return new WaitForSeconds(2.0f);
    endButton.SetActive(true);
    ////どのイベントにも必要なやつ
    //turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
  void OffObjects()
  {
    //ライトを消す
    GameObject.Find("StageObjects").transform.Find("Directional Light").gameObject.SetActive(false);
    RenderSettings.ambientIntensity = 0.3f;
    //ルーレットカメラ消す
    GameObject.Find(" RouletteCamera").SetActive(false);
    //バードカメラ消す
    //GameObject.Find("Bird's-eyeCamera").SetActive(false);
    //Canvas消す
    GameObject.Find("Canvas").SetActive(false);
  }
  void OnObjects()
  {
    //Prefab生成
    winnerPlayerParentObj = GameObject.Find("ch0");
    anim = winnerPlayerParentObj.GetComponent<Animator>();
    winnerPlayerObj = winnerPlayerParentObj.transform.Find("Body.002").GetComponent<SkinnedMeshRenderer>().materials;

    Debug.Log(winnerPlayerObj[0]);
    //優勝者を真ん中、残りのプレイヤーは周りに置く
    switch (winner.Color)
    {
      case "Green":
        winnerPlayerObj[0].color = Color.green;
        winnerPlayerObj[4].color = Color.green;
        GameObject.Find("ch1").SetActive(false);
        break;
      case "Blue":
        winnerPlayerObj[0].color = Color.blue;
        winnerPlayerObj[4].color = Color.blue;
        GameObject.Find("ch2").SetActive(false);
        break;
      case "Red":
        winnerPlayerObj[0].color = Color.red;
        winnerPlayerObj[4].color = Color.red;
        GameObject.Find("ch3").SetActive(false);
        break;
      case "Yellow":
        winnerPlayerObj[0].color = Color.yellow;
        winnerPlayerObj[4].color = Color.yellow;
        GameObject.Find("ch4").SetActive(false);
        break;
      default:
        break;
    }

  }
  void Calc()
  {
    Player[] players = new Player[] { player1, player2, player3, player4 };
    winner = players[0];
    //配列の回数分回す
    for (int i = 1; i < players.Length; i++)
    {
      //配列の回数分回す
      if(winner.Money < players[i].Money)
      {
        winner = players[i];
      }
    }
  }

  public void ClickedEndButton()
  {
    SceneManager.LoadScene("RoomDetailScene");
  }
}
