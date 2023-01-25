using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーを生成するスクリプト
public class MakePlayerPrefab : MonoBehaviour
{
  //呼び出したプレイヤープレハブを格納するための配列
  private GameObject player_prefab;

  //プレイヤーを格納するための配列
  private GameObject[] players;

  //プレイヤーを最初に配置する座標の配列
  private Vector3[][] firstPlayerPos = new Vector3[4][];

  //マテリアル関係の変数
  #region
  private Material black;//プレイヤーキャラに使う
  private Material white;//プレイヤーキャラに使う
  private Material libon;//プレイヤーキャラに使う

  private Material[] carMaterials;
  private Material[] carClearMaterials;
  #endregion

  //初期値設定をするスクリプト
  private SettingInitialPlayerStatus firstSet;

  //プレイヤーデータを受け取るのに必要なスクリプト
  private SendPlayerData setplayerDataScript;

  //プレイヤー数を保存
  public int playerNum;

  //プレイヤー名を保存
  private string[] playerNames;

  //プレイヤー数を外部から取得可能にする
  public int GetPlayerNum()
  {
    return playerNum;
  }

  //プレイヤー配列を外部から取得可能にする
  public GameObject[] GetPlayers()
  {
    return players;
  }

  void Awake()
  {
    //BGM消す
    GameObject bgm = GameObject.Find("BGM");
    bgm.SetActive(false);
    SceneManager.MoveGameObjectToScene(bgm, SceneManager.GetActiveScene());

    //プレイヤーを最初に配置する座標を代入
    firstPlayerPos[0] = new Vector3[] { new Vector3(81.00f, 409.00f, -170.00f) };//プレイヤー数1人用
    firstPlayerPos[1] = new Vector3[] { new Vector3(71.00f, 409.00f, -170.00f), new Vector3(88.50f, 409.00f, -170.00f) };//プレイヤー数2人用
    firstPlayerPos[2] = new Vector3[] { new Vector3(69.50f, 409.00f, -170.00f), new Vector3(81.00f, 409.00f, -170.00f), new Vector3(92.50f, 409.00f, -170.00f) };//プレイヤー数3人用
    firstPlayerPos[3] = new Vector3[] { new Vector3(66.00f, 409.00f, -170.00f), new Vector3(76.00f, 409.00f, -170.00f), new Vector3(86.00f, 409.00f, -170.00f), new Vector3(96.00f, 409.00f, -170.00f) };//プレイヤー数4人用

    //マテリアルを設定
    setMaterials();

    //プレイヤーの人数に応じてプレイヤーを格納する配列を宣言(ここでプレイヤー人数を取得)
    setplayerDataScript = GameObject.Find("SendPlayerData").GetComponent<SendPlayerData>();
    playerNum = setplayerDataScript.GetPlayerNum();
    players = new GameObject[playerNum];

    //プレイヤー名の配列を取得
    playerNames = setplayerDataScript.GetPlayerNames();

    //プレイヤー生成
    MakePlayer();
    
  }

  //マテリアルの設定
  void setMaterials()
  {
    black = (Material)Resources.Load("Player_Resources/Black");
    white = (Material)Resources.Load("Player_Resources/White");
    libon = (Material)Resources.Load("Player_Resources/Libon");

    carMaterials = new Material[] { (Material)Resources.Load("Player_Resources/car1"),
                                    (Material)Resources.Load("Player_Resources/car2"),
                                    (Material)Resources.Load("Player_Resources/car3"),
                                    (Material)Resources.Load("Player_Resources/car4") };

    carClearMaterials = new Material[] { (Material)Resources.Load("Player_Resources/car1Clear"),
                                         (Material)Resources.Load("Player_Resources/car2Clear"),
                                         (Material)Resources.Load("Player_Resources/car3Clear"),
                                         (Material)Resources.Load("Player_Resources/car4Clear") };

  }

  //プレイヤーの生成をする関数
  void MakePlayer()
  {
    //プレハブを取得
    player_prefab = (GameObject)Resources.Load("Player_Resources/player");
    for (int i = 0; i < playerNum; i++)
    {
      //プレハブからインスタンスを生成してプレイヤー配列に格納
      players[i] = Instantiate(player_prefab, firstPlayerPos[playerNum-1][i], Quaternion.identity);


      //オブジェクトの名前変更
      players[i].name = "Player" + (i+1);


      //マテリアルの付替え(配列ごと置き換える)
      
      //車の色付け替え
      Material[] carMat = new Material[1];
      carMat[0] = carMaterials[i];
      players[i].GetComponent<Renderer>().materials = carMat;
      
      //K1の色付け替え
      Material[] k1Mat = new Material[3];
      k1Mat[0] = carMaterials[i];
      k1Mat[1] = white;
      k1Mat[2] = black;
      players[i].transform.Find("K1").gameObject.GetComponent<Renderer>().materials = k1Mat;
      
      //K2の色付け替え
      Material[] k2Mat = new Material[3];
      k2Mat[0] = carMaterials[i];
      k2Mat[1] = white;
      k2Mat[2] = black;
      players[i].transform.Find("K2").gameObject.GetComponent<Renderer>().materials = k2Mat;
      
      //Main_Charaの色付け替え
      Material[] mainMat = new Material[3];
      mainMat[0] = carMaterials[i];
      mainMat[1] = white;
      mainMat[2] = black;
      players[i].transform.Find("Main_Chara").gameObject.GetComponent<Renderer>().materials = mainMat;
      
      //Sub_Charaの色付け替え
      Material[] subMat = new Material[3];
      subMat[0] = carMaterials[i];
      subMat[1] = white;
      subMat[2] = black;
      players[i].transform.Find("Sub_Chara").gameObject.GetComponent<Renderer>().materials = subMat;
      
      //マテリアルの付替え(配列ごと置き換える)ここまで


      //プレイヤーの初期値設定&所持金更新用
      firstSet = GameObject.Find("GameScripts").GetComponent<SettingInitialPlayerStatus>();
      switch (i)
      {
        case 0:
          firstSet.SettingPlayer1(playerNames[0]);
          break;
        case 1:
          firstSet.SettingPlayer2(playerNames[1]);
          break;
        case 2:
          firstSet.SettingPlayer3(playerNames[2]);
          break;
        case 3:
          firstSet.SettingPlayer4(playerNames[3]);
          break;
        default:
          break;
      }
    }
  }
}
