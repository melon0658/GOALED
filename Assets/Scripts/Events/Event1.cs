using System.Collections;
using UnityEngine;
using TMPro;

public class Event1 : MonoBehaviour
{
  //どのイベントにも必要なやつ
  private TurnSystem turnSystemScript;

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
  }

    // Update is called once per frame
  void Update()
  {
        
  }

  public void execution()
  {
    //イベント固有
    text.SetActive(true);
    eventText.text = "event1";
    StartCoroutine("sleep");
    text.SetActive(false);

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("開始");
    yield return new WaitForSeconds(3.0f);  //10秒待つ
    Debug.Log("3秒経ちました");

  }

}
