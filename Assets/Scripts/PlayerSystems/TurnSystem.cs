using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ターン処理を管理するスクリプト
public class TurnSystem : MonoBehaviour
{
  //各プレイヤーの車
  private GameObject player1;
  private GameObject player2;
  private GameObject player3;
  private GameObject player4;
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

  //ルート分岐ボタン
  private GameObject upButton;
  private GameObject rightButton;
  private GameObject leftButton;

  //各スクリプト
  private MakePlayerPrefab makePlayerPrefabScript;
  private EventEndGame eventEndGameScript;
  private MoneyUpdate moneyUpdateScript;
  private EventPayDay eventPayDayScript;
  public PlayerStatusUI playerStatusUIScript;

  //給料日の効果音
  private AudioSource payDayEffect;

  //切り替えるスクリプト
  private ClickLeftRight routeBranchScript;
  private Roulette rScript;
  private Action actionScript;
  private EventSystem eventSystemScript;

  //現在の操作プレイヤーが誰かを保存
  private int nowTurnPlayerNum = 0;

  //ゴールした人数を保存
  private int goalPlayerNum = 0;

  //ターンスタート時の所持金を保持
  private int originalMoney = 0;

  //外部参照系
  #region
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
  #endregion

  void Start()
  {
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
    routeBranchScript = GameObject.Find("GameScripts").GetComponent<ClickLeftRight>();

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

    //プレイヤー配列
    Players = new GameObject[] { player1, player2, player3, player4 };

    //プレイヤーカメラ配列
    PlayerCameras = new GameObject[] { playerCamera1, playerCamera2, playerCamera3, playerCamera4 };

    //プレイヤーの基本マテリアル配列
    BaseMaterials = new Material[] { player1BaseMaterial, player2BaseMaterial, player3BaseMaterial, player4BaseMaterial };

    //プレイヤーの透明化マテリアル配列
    ClearMaterials = new Material[] { player1ClearMaterial, player2ClearMaterial, player3ClearMaterial, player4ClearMaterial };

    //プレイヤーの設定
    for (int i = 0; i < makePlayerPrefabScript.GetPlayerNum(); i++)
    {
      //プレイヤースクリプトをプレイヤー配列に格納
      Players[i] = GameObject.Find("Player" + (i + 1));

      //最初は全員のカメラを非アクティブにする
      PlayerCameras[i] = Players[i].transform.Find("PlayerCamera").gameObject;
      PlayerCameras[i].SetActive(false);

      //最初は全員の車を透明化する
      Players[i].GetComponent<Renderer>().material = ClearMaterials[i];
      Players[i].GetComponent<BoxCollider>().enabled = false;
    }

    //現在のターンのプレイヤーを設定
    nowTurnPlayerNum = 0;

    //ターンの開始
    TurnStartSystemMaster();
  }


  //現在のターンのプレイヤーのカメラをアクティブにする
  void OnPlayerCamera()
  {
    PlayerCameras[nowTurnPlayerNum].SetActive(true);
  }


  //現在のターンのプレイヤーのカメラを非アクティブにする
  void OffPlayerCamera()
  {
    PlayerCameras[nowTurnPlayerNum].SetActive(false);
  }


  //ターンの切り替え
  void ChangeNowPlayer()
  {
    Players[nowTurnPlayerNum].GetComponent<Renderer>().material = ClearMaterials[nowTurnPlayerNum];
    Players[nowTurnPlayerNum].GetComponent<BoxCollider>().enabled = false;

    //次のターンのプレイヤーの番号を指定
    if (nowTurnPlayerNum == makePlayerPrefabScript.GetPlayerNum() - 1)
    {
      SetnowTurnPlayerNum(0);
    }
    else
    {
      SetnowTurnPlayerNum(nowTurnPlayerNum + 1);
    }
  }


