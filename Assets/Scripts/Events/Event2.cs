using System.Collections;
using UnityEngine;
using TMPro;

public class Event2 : MonoBehaviour
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
    //text = GameObject.Find("EventText2");
    //eventText = GameObject.Find("EventText2").GetComponent<TextMeshProUGUI>();
    //text.SetActive(false);
  }

    // Update is called once per frame
  void Update()
  {
        
  }

  public void execution()
  {
    //イベント固有
    //text.SetActive(true);
    // eventText.text = "event2";
    StartCoroutine("sleep");
    
  }

  private IEnumerator sleep()
  {
    //イベント固有
    Debug.Log("イベント開始");
    yield return new WaitForSeconds(1f);  //10秒待つ
    Debug.Log("イベント終了");
    //text.SetActive(false);

    //どのイベントにも必要なやつ
    turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }

}
