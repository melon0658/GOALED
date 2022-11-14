using System.Collections;
using UnityEngine;
using TMPro;

public class Event1 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;
  private Player playerScript;

  //イベント固有
  private GameObject text; 
  private TextMeshProUGUI eventText;

  void Start()
  {
    


    //イベント固有
    text = GameObject.Find("EventText");
    eventText = text.GetComponent<TextMeshProUGUI>();
    text.SetActive(false);
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
    text.SetActive(true);
    eventText.text = "event1";
    StartCoroutine("sleep");

    //playerのお金が取得して変更したい！(getもsetもこの書き方)
    playerScript.Money = playerScript.Money + 10000;
    
  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(1f);  //10秒待つ
    Debug.Log("イベント終了");
    text.SetActive(false);

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}
