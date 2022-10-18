using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayData : MonoBehaviour
{
  [SerializeField] GameObject textObject;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetText(Dictionary<string, Dictionary<string, string>> data)
  {
    string text = "";
    foreach (var player in data)
    {
      text += player.Key + " : ";
      foreach (var item in player.Value)
      {
        text += item.Key + " : " + item.Value + " ";
      }
      text += "\n";
    }
    textObject.GetComponent<TextMeshProUGUI>().text = text;
  }
}
