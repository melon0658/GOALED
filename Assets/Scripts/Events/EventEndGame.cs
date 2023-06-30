using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class EventEndGame : MonoBehaviour
{
  //ゴール後演出に使うオブジェクト全てをまとめたオブジェクト
  private GameObject endGameObjects;

  //スクリプト
  private MakePlayerPrefab makePlayerPrefabScript;

  //勝者関係
  private Player winner;
  private GameObject winnerPlayerParentObj;
  private Material[] winnerPlayerObj;

  //アニメーション関係
  private Animator anim;
  private Vector3 winnersPodiumPos;
  private Vector3 velocity = Vector3.zero;
  private float time = 0.8F;
  private bool isAnimationStarted = false;

  //UI関係
  private GameObject canvas_end;
  private GameObject title;

  //金額表示
  private TextMeshProUGUI moneyText;
  private Transform moneyTransform;

  //勝者の後ろに置く車
  private GameObject car1;
  private GameObject car2;
  private GameObject car3;
  private GameObject car4;
  private GameObject[] cars;

  //勝者の後ろに置くプレイヤー
  private GameObject char1;
  private GameObject char2;
  private GameObject char3;
  private GameObject char4;
  private GameObject[] chars;

  //スポットライトの効果音
  private AudioSource spotLightEffect;

  //ゴール後イベントのBGM
  private AudioSource winnerBGM;

  //通常のBGM
  private AudioSource baseBGM;

  //終了ボタン
  private GameObject endButton;

  //お金降らす用の変数
  private bool onMoneyFall = false;
  private float timer = 0f;


  void Start()
  {
    car1 = GameObject.Find("car1");
    car2 = GameObject.Find("car2");
    car3 = GameObject.Find("car3");
    car4 = GameObject.Find("car4");
    cars = new GameObject[] { car1, car2, car3, car4 };

    char1 = GameObject.Find("char1");
    char2 = GameObject.Find("char2");
    char3 = GameObject.Find("char3");
    char4 = GameObject.Find("char4");
    chars = new GameObject[] { char1, char2, char3, char4 };

    //外部スクリプト設定
    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    //ゴール時金額のUI関連の設定
    moneyText = GameObject.Find("MoneyText_End").GetComponent<TextMeshProUGUI>();
    moneyTransform = GameObject.Find("MoneyTrigger").GetComponent<Transform>();

    //ゴール時のUI関連の設定
    title = GameObject.Find("Title");
    canvas_end = GameObject.Find("Canvas_end");
    endButton = GameObject.Find("EndButton");
    title.SetActive(false);
    canvas_end.SetActive(false);
    endButton.SetActive(false);

    //BGM・効果音関連の設定
    baseBGM = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    spotLightEffect = GameObject.Find("SpotLightEffect").GetComponent<AudioSource>();
    winnerBGM = GameObject.Find("WinnerBGM").GetComponent<AudioSource>();

    //アニメーション関連の設定
    winnersPodiumPos = GameObject.Find("Cylinder").GetComponent<Transform>().position;
    winnersPodiumPos.y += 9.0f;

    //ゴールイベント関係のオブジェクトを全て非アクティブ化
    endGameObjects = GameObject.Find("EndGameObjects");
    endGameObjects.SetActive(false);
  }


  void Update()
  {
    //お金をふらす処理を定期的に実行
    if (onMoneyFall)
    {
      timer += Time.deltaTime;

      if (timer >= 3f)
      {
        Money_Event.instance.lostMoney(moneyTransform, 2000000);
        timer = 0f; // タイマーリセット
      }
    }

    //落下アニメーション処理
    if (isAnimationStarted)
    {
      winnerPlayerParentObj.GetComponent<Transform>().position = Vector3.SmoothDamp(winnerPlayerParentObj.GetComponent<Transform>().position, winnersPodiumPos, ref velocity, time);
      if (winnerPlayerParentObj.GetComponent<Transform>().position.y - 39.0f < 0.01f)
      {
        isAnimationStarted = false;
      }
      Debug.Log("aa");
    }
  }


  public void EndGame()
  {
    //ゴール後演出用のオブジェクトをアクティブ化
    endGameObjects.SetActive(true);

    //イベントスタート
    StartCoroutine("StartEvevt");
    
  }


  private IEnumerator StartEvevt()
  {
    //通常BGMを消す
    baseBGM.enabled = false;

    //勝者を計算
    Calc();

    //いらないオブジェクト非表示(ライトとかカメラとかUIとか)
    OffObjects();

    //必要なオブジェクト表示(Prefabとか)
    OnObjects();

    //1秒まつ
    yield return new WaitForSeconds(1f);

    //スポットライト照射
    GameObject.Find("EndGameObjects").transform.Find("SpotLight1").gameObject.SetActive(true);
    spotLightEffect.PlayOneShot(spotLightEffect.clip);
    yield return new WaitForSeconds(1f);
    GameObject.Find("EndGameObjects").transform.Find("SpotLight2").gameObject.SetActive(true);
    spotLightEffect.PlayOneShot(spotLightEffect.clip);

    //エモート&落下
    anim.SetBool("onWinAnimation", true);
    yield return new WaitForSeconds(1f);
    isAnimationStarted = true;
    yield return new WaitForSeconds(1f);

    //お金が降ってくる演出ON
    Money_Event.instance.lostMoney(moneyTransform, 2000000);
    onMoneyFall = true;

    //所持金表示 + You are a Millionaire !!! or GOALED !!!
    canvas_end.SetActive(true);
    title.SetActive(true);
    moneyText.text = winner.Money.ToString("N0") + "$";
    for (float i = 0.01f; i <= 0.5f; i += 0.02f)
    {
      title.transform.localScale = new Vector3(i, i, 1f);
      yield return new WaitForSeconds(0.05f);
    }

    //BGMスタート
    winnerBGM.Play();

    //終了ボタンを表示
    yield return new WaitForSeconds(2.0f);
    endButton.SetActive(true);
  }


  void OffObjects()
  {
    //ライトを消す
    GameObject.Find("StageObjects").transform.Find("Directional Light").gameObject.SetActive(false);
    RenderSettings.ambientIntensity = 0.3f;

    //ルーレットカメラ消す
    GameObject.Find(" RouletteCamera").SetActive(false);

    //Canvas消す
    GameObject.Find("Canvas").SetActive(false);

    //車とキャラクターの数がプレイヤー数と一致するようにする
    for (int i = 0; i < 4; i++)
    {
      if (i >= makePlayerPrefabScript.GetPlayerNum())
      {
        cars[i].SetActive(false);
        chars[i].SetActive(false);
      }
    }
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
        GameObject.Find("char1").SetActive(false);
        break;
      case "Blue":
        winnerPlayerObj[0].color = Color.blue;
        winnerPlayerObj[4].color = Color.blue;
        GameObject.Find("char2").SetActive(false);
        break;
      case "Red":
        winnerPlayerObj[0].color = Color.red;
        winnerPlayerObj[4].color = Color.red;
        GameObject.Find("char3").SetActive(false);
        break;
      case "Yellow":
        winnerPlayerObj[0].color = Color.yellow;
        winnerPlayerObj[4].color = Color.yellow;
        GameObject.Find("char4").SetActive(false);
        break;
      default:
        break;
    }

  }


  void Calc()
  {
    GameObject[] players = makePlayerPrefabScript.GetPlayers();
    winner = players[0].GetComponent<Player>();

    //配列の回数分回して、所持金が一番多いプレイヤーを勝者とする
    for (int i = 1; i < players.Length; i++)
    {
      Player playerScript = players[i].GetComponent<Player>();
      //
      if (winner.Money < playerScript.Money)
      {
        winner = playerScript;
      }
      else if(winner.Money == playerScript.Money)
      {
        //同金額者がいた場合は、先にゴールした人を勝者とする
        if (winner.GoalNum > playerScript.GoalNum)
        {
          winner = playerScript;
        }
      }
    }
  }


  public void ClickedEndButton()
  {
    //SceneManager.LoadScene("RoomDetailScene");

    //SceneManager.MoveGameObjectToScene(GameObject.Find("SendPlayerData"), SceneManager.GetActiveScene());

    //GameObject.Find("SendPlayerData").SetActive(false);

    SceneManager.LoadScene("OffLineTitleScene");
  }
}
