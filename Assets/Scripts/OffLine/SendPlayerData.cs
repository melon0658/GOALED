using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SendPlayerData : MonoBehaviour
{
  private ChangePlayerNum changePlayerNumScript;
  private SetPlayerName setplayerNameScript;

  private int playerNum;
  private string[] playerNames;

  private TextMeshProUGUI errorText;
  private float clearValue = 0.0f;

  private GameObject startGameButton;

  //ローディング用
  [SerializeField] private GameObject loadingUI;
  [SerializeField] private Slider slider;


  public int GetPlayerNum()
  {
    return playerNum;
  }


  public string[] GetPlayerNames()
  {
    return playerNames;
  }


  void Start()
  {

    changePlayerNumScript = GameObject.Find("Dropdown").GetComponent<ChangePlayerNum>();

    setplayerNameScript = GameObject.Find("InputFields").GetComponent<SetPlayerName>();

    errorText = GameObject.Find("ErrorText").GetComponent<TextMeshProUGUI>();
    errorText.color = new Color(255, 0, 0, clearValue);

    startGameButton = GameObject.Find("StartGameButton");


    DontDestroyOnLoad(this);
  }

  public void ClickStartGameButton()
  {
    playerNum = changePlayerNumScript.GetPlayerNum();
    playerNames = setplayerNameScript.GetPlayerNames();

    int i = 0;
    for (i = 0; i < playerNum; i++)
    {
      if(i < playerNum)
      {
        if(playerNames[i] == "")
        {
          break;
        }
      }
    }


    if(i == playerNum)
    {
      //SceneManager.LoadScene("MainScene");

      loadingUI.SetActive(true);
      StartCoroutine(LoadScene());
    }
    else
    {
      //StartGameボタンを押せなくする
      startGameButton.GetComponent<Button>().interactable = false;

      //エラーメッセージをフェードイン
      InvokeRepeating("FadeIn", 0.0f, 0.015f);

      //エラーメッセージをフェードアウト
      StartCoroutine("wait");
    }
    
  }


  void FadeIn()
  {
    //テキストの不透明度を上げる
    clearValue += 0.05f;
    errorText.color = new Color(255, 0, 0, clearValue);

    //不透明化が終了したらFadeInを止める
    if (clearValue >= 1.0f)
    {
      CancelInvoke("FadeIn");
    }
  }


  private IEnumerator wait()
  {
    yield return new WaitForSeconds(2f);
    InvokeRepeating("FadeOut", 0.0f, 0.015f);
  }


  void FadeOut()
  {
    //テキストの不透明度を下げる
    clearValue -= 0.05f;
    errorText.color = new Color(255, 0, 0, clearValue);

    //不透明化が終了したらFadeOutを止める
    if (clearValue <= 0.0f)
    {
      CancelInvoke("FadeOut");

      //StartGameボタンを押せるようにする
      startGameButton.GetComponent<Button>().interactable = true;
    }
  }

  IEnumerator LoadScene()
  {
    AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
    while (!async.isDone)
    {
      slider.value = async.progress;
      yield return null;
    }
  }
}
