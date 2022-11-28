using System.Collections;
using UnityEngine;
using TMPro;

public class Event27 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
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
    textDialogManegerScript.SetdialogText("医者になる\n給与：70000$");

    StartCoroutine("sleep");

    
    playerScript.Job = "医者";
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
    Vector3 pos = transform.position;
    pos.x =145.80f;pos.y=409f;pos.z=-121.40f;
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        GameObject.Find("defaultCar1").transform.position=pos;
        GameObject.Find("defaultCar1").transform.rotation = Quaternion.Euler(0f,90f,0f);
        break;
      case 2:
        GameObject.Find("defaultCar2").transform.position=pos;
        GameObject.Find("defaultCar2").transform.rotation = Quaternion.Euler(0f,90f,0f);
        break;
      case 3:
        GameObject.Find("defaultCar3").transform.position=pos;
        GameObject.Find("defaultCar3").transform.rotation = Quaternion.Euler(0f,90f,0f);
        break;
      case 4:
        GameObject.Find("defaultCar4").transform.position=pos;
        GameObject.Find("defaultCar4").transform.rotation = Quaternion.Euler(0f,90f,0f);
        break;
      default:
        break;
    }
    playerScript.NowPosIndex = 9;

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}