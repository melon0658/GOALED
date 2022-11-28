using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money_Event : MonoBehaviour
{
  public float plus_height = 10.0f; //お金が落ちてくる高さ
  public float minus_height = 5.0f; //お金が抜き取られる高さ
  public int dispersion = 50; //お金の散布範囲
  public Material[] Money_Material = new Material[6];
  public static Money_Event instance;
  private int[] Money_List = { 100000, 50000, 20000, 10000, 5000, 1000 };
  private int[] plus_minus = { -1, 1 };

  public void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
  }

  public void Update(){
        //if (Input.GetKey (KeyCode.KeypadPlus)) {
        //    getMoney(new Vector3(65, 409, -170), 1138000);
        //}
        //if (Input.GetKey (KeyCode.KeypadMinus)) {
        //    lostMoney(new Vector3(65, 409, -170), 1138000);//コマの座標、金額
        //}
  }

  public void getMoney(Transform t, int amount)
  {
    StartCoroutine(GetM(t, amount));
  }

  public void lostMoney(Transform t, int amount)
  {
    StartCoroutine(LostM(t, amount));
  }

  private IEnumerator GetM(Transform t, int amount)
  {
    GameObject pm = (GameObject)Resources.Load("pre_money");
    for (int i = 0; i < 6; i++)
    {
      int count = amount / Money_List[i];
      for (int j = 0; j < count; j++)
      {
        GameObject m = Instantiate(pm, new Vector3(t.position.x, t.position.y + plus_height, t.position.z), Quaternion.Euler(0, t.localEulerAngles.y, 0), t);
        m.GetComponent<MeshRenderer>().material = Money_Material[i];
        yield return new WaitForSeconds(0.2f);
      }
      amount = amount - Money_List[i] * count;
    }
  }

  private IEnumerator LostM(Transform t, int amount)
  {
    GameObject pm = (GameObject)Resources.Load("pre_money");
    for (int i = 0; i < 6; i++)
    {
      int count = amount / Money_List[i];
      for (int j = 0; j < count; j++)
      {
        GameObject m = Instantiate(pm, new Vector3(t.position.x, t.position.y + minus_height, t.position.z), Quaternion.Euler(0, t.localEulerAngles.y, 0), t);
        m.GetComponent<MeshRenderer>().material = Money_Material[i];
        int v_x = Random.Range(1, 10) * dispersion;
        int v_y = dispersion * 10 - v_x;
        m.GetComponent<Rigidbody>().AddForce(v_x * plus_minus[Random.Range(0, 2)], 500, v_y * plus_minus[Random.Range(0, 2)]);
        yield return new WaitForSeconds(0.1f);
      }
      amount = amount - Money_List[i] * count;
    }
  }
}
