using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLineGameExit : MonoBehaviour
{
  public void ButtonExit()
  {
    UnityEditor.EditorApplication.isPlaying = false;
  }
}
