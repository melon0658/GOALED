using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPayDay : MonoBehaviour
{
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;
  private MoneyUpdate moneyUpdateScript;
  private AudioSource payDayEffect;


  // Start is called before the first frame update
  void Start()
  {
    //どのイベントにも必要なやつ
    canvas = GameObject.Find("Canvas");
    payDayEffect = GameObject.Find("PaydayObjects").GetComponent<AudioSource>();

  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void execution()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    //現在のターンが誰かを取得して、それに応じてプレイヤースクリプトを取得
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        break;
      default:
        break;
    }

    //イベント固有
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();

    moneyUpdateScript = GameObject.Find("MoneyUIBox").GetComponent<MoneyUpdate>();

    StartCoroutine("sleep");

    //playerのお金が取得して変更したい！(getもsetもこの書き方)

    

  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始payday");
    int salary;
    string jobName;
    switch (playerScript.Job)
    {
      case "プロゲーマー":
        salary = 20000;
        jobName = playerScript.Job;
        break;
      case "エンジニア":
        salary = 25000;
        jobName = playerScript.Job;
        break;
      case "作家":
        salary = 30000;
        jobName = playerScript.Job;
        break;
      case "アスリート":
        salary = 35000;
        jobName = playerScript.Job;
        break;
      case "パイロット":
        salary = 40000;
        jobName = playerScript.Job;
        break;
      case "パティシエ":
        salary = 45000;
        jobName = playerScript.Job;
        break;
      case "科学者":
        salary = 50000;
        jobName = playerScript.Job;
        break;
      case "俳優":
        salary = 55000;
        jobName = playerScript.Job;
        break;
      case "建築家":
        salary = 60000;
        jobName = playerScript.Job;
        break;
      case "弁護士":
        salary = 65000;
        jobName = playerScript.Job;
        break;
      case "医者":
        salary = 70000;
        jobName = playerScript.Job;
        break;
      case "無し":
        salary = 10000;
        playerScript.Job = "フリーター";
        jobName = playerScript.Job;
        break;
      default:
        salary = 10000;
        jobName = playerScript.Job;
        break;
    }
    if(playerScript.PayDayCount == 1)
    {
      textDialogManegerScript.SetdialogText("給料日がやってきた!!\n" + jobName + " : $" + salary.ToString("N0"));
    }
    else
    {
      textDialogManegerScript.SetdialogText("給料日がやってきた!!\n" + jobName + " : $" + salary.ToString("N0") + " × " + playerScript.PayDayCount + "回");
    }

    playerScript.Money = playerScript.Money + salary * playerScript.PayDayCount;

    playerScript.PayDayCount = 0;

    yield return new WaitForSeconds(1.5f);  //10秒待つ

    moneyUpdateScript.UpdateMoneyText();
    payDayEffect.PlayOneShot(payDayEffect.clip);

    yield return new WaitForSeconds(1.5f);  //10秒待つ

    textDialogManegerScript.HiddentextDialogBox();

    turnSystemScript.LastProcessing();

    //どのイベントにも必要なやつ
    //turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
}
