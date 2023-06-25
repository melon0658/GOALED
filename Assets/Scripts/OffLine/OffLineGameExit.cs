using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLineGameExit : MonoBehaviour
{
  public void ButtonExit()
  {
    //buil前
    //UnityEditor.EditorApplication.isPlaying = false;

    //build後
    Application.Quit();
  }
}
