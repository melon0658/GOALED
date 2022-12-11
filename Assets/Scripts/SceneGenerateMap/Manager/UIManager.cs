using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
  [SerializeField] private StetusTableUI stetusTableUI;
  [SerializeField] private ControllerUI controllerUI;

  public void ActiveDiceButton()
  {
    controllerUI.ActiveDiceUI();
  }

  public void ActiveDirectionButton(Tile tile)
  {
    controllerUI.ActiveDirectionUI(tile);
  }

  public void UpdatePlayerStetus(Dictionary<string, PlayerStetus> players)
  {
    stetusTableUI.UpdatePlayerStetus(players);
  }
}