  //ルート分岐ボタンの表示・非表示設定の変更
  void SwitchRouteSelectButton()
  {
    if (actionScript.GetCheckPoint())
    {
      //スタートにいる
      if (actionScript.GetCheckPointName() == "CheckPosition")
      {
        //Debug.Log("CheckPosition");
        rightButton.SetActive(true);
        leftButton.SetActive(true);
      }
      //結婚マスもしくは最後のルート分岐マスにいる
      else if (actionScript.GetCheckPointName() == "CheckPosition2" || actionScript.GetCheckPointName() == "CheckPosition3")
      {
        //Debug.Log("CheckPosition2orCheckPosition3");
        leftButton.SetActive(true);
        upButton.SetActive(true);
      }
      //ゴールにいる
      else if (actionScript.GetCheckPointName() == "GoalPosition")
      {
        if (Players[nowTurnPlayerNum].GetComponent<Player>().CheckGoal)
        {
          rScript.PowerBarStart();
        }
        else
        {
          Players[nowTurnPlayerNum].GetComponent<Player>().CheckGoal = true;
          rScript.PowerBarStart();
        }
        //Debug.Log("Goal");
      }
    }
    //それ以外のマスにいる
    else
    {
      //Debug.Log("No");
      rScript.PowerBarStart();
    }
  }


  //現在のターンのプレイヤーに付いているスクリプトに切り替え・車の切り替え
  void SetObjects()
  {
    //ClickLeftRightスクリプトで使われるMovementBaseScriptスクリプトを変更
    routeBranchScript.SetmbScript(Players[nowTurnPlayerNum].GetComponent<MovementBaseScript>());

    //ClickLeftRightスクリプトで使われるActionスクリプトを変更
    routeBranchScript.SetActionScript(Players[nowTurnPlayerNum].GetComponent<Action>());


    //Rouletteスクリプトで使われるcarオブジェクトを変更
    rScript.SetCar(Players[nowTurnPlayerNum]);

    //Rouletteスクリプトで使われるMovementBaseScriptスクリプトを変更
    rScript.SetmbScript(Players[nowTurnPlayerNum].GetComponent<MovementBaseScript>());
    //rScript.actionScript = Players[nowTurnPlayerNum].GetComponent<Action>();


    //Actionスクリプトを現在のプレイヤーのものに変更
    actionScript = Players[nowTurnPlayerNum].GetComponent<Action>();


    //EventSystemスクリプトで使われるPlayerスクリプトを変更
    eventSystemScript.SetpPlayerScript(Players[nowTurnPlayerNum].GetComponent<Player>());


    //現在のターンのプレイヤーの車のマテリアルをもとに戻して透明化解除
    Players[nowTurnPlayerNum].GetComponent<Renderer>().material = BaseMaterials[nowTurnPlayerNum];
    Players[nowTurnPlayerNum].GetComponent<BoxCollider>().enabled = true;

    //現在のターンのプレイヤーの所持金を設定（お金の効果音の発動判定に使う）
    originalMoney = Players[nowTurnPlayerNum].GetComponent<Player>().Money;
  }


  //所持金表示UIの金額が変わっていたらお金の音を鳴らす
  private void JudgeOnSound()
  {
    if (Players[nowTurnPlayerNum].GetComponent<Player>().Money != originalMoney)
    {
      payDayEffect.PlayOneShot(payDayEffect.clip);
    }
  }


  //給料日判定
  public void CheckPayDay()
  {
    //給料日にいるもしくは通り過ぎていたら給料日イベントを実行
    if (Players[nowTurnPlayerNum].GetComponent<Player>().PayDayCount != 0)
    {
      eventPayDayScript.execution();
    }
    else
    {
      LastProcessing();
    }
  }


  //4人全員ゴールしているか判定を行い次のターンを始めるか決める
  public void LastProcessing()
  {
    playerStatusUIScript.UpdatePlayersStatus();

    if (goalPlayerNum != makePlayerPrefabScript.GetPlayerNum())
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


  //ターンを始める際の処理をまとめた関数
  public void TurnStartSystemMaster()
  {
    Debug.Log("Start");

    //現在のターンのプレイヤーカメラをつける
    OnPlayerCamera();

    //スクリプトの切り替え
    SetObjects();

    //所持金UIの金額を切り替え
    moneyUpdateScript.UpdateMoneyText();

    //ルート選択ボタンの表示・非表示処理をする
    SwitchRouteSelectButton();
  }


  //ターン終わる際の処理をまとめた関数
  public void TurnEndSystemMaster()
  {
    playerStatusUIScript.UpdatePlayersStatus();
    moneyUpdateScript.UpdateMoneyText();

    JudgeOnSound();

    StartCoroutine("sleep");
  }
  private IEnumerator sleep()
  {
    //イベント固有

    yield return new WaitForSeconds(1f);  //1秒待つ

    CheckPayDay();
  }
}
