using System.Collections;
using UnityEngine;
using TMPro;

public class Event1 : MonoBehaviour
{
  
  private TurnSystem turnSystemScript;
  private Player playerScript;

  //イベント固有
  private GameObject text; 
  private TextMeshProUGUI eventText;

  void Start()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();

    //イベント固有
    text = GameObject.Find("EventText");
    eventText = GameObject.Find("EventText").GetComponent<TextMeshProUGUI>();
    text.SetActive(false);

    //現在のターンが誰かを取得して、それに応じてプレイヤースクリプトを取得
    switch(turnSystemScript.GetnowTurnPlayerNum())
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
  }

    // Update is called once per frame
  void Update()
  {
        
  }

  public void execution()
  {
    Player.setJob = "programmer";
    Player.
    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

  // private IEnumerator sleep()
  // {
  //   //イベント固有
  //   Debug.Log("開始");
  //   yield return new WaitForSeconds(3.0f);  //10秒待つ
  //   Debug.Log("3秒経ちました");

  // }
}