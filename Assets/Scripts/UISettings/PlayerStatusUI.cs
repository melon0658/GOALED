using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUI : MonoBehaviour
{
  private GameObject status;

  private GameObject[] players;

  private MakePlayerPrefab makePlayerPrefabScript;

  //player1の情報を格納するテキストUI
  #region
  public TextMeshProUGUI player1Name;
  public TextMeshProUGUI player1Money;
  public TextMeshProUGUI player1Job;
  public TextMeshProUGUI player1Spouse;
  public TextMeshProUGUI player1ChildrenNum;
  public TextMeshProUGUI player1HasHouse;

  private List<TextMeshProUGUI> player1Texts;
  #endregion

  //player2の情報を格納するテキストUI
  #region
  public TextMeshProUGUI player2Name;
  public TextMeshProUGUI player2Money;
  public TextMeshProUGUI player2Job;
  public TextMeshProUGUI player2Spouse;
  public TextMeshProUGUI player2ChildrenNum;
  public TextMeshProUGUI player2HasHouse;

  private List<TextMeshProUGUI> player2Texts;
  #endregion

  //player3の情報を格納するテキストUI
  #region
  public TextMeshProUGUI player3Name;
  public TextMeshProUGUI player3Money;
  public TextMeshProUGUI player3Job;
  public TextMeshProUGUI player3Spouse;
  public TextMeshProUGUI player3ChildrenNum;
  public TextMeshProUGUI player3HasHouse;

  private List<TextMeshProUGUI> player3Texts;
  #endregion

  //player4の情報を格納するテキストUI
  #region
  public TextMeshProUGUI player4Name;
  public TextMeshProUGUI player4Money;
  public TextMeshProUGUI player4Job;
  public TextMeshProUGUI player4Spouse;
  public TextMeshProUGUI player4ChildrenNum;
  public TextMeshProUGUI player4HasHouse;

  private List<TextMeshProUGUI> player4Texts;
  #endregion

  private List<List<TextMeshProUGUI>> playersTexts;

  // Start is called before the first frame update
  void Start()
  {
    status = GameObject.Find("Status");

    makePlayerPrefabScript = GameObject.Find("GameScripts").GetComponent<MakePlayerPrefab>();

    player1Texts = new List<TextMeshProUGUI>() { player1Name, player1Money, player1Job, player1Spouse, player1ChildrenNum, player1HasHouse };
    player2Texts = new List<TextMeshProUGUI>() { player2Name, player2Money, player2Job, player2Spouse, player2ChildrenNum, player2HasHouse };
    player3Texts = new List<TextMeshProUGUI>() { player3Name, player3Money, player3Job, player3Spouse, player3ChildrenNum, player3HasHouse };
    player4Texts = new List<TextMeshProUGUI>() { player4Name, player4Money, player4Job, player4Spouse, player4ChildrenNum, player4HasHouse };
    playersTexts = new List<List<TextMeshProUGUI>>() { player1Texts, player2Texts, player3Texts, player4Texts };

    UpdatePlayersStatus();

    status.SetActive(false);
  }

  public void UpdatePlayersStatus()
  {
    //プレイヤー配列を取得
    players = makePlayerPrefabScript.GetPlayers();

    for (int i = 0;i < makePlayerPrefabScript.GetPlayerNum(); i++){
      //プレイヤースクリプトを取得
      Player player = players[i].GetComponent<Player>();

      //プレイヤー名
      playersTexts[i][0].text = player.PlyaerName;

      //所持金
      playersTexts[i][1].text = player.Money.ToString("N0");

      //仕事
      playersTexts[i][2].text = player.Job;

      //配偶者の有・無
      if (player.Spouse)
      {
        playersTexts[i][3].text = "有り";
      }
      else
      {
        playersTexts[i][3].text = "無し";
      }

      //子供の数
      playersTexts[i][4].text = player.Child.ToString() + " 人";

      //持ち家の有・無
      if (player.HouseNumber != 100)
      {
        playersTexts[i][5].text = "有り";
      }
      else
      {
        playersTexts[i][5].text = "無し";
      }
    } 
  }
}
