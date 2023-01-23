using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.TableUI;
using TMPro;

public class StetusTableUI : MonoBehaviour
{
  [SerializeField] private TableUI table;

  void Start()
  {
    table.Rows = 1;
    table.Columns = 6;
    table.GetCell(0, 0).text = "名前";
    table.GetCell(0, 1).text = "所持金";
    table.GetCell(0, 2).text = "職業";
    table.GetCell(0, 3).text = "配偶者";
    table.GetCell(0, 4).text = "子供";
    table.GetCell(0, 5).text = "自分の家";
    table.gameObject.SetActive(false);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
    {
      table.gameObject.SetActive(true);
    }
    else if (Input.GetKeyUp(KeyCode.Tab))
    {
      table.gameObject.SetActive(false);
    }
  }

  public void UpdatePlayerStetus(Dictionary<string, PlayerStetus> stetus)
  {
    table.Rows = stetus.Count + 1;
    table.Columns = 6;
    var index = 1;
    foreach (var st in stetus)
    {
      table.GetCell(index, 0).text = st.Value.PlayerName;
      table.GetCell(index, 1).text = st.Value.Money.ToString();
      table.GetCell(index, 2).text = st.Value.Job.ToString();
      table.GetCell(index, 3).text = st.Value.Spouse.ToString();
      table.GetCell(index, 4).text = st.Value.Child.ToString();
      table.GetCell(index, 5).text = st.Value.HouseNumber.ToString();
      index++;
    }
  }
}
