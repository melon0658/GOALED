using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OffLineStartButton : MonoBehaviour
{
  [SerializeField] private GameObject bgm;
  private void Start()
  {
    if (!bgm.activeSelf)
    {
      bgm.SetActive(true);
    }
  }

public void OnClick()
  {
    SceneManager.LoadScene("OffLineCreateUserScene");// Mainシーンに変える
  }
}
