using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDialogManager : MonoBehaviour
{
  //TextDialogBoxを入れる
  public GameObject textDialogBox;

  //テキスト文
  private TextMeshProUGUI dialogText;

  //現在のテキスト参照
  public TextMeshProUGUI GetdialogText()
  {
    return dialogText;
  }

  //テキスト変更
  public void SetdialogText(string changeText)
  {
    this.dialogText.text = changeText;
  }

  // Start is called before the first frame update
  void Start()
  {
    //初期値は非表示状態
    //textDialogBox.SetActive(false);
  }

  //textDialogBoxを表示
  public void ShowtextDialogBox()
  {
    textDialogBox.SetActive(true);
  }

  //textDialogBoxを非表示
  public void HiddentextDialogBox()
  {
    textDialogBox.SetActive(false);
  }
}
