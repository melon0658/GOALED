using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUpdate : MonoBehaviour
{
  private TextMeshProUGUI moneyText;
  private TurnSystem turnSystemScript;
  private MakePlayerPrefab makePlayerPrefabScript;

  private Player playerScript;

  void Awake()
  {
    moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
  }
  
  public void UpdateMoneyText()
  {
    //どのイベントにも必要なやつ
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    //現在のターンが誰かを取得
    int nowTrunPlayerNum = turnSystemScript.GetnowTurnPlayerNum();

    //それに応じてプレイヤーを取得
    GameObject Player = makePlayerPrefabScript.GetPlayers()[nowTrunPlayerNum];

    //プレイヤースクリプトを取得
    playerScript = Player.GetComponent<Player>();

    //現在の操作プレイヤーの所持金に変更
    moneyText.text = "$ " + playerScript.Money.ToString("N0");
    
    StartCoroutine("sleep");
  }
  private IEnumerator sleep()
  {
    yield return new WaitForSeconds(0.5f);  //0.5秒待つ
  }
}
