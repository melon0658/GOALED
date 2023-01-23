using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetPlayerName : MonoBehaviour
{
  private string playerName1;
  private string playerName2;
  private string playerName3;
  private string playerName4;
  private string[] playerNames;

  public string[] GetPlayerNames()
  {
    return playerNames;
  }

  void Start()
  {
    playerNames = new string[] { playerName1, playerName2, playerName3, playerName4 };

    for(int i = 0; i < 4; i++)
    {
      playerNames[i] = "";
    }
  }

  public void SetPlayerName1(string text)
  {

    playerNames[0] = text;
  }


  public void SetPlayerName2(string text)
  {

    playerNames[1] = text;
  }


  public void SetPlayerName3(string text)
  {

    playerNames[2] = text;
  }


  public void SetPlayerName4(string text)
  {

    playerNames[3] = text;
  }
}
