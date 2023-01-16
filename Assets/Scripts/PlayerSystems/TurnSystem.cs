using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターン処理を管理するスクリプト
public class TurnSystem : MonoBehaviour
{
  //各プレイヤーの車
  public GameObject player1;
  public GameObject player2;
  public GameObject player3;
  public GameObject player4;

  private GameObject[] Players;

  //各プレイヤーの基本マテリアル
  public Material player1BaseMaterial;
  public Material player2BaseMaterial;
  public Material player3BaseMaterial;
  public Material player4BaseMaterial;

  private Material[] BaseMaterials;

  //各プレイヤーの透過マテリアル
  public Material player1ClearMaterial;
  public Material player2ClearMaterial;
  public Material player3ClearMaterial;
  public Material player4ClearMaterial;

  private Material[] ClearMaterials;

  //各プレイヤーのカメラ
  private GameObject playerCamera1;
  private GameObject playerCamera2;
  private GameObject playerCamera3;
  private GameObject playerCamera4;

  private GameObject[] PlayerCameras;

  private GameObject upButton;
  private GameObject rightButton;
  private GameObject leftButton;


  private MakePlayerPrefab makePlayerPrefabScript;
  private EventEndGame eventEndGameScript;
  private MoneyUpdate moneyUpdateScript;
  private EventPayDay eventPayDayScript;
  private AudioSource payDayEffect;
  public PlayerStatusUI playerStatusUIScript;
  

  //切り替えるオブジェクト
  private ClickLeftRight gameScripts;
  private Roulette rScript;
  private Action actionScript;
  private EventSystem eventSystemScript;


  //現在の操作プレイヤーが誰かを保存
  private int nowTurnPlayerNum = 0;

  //ゴールした人数を保存
  private int goalPlayerNum = 0;

  private int originalMoney = 0;

  public int GetnowTurnPlayerNum()
  {
    return nowTurnPlayerNum;
  }

  public void SetnowTurnPlayerNum(int nowTurnPlayerNum)
  {
    this.nowTurnPlayerNum = nowTurnPlayerNum;
  }

  public int GetgoalPlayerNum()
  {
    return goalPlayerNum;
  }

  public void SetgoalPlayerNum(int goalPlayerNum)
  {
    this.goalPlayerNum = goalPlayerNum;
  }

