using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private string plyaerName; //プレイヤー名
  private int money; //所持金
  private int nowPosIndex; //現在いるマス
  private string color; //プレイヤーカラー
  private string job; //職業
  private bool spouse; //配偶者
  private int child; //子供
  private int houseNumber; //持ち家の番号
  private bool checkGoal; //ゴールしているか
  private int payDayCount; //給料日を過ぎた回数
  private bool restTurn; //ターン休みかどうか
    

  public GameObject status;
  public GameObject birdCamera;
  private GameObject canvas;
  private GameObject rouletteCamera;

  public Player(string plyaerName, int money, int nowPosIndex, string color, string job, bool spouse, int child, int houseNumber, bool checkGoal)
  {
    this.PlyaerName = plyaerName;
    this.Money = money;
    this.NowPosIndex = nowPosIndex;
    this.Color = color;
    this.Job = job;
    this.Spouse = spouse;
    this.Child = child;
    this.HouseNumber = houseNumber;
    this.CheckGoal = checkGoal;
  }

  //各ステータスのgetとset
  public string PlyaerName { get => plyaerName; set => plyaerName = value; }
  public int Money { get => money; set => money = value; }
  public int NowPosIndex { get => nowPosIndex; set => nowPosIndex = value; }
  public string Color { get => color; set => color = value; }
  public string Job { get => job; set => job = value; }
  public bool Spouse { get => spouse; set => spouse = value; }
  public int Child { get => child; set => child = value; }
  public int HouseNumber { get => houseNumber; set => houseNumber = value; }
  public bool CheckGoal { get => checkGoal; set => checkGoal = value; }
  public int PayDayCount { get => payDayCount; set => payDayCount = value; }
  public bool RestTurn { get => restTurn; set => restTurn = value; }

  // Start is called before the first frame update
  void Start()
  {
    //new Player("aa", 30000, 0, "red", "", false, 0, 0, false);
    //status = GameObject.Find("Status");
    //status.SetActive(false);
    //Debug.Log(status);
    this.PayDayCount = 0;
    this.RestTurn = false;

    canvas = GameObject.Find("Canvas");
    rouletteCamera = GameObject.Find(" RouletteCamera");
  }

    // Update is called once per frame
    void Update()
    {
        //Tabキーが入力された場合
        if (Input.GetKey(KeyCode.Tab))
        {
            //ステータス画面を表示
            status.SetActive(true);
        }
        else
        {
            //ステータス画面を非表示
            status.SetActive(false);
        }
        //Mキーが入力された場合
        if (Input.GetKey(KeyCode.M))
        {
          //ステータス画面を表示
          birdCamera.SetActive(true);
          canvas.SetActive(false);
          rouletteCamera.SetActive(false);
        }
        else
        {
          //ステータス画面を非表示
          birdCamera.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
          canvas.SetActive(true);
          rouletteCamera.SetActive(true);
        }
        
  }

  void OnTriggerExit(Collider other)
  {
    //Debug.Log(other.name);
    // 衝突した相手オブジェクトのタグが"CheckPoint"
    if (other.tag == "PayDay")
    {
      // 分岐判定をtrueにする
      Debug.Log("true");
      PayDayCount++;
    }
  }
}
