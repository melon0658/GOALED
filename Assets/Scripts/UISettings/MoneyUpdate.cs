using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUpdate : MonoBehaviour
{
  private TextMeshProUGUI moneyText;
  private TurnSystem turnSystemScript;

  //各プレイヤーのスクリプト
  public Player player1;
  public Player player2;
  public Player player3;
  public Player player4;

  // Start is called before the first frame update
  void Start()
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
        moneyText.text = "$ " + player1.Money.ToString("N0");
        break;
      case 2:
        moneyText.text = "$ " + player2.Money.ToString("N0");
        break;
      case 3:
        moneyText.text = "$ " + player3.Money.ToString("N0");
        break;
      case 4:
        moneyText.text = "$ " + player4.Money.ToString("N0");
        break;
      default:
        break;
    }
  }
}