  // Start is called before the first frame update
  void Start()
  {
    //ライトを消す(デバッグ用)
    //GameObject.Find("StageObjects").transform.Find("Directional Light").gameObject.SetActive(false);
    //RenderSettings.ambientIntensity = 0.5f;

    //真っ直ぐ進むボタン(ルート分岐)を取得して非表示
    upButton = GameObject.Find("Button Up");
    upButton.SetActive(false);

    //右に進むボタン(ルート分岐)を取得して非表示
    rightButton = GameObject.Find("Button Right");
    rightButton.SetActive(false);

    //左に進むボタン(ルート分岐)を取得して非表示
    leftButton = GameObject.Find("Button Left");
    leftButton.SetActive(false);

    //プレイヤー生成スクリプトを取得
    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    //ルート分岐ボタン表示スクリプトを取得
    gameScripts = GameObject.Find("GameScripts").GetComponent<ClickLeftRight>();

    //ルーレット処理スクリプトを取得
    rScript = GameObject.Find("ButtonStop").GetComponent<Roulette>();

    //イベント発生管理スクリプトを取得
    eventSystemScript = GameObject.Find("EventScripts").GetComponent<EventSystem>();

    //ゴール後イベントスクリプトを取得
    eventEndGameScript = GameObject.Find("EventScripts").GetComponent<EventEndGame>();

    //所持金UIの切り替えスクリプトを取得
    moneyUpdateScript = GameObject.Find("MoneyUIBox").GetComponent<MoneyUpdate>();

    //給料日イベントスクリプトを取得
    eventPayDayScript = GameObject.Find("EventScripts").GetComponent<EventPayDay>();

    //給料日イベント用効果音を取得
    payDayEffect = GameObject.Find("PaydayObjects").GetComponent<AudioSource>();

    //プレイヤー配列(気が向いたらfor文でコード短縮化実装)
    Players = new GameObject[] { player1, player2, player3, player4 };
    PlayerCameras = new GameObject[] { playerCamera1, playerCamera2, playerCamera3, playerCamera4 };
    BaseMaterials = new Material[] { player1BaseMaterial, player2BaseMaterial, player3BaseMaterial, player4BaseMaterial };
    ClearMaterials = new Material[] { player1ClearMaterial, player2ClearMaterial, player3ClearMaterial, player4ClearMaterial };


    for (int i=0;i < makePlayerPrefabScript.GetPlayerNum(); i++)
    {
      Players[i] = GameObject.Find("Player" + (i+1));

      PlayerCameras[i] = Players[i].transform.Find("PlayerCamera").gameObject;
      PlayerCameras[i].SetActive(false);

      Players[i].GetComponent<Renderer>().material = ClearMaterials[i];
      Players[i].GetComponent<BoxCollider>().enabled = false;
    }

    
    //playerCamera1 = GameObject.Find("PlayerCamera");
    //playerCamera1.SetActive(false);
    //playerCamera2 = GameObject.Find("PlayerCamera");
    //playerCamera2.SetActive(false);
    //playerCamera3 = GameObject.Find("PlayerCamera");
    //playerCamera3.SetActive(false);
    //playerCamera4 = GameObject.Find("PlayerCamera");
    //playerCamera4.SetActive(false);

    //player1.GetComponent<Renderer>().material = player1ClearMaterial;
    //player2.GetComponent<Renderer>().material = player2ClearMaterial;
    //player3.GetComponent<Renderer>().material = player3ClearMaterial;
    //player4.GetComponent<Renderer>().material = player4ClearMaterial;

    //player1.GetComponent<BoxCollider>().enabled = false;
    //player2.GetComponent<BoxCollider>().enabled = false;
    //player3.GetComponent<BoxCollider>().enabled = false;
    //player4.GetComponent<BoxCollider>().enabled = false;

    nowTurnPlayerNum = 1;

    TurnStartSystemMaster();
  }

