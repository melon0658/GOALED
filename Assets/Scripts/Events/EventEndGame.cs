using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventEndGame : MonoBehaviour
{
  public Player player1;
  public Player player2;
  public Player player3;
  public Player player4;

  private Player winner;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void EndGame()
  {
    Debug.Log("The End");
    //金額計算して１位を決める

    //イベントスタート
    StartCoroutine("StartEvevt");
    
  }
  private IEnumerator StartEvevt()
  {
    //いらないオブジェクト非表示(ライトとかカメラとかUIとか)
    OffObjects();

    //必要なオブジェクト表示(Prefabとか)
    OnObjects();

    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight1").gameObject.SetActive(true);
    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight2").gameObject.SetActive(true);
    //1秒まつ
    yield return new WaitForSeconds(1f);
    //スポットライト当てる
    GameObject.Find("EndGameObjects").transform.Find("SpotLight3").gameObject.SetActive(true);

    //所持金表示 + You are a Millionaire !!! or GOALED !!!

    //エモート

    ////どのイベントにも必要なやつ
    //turnSystemScript.TurnEndSystemMaster(); //ターンを終了
  }
  void OffObjects()
  {
    //ライトを消す
    GameObject.Find("StageObjects").transform.Find("Directional Light").gameObject.SetActive(false);
    RenderSettings.ambientIntensity = 0.3f;
    //ルーレットカメラ消す
    GameObject.Find(" RouletteCamera").SetActive(false);
    //Canvas消す
    GameObject.Find("Canvas").SetActive(false);
  }
  void OnObjects()
  {
    //Prefab生成
    
    //優勝者を真ん中、残りのプレイヤーは周りに置く

  }
}
