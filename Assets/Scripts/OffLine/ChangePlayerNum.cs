using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class ChangePlayerNum : MonoBehaviour
{
  private GameObject inputField_1;
  private GameObject inputField_2;
  private GameObject inputField_3;
  private GameObject inputField_4;
  private GameObject[] inputFields;

  private TextMeshProUGUI playerNumText;

  private int playerNum;

  public int GetPlayerNum()
  {
    return playerNum;
  }

  void Start()
  {
    inputField_1 = GameObject.Find("InputField_1");
    inputField_2 = GameObject.Find("InputField_2");
    inputField_3 = GameObject.Find("InputField_3");
    inputField_4 = GameObject.Find("InputField_4");

    inputFields = new GameObject[] { inputField_1, inputField_2, inputField_3, inputField_4 };

    playerNumText = GameObject.Find("Dropdown").transform.Find("Label").GetComponent<TextMeshProUGUI>();
    playerNum = int.Parse(Regex.Replace(playerNumText.text, @"[^0-9]", ""));

    for(int i = 0; i < 4; i++)
    {
      if(i >= playerNum)
      {
        inputFields[i].SetActive(false);
      }
    }
  }

  public void OnInputField()
  {
    playerNumText = GameObject.Find("Dropdown").transform.Find("Label").GetComponent<TextMeshProUGUI>();

    playerNum = int.Parse(Regex.Replace(playerNumText.text, @"[^0-9]", ""));

    for (int i = 0; i < 4; i++)
    {
      if (i < playerNum)
      {
        inputFields[i].SetActive(true);
      }
      else if (i >= playerNum)
      {
        inputFields[i].SetActive(false);
      }
    }
  }
}