  void OnPlayerCamera()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        playerCamera1.SetActive(true);
        break;
      case 2:
        playerCamera2.SetActive(true);
        break;
      case 3:
        playerCamera3.SetActive(true);
        break;
      case 4:
        playerCamera4.SetActive(true);
        break;
      default:
        break;
    }
  }

  void OffPlayerCamera()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        playerCamera1.SetActive(false);
        break;
      case 2:
        playerCamera2.SetActive(false);
        break;
      case 3:
        playerCamera3.SetActive(false);
        break;
      case 4:
        playerCamera4.SetActive(false);
        break;
      default:
        break;
    }
  }

  //ターンの切り替え
  void ChangeNowPlayer()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        player1.GetComponent<Renderer>().material = player1ClearMaterial;
        player1.GetComponent<BoxCollider>().enabled = false;
        SetnowTurnPlayerNum(2);
        break;
      case 2:
        player2.GetComponent<Renderer>().material = player2ClearMaterial;
        player2.GetComponent<BoxCollider>().enabled = false;
        SetnowTurnPlayerNum(3);
        break;
      case 3:
        player3.GetComponent<Renderer>().material = player3ClearMaterial;
        player3.GetComponent<BoxCollider>().enabled = false;
        SetnowTurnPlayerNum(4);
        break;
      case 4:
        player4.GetComponent<Renderer>().material = player4ClearMaterial;
        player4.GetComponent<BoxCollider>().enabled = false;
        SetnowTurnPlayerNum(1);
        break;
      default:
        break;
    }
  }

  void SwitchRouteSelectButton()
  {
    if (actionScript.GetCheckPoint())
    {
      if (actionScript.GetCheckPointName() == "CheckPosition")
      {
        Debug.Log("CheckPosition");
        rightButton.SetActive(true);
        leftButton.SetActive(true);
      }
      else if (actionScript.GetCheckPointName() == "CheckPosition2" || actionScript.GetCheckPointName() == "CheckPosition3")
      {
        Debug.Log("CheckPosition2orCheckPosition3");
        leftButton.SetActive(true);
        upButton.SetActive(true);
      }
      else if (actionScript.GetCheckPointName() == "GoalPosition")
      {
        switch (nowTurnPlayerNum)
        {
          case 1:
            if (player1.GetComponent<Player>().CheckGoal)
            {
              rScript.PowerBarStart();
            }
            else
            {
              player1.GetComponent<Player>().CheckGoal = true;
              rScript.PowerBarStart();
            }
            break;
          case 2:
            if (player2.GetComponent<Player>().CheckGoal)
            {
              rScript.PowerBarStart();
            }
            else
            {
              player2.GetComponent<Player>().CheckGoal = true;
              rScript.PowerBarStart();
            }
            break;
          case 3:
            if (player3.GetComponent<Player>().CheckGoal)
            {
              rScript.PowerBarStart();
            }
            else
            {
              player3.GetComponent<Player>().CheckGoal = true;
              rScript.PowerBarStart();
            }
            break;
          case 4:
            if (player4.GetComponent<Player>().CheckGoal)
            {
              rScript.PowerBarStart();
            }
            else
            {
              player4.GetComponent<Player>().CheckGoal = true;
              rScript.PowerBarStart();
            }
            break;
        }
        Debug.Log("Goal");
      }
    }
    else
    {
      Debug.Log("No");
      rScript.PowerBarStart();
    }
  }

  //各プレーヤーのスクリプト・車に切り替え
  void SetObjects()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        gameScripts.car1 = player1;
        gameScripts.mbScript = player1.GetComponent<MovementBaseScript>();
        gameScripts.actionScript = player1.GetComponent<Action>();
        rScript.car1 = player1;
        rScript.mbScript = player1.GetComponent<MovementBaseScript>();
        rScript.actionScript = player1.GetComponent<Action>();

        actionScript = player1.GetComponent<Action>();

        eventSystemScript.playerScript = player1.GetComponent<Player>();

        player1.GetComponent<Renderer>().material = player1BaseMaterial;
        player1.GetComponent<BoxCollider>().enabled = true;

        originalMoney = player1.GetComponent<Player>().Money;
        break;
      case 2:
        gameScripts.car1 = player2;
        gameScripts.mbScript = player2.GetComponent<MovementBaseScript>();
        gameScripts.actionScript = player2.GetComponent<Action>();
        rScript.car1 = player2;
        rScript.mbScript = player2.GetComponent<MovementBaseScript>();
        rScript.actionScript = player2.GetComponent<Action>();

        actionScript = player2.GetComponent<Action>();

        eventSystemScript.playerScript = player2.GetComponent<Player>();

        player2.GetComponent<Renderer>().material = player2BaseMaterial;
        player2.GetComponent<BoxCollider>().enabled = true;

        originalMoney = player2.GetComponent<Player>().Money;
        break;
      case 3:
        gameScripts.car1 = player3;
        gameScripts.mbScript = player3.GetComponent<MovementBaseScript>();
        gameScripts.actionScript = player3.GetComponent<Action>();
        rScript.car1 = player3;
        rScript.mbScript = player3.GetComponent<MovementBaseScript>();
        rScript.actionScript = player3.GetComponent<Action>();

        actionScript = player3.GetComponent<Action>();

        eventSystemScript.playerScript = player3.GetComponent<Player>();

        player3.GetComponent<Renderer>().material = player3BaseMaterial;
        player3.GetComponent<BoxCollider>().enabled = true;

        originalMoney = player3.GetComponent<Player>().Money;
        break;
      case 4:
        gameScripts.car1 = player4;
        gameScripts.mbScript = player4.GetComponent<MovementBaseScript>();
        gameScripts.actionScript = player4.GetComponent<Action>();
        rScript.car1 = player4;
        rScript.mbScript = player4.GetComponent<MovementBaseScript>();
        rScript.actionScript = player4.GetComponent<Action>();

        actionScript = player4.GetComponent<Action>();

        eventSystemScript.playerScript = player4.GetComponent<Player>();

        player4.GetComponent<Renderer>().material = player4BaseMaterial;
        player4.GetComponent<BoxCollider>().enabled = true;

        originalMoney = player4.GetComponent<Player>().Money;
        break;
      default:
        break;
    }
  }

  private void JudgeOnSound()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        if (player1.GetComponent<Player>().Money != originalMoney)
        {
          payDayEffect.PlayOneShot(payDayEffect.clip);
        }
        break;
      case 2:
        if (player2.GetComponent<Player>().Money != originalMoney)
        {
          payDayEffect.PlayOneShot(payDayEffect.clip);
        }
        break;
      case 3:
        if (player3.GetComponent<Player>().Money != originalMoney)
        {
          payDayEffect.PlayOneShot(payDayEffect.clip);
        }
        break;
      case 4:
        if (player4.GetComponent<Player>().Money != originalMoney)
        {
          payDayEffect.PlayOneShot(payDayEffect.clip);
        }
        break;
      default:
        break;
    }
    
  }
  public void CheckPayDay()
  {
    switch (nowTurnPlayerNum)
    {
      case 1:
        if(player1.GetComponent<Player>().PayDayCount != 0)
        {
          eventPayDayScript.execution();
        }
        else
        {
          LastProcessing();
        }
        break;
      case 2:
        if (player2.GetComponent<Player>().PayDayCount != 0)
        {
          eventPayDayScript.execution();
        }
        else
        {
          LastProcessing();
        }
        break;
      case 3:
        if (player3.GetComponent<Player>().PayDayCount != 0)
        {
          eventPayDayScript.execution();
        }
        else
        {
          LastProcessing();
        }
        break;
      case 4:
        if (player4.GetComponent<Player>().PayDayCount != 0)
        {
          eventPayDayScript.execution();
        }
        else
        {
          LastProcessing();
        }
        break;
      default:
        break;
    }
  }

  public void LastProcessing()
  {
    playerStatusUIScript.UpdatePlayersStatus();
    //4人全員ゴールしているか判定
    if (!CheckEndGame())
    {
      OffPlayerCamera();
      ChangeNowPlayer();
      TurnStartSystemMaster();
    }
    else
    {
      OffPlayerCamera();
      eventEndGameScript.EndGame();
    }
  }

  bool CheckEndGame()
  {
    if(goalPlayerNum == 4)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public void TurnStartSystemMaster()
  {
    Debug.Log("Start");
    OnPlayerCamera();

    //Debug.Log("OK OnPlayerCamera");
    SetObjects();
    //Debug.Log("OK SetObjects");
    moneyUpdateScript.UpdateMoneyText();

    SwitchRouteSelectButton();
    //Debug.Log("OK SwitchRouteSelectButton");
  }

  public void TurnEndSystemMaster()
  {
    playerStatusUIScript.UpdatePlayersStatus();
    moneyUpdateScript.UpdateMoneyText();

    JudgeOnSound();

    StartCoroutine("sleep");

    ////4人全員ゴールしているか判定
    //if (!CheckEndGame())
    //{

    //}
    //else
    //{
    //  playerStatusUIScript.UpdatePlayersStatus();
    //  moneyUpdateScript.UpdateMoneyText();
    //  CheckPayDay();
    //}
  }
  private IEnumerator sleep()
  {
    //イベント固有

    yield return new WaitForSeconds(1f);  //10秒待つ

    CheckPayDay();
  }
}
