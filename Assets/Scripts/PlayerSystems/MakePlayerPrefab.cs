using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーを生成するスクリプト
public class MakePlayerPrefab : MonoBehaviour
{
  //呼び出したプレイヤープレハブを格納するための配列
  private GameObject player_prefab;
  //プレイヤーを格納するための配列
  private GameObject[] players;
  //プレイヤーを最初に配置する座標の配列
  private Vector3[][] firstPlayerPos = new Vector3[4][];
  //仮でプレイヤー数を指定
  private int playerNum = 1;

  void Awake()
  {
    //プレイヤーを最初に配置する座標を代入
    firstPlayerPos[0] = new Vector3[] { new Vector3(81.00f, 409.00f, -170.00f) };//プレイヤー数1人用
    firstPlayerPos[1] = new Vector3[] { new Vector3(71.00f, 409.00f, -170.00f), new Vector3(88.50f, 409.00f, -170.00f) };//プレイヤー数2人用
    firstPlayerPos[2] = new Vector3[] { new Vector3(69.50f, 409.00f, -170.00f), new Vector3(81.00f, 409.00f, -170.00f), new Vector3(91.00f, 409.00f, -170.00f) };//プレイヤー数3人用
    firstPlayerPos[3] = new Vector3[] { new Vector3(66.00f, 409.00f, -170.00f), new Vector3(76.00f, 409.00f, -170.00f), new Vector3(86.00f, 409.00f, -170.00f), new Vector3(96.00f, 409.00f, -170.00f) };//プレイヤー数4人用

    //プレイヤーの人数に応じてプレイヤーを格納する配列を宣言
    players = new GameObject[playerNum];

    //プレイヤー生成
    MakePlayer();

  }

  //プレイヤーの生成をする関数
  void MakePlayer()
  {
    //プレハブを取得
    player_prefab = (GameObject)Resources.Load("Player_Resources/player");
    for (int i = 0; i < playerNum; i++)
    {
      //プレハブからインスタンスを生成してプレイヤー配列に格納
      players[i] = Instantiate(player_prefab, firstPlayerPos[playerNum-1][i], Quaternion.identity);
      //オブジェクトの名前変更
      players[i].name = "Player" + (i+1);

      //マテリアルの付替え(配列ごと置き換える)
      Material mat = (Material)Resources.Load("Player_Resources/car2Clear");
      //players[i].GetComponent<MeshRenderer>().materials[0].color = new Color(255, 0, 0, 1.0f);
      Material[] tmp = new Material[1];
      tmp[0] = mat;
      players[i].GetComponent<Renderer>().materials = tmp;
    }
  }
}
