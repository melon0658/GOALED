using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//各プレイヤーの初期値を設定するスクリプト
public class SettingInitialPlayerStatus : MonoBehaviour
{
  private Player player1;
  private Player player2;
  private Player player3;
  private Player player4;

  public void SettingPlayer1()
  {
    player1 = GameObject.Find("Player1").GetComponent<Player>();

    player1.PlyaerName = "A";
    player1.Money = 3000;
    player1.Color = "Green";
    player1.Job = "無し";
    player1.Spouse = false;
    player1.Child = 0;
    player1.HouseNumber = 100;
    player1.CheckGoal = false;

  }
  public void SettingPlayer2()
  {
    player2 = GameObject.Find("Player2").GetComponent<Player>();

    player2.PlyaerName = "B";
    player2.Money = 3000;
    player2.Color = "Blue";
    player2.Job = "無し";
    player2.Spouse = false;
    player2.Child = 0;
    player2.HouseNumber = 100;
    player2.CheckGoal = false;
  }
  public void SettingPlayer3()
  {
    player3 = GameObject.Find("Player3").GetComponent<Player>();

    player3.PlyaerName = "C";
    player3.Money = 3000;
    player3.Color = "Red";
    player3.Job = "無し";
    player3.Spouse = false;
    player3.Child = 0;
    player3.HouseNumber = 100;
    player3.CheckGoal = false;
  }
  public void SettingPlayer4()
  {
    player4 = GameObject.Find("Player4").GetComponent<Player>();

    player4.PlyaerName = "D";
    player4.Money = 3000;
    player4.Color = "Yellow";
    player4.Job = "無し";
    player4.Spouse = false;
    player4.Child = 0;
    player4.HouseNumber = 100;
    player4.CheckGoal = false;
  }
}
