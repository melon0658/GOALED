using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class EventEndGame : MonoBehaviour
{

  private GameObject endGameObjects;

  private MakePlayerPrefab makePlayerPrefabScript;

  private Player winner;
  private GameObject winnerPlayerParentObj;
  private Material[] winnerPlayerObj;
  private GameObject cylinder;
  private GameObject canvas_end;
  private TextMeshProUGUI moneyText;
  private GameObject rogo;
  private GameObject endButton;
  private Transform moneyTransform;
  private Animator anim;

  private GameObject car1;
  private GameObject car2;
  private GameObject car3;
  private GameObject car4;
  private GameObject[] cars;

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

    cylinder = GameObject.Find("Cylinder");
    cylinder.SetActive(false);

    canvas_end = GameObject.Find("Canvas_end");
    rogo = GameObject.Find("Rogo");
    endButton = GameObject.Find("EndButton");

    moneyText = GameObject.Find("MoneyText_End").GetComponent<TextMeshProUGUI>();
    moneyTransform = GameObject.Find("MoneyTrigger").GetComponent<Transform>();

    rogo.SetActive(false);
    endButton.SetActive(false);
    canvas_end.SetActive(false);

    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    baseBGM = GameObject.Find("Audio Source").GetComponent<AudioSource>();

    spotLightEffect = GameObject.Find("SpotLightEffect").GetComponent<AudioSource>();

    winnerBGM = GameObject.Find("WinnerBGM").GetComponent<AudioSource>();

    endGameObjects = GameObject.Find("EndGameObjects");
    endGameObjects.SetActive(false);
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

    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight2").gameObject.SetActive(true);
    spotLightEffect.PlayOneShot(spotLightEffect.clip);

    //1秒まつ
    yield return new WaitForSeconds(1f);

    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight3").gameObject.SetActive(true);
    spotLightEffect.PlayOneShot(spotLightEffect.clip);

    //1秒まつ
    yield return new WaitForSeconds(1f);

    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight1").gameObject.SetActive(true);
    spotLightEffect.PlayOneShot(spotLightEffect.clip);

    //所持金表示 + You are a Millionaire !!! or GOALED !!!
    canvas_end.SetActive(true);
    rogo.SetActive(true);
    moneyText.text = winner.Money.ToString("N0") + "$";
    for (float i = 0.01f; i <= 0.35f; i += 0.01f)
    {
      rogo.transform.localScale = new Vector3(i, i, 1f);
      yield return new WaitForSeconds(0.05f);
    }

    //エモート
    anim.SetBool("onWinAnimation", true);
    yield return new WaitForSeconds(0.6f);
    cylinder.SetActive(true);

    //お金が降ってくる演出
    Money_Event.instance.lostMoney(moneyTransform, 2000000000);

    //BGMスタート
    //winnerBGM.PlayOneShot(winnerBGM.clip);
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
    for(int i = 0; i < 4; i++)
    {
      if(i >= makePlayerPrefabScript.GetPlayerNum()){
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
    //配列の回数分回す
    for (int i = 1; i < players.Length; i++)
    {
      Player playerScript = players[i].GetComponent<Player>();
      //配列の回数分回す
      if (winner.Money < playerScript.Money)
      {
        winner = playerScript;
      }
    }
  }


  public void ClickedEndButton()
  {
    //SceneManager.LoadScene("RoomDetailScene");

    SceneManager.LoadScene("OffLineTitleScene");
  }
}
