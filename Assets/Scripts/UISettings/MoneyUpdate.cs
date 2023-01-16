using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUpdate : MonoBehaviour
{
  private TextMeshProUGUI moneyText;
  private TurnSystem turnSystemScript;
  //private AudioSource payDayEffect;


  // Start is called before the first frame update
  void Awake()
  {
    moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    
  }
  
  public void UpdateMoneyText()
  {
    //現在の操作プレイヤーの所持金に変更
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        moneyText.text = "$ " + GameObject.Find("Player1").GetComponent<Player>().Money.ToString("N0");
        break;
      case 2:
        moneyText.text = "$ " + GameObject.Find("Player2").GetComponent<Player>().Money.ToString("N0");
        break;
      case 3:
        moneyText.text = "$ " + GameObject.Find("Player3").GetComponent<Player>().Money.ToString("N0");
        break;
      case 4:
        moneyText.text = "$ " + GameObject.Find("Player4").GetComponent<Player>().Money.ToString("N0");
        break;
      default:
        break;
    }
    StartCoroutine("sleep");
  }
  private IEnumerator sleep()
  {
    //イベント固有
    //Debug.Log("イベント開始");
    //payDayEffect.PlayOneShot(payDayEffect.clip);
    yield return new WaitForSeconds(0.5f);  //10秒待つ
    //Debug.Log("イベント終了");
    //text.SetActive(false);

    //どのイベントにも必要なやつ
    //turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
}
