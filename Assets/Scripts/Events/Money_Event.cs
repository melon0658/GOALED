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
    private int[] Money_List = {100000, 50000, 20000, 10000, 5000, 1000};
    private int[] plus_minus = {-1, 1};

    public void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public void Update(){
        if (Input.GetKey (KeyCode.W)) {
            getMoney(new Vector3(65, 409, -170), 1138000);
        }
    }

    public void getMoney(Vector3 p, int amount){
        StartCoroutine(GetM(p, amount));
    }

    public void lostMoney(Vector3 p, int amount){
        StartCoroutine(LostM(p, amount));
    }

    private IEnumerator GetM(Vector3 p, int amount){
        GameObject pm = (GameObject)Resources.Load("pre_money");
        for(int i=0;i<6;i++){
            int count = amount/Money_List[i];
            for(int j=0; j<count; j++){
                GameObject m = Instantiate(pm, new Vector3(p.x,p.y+plus_height,p.z), Quaternion.Euler(0, this.transform.localEulerAngles.y, 0), this.transform);
                m.GetComponent<MeshRenderer>().material = Money_Material[i];
                yield return new WaitForSeconds(0.2f);
            }
            amount=amount-Money_List[i]*count;
        }
    }

    private IEnumerator LostM(Vector3 p, int amount){
        GameObject pm = (GameObject)Resources.Load("pre_money");
        for(int i=0;i<6;i++){
            int count = amount/Money_List[i];
            for(int j=0; j<count; j++){
                GameObject m = Instantiate(pm, new Vector3(p.x,p.y+minus_height,p.z), Quaternion.Euler(0, this.transform.localEulerAngles.y, 0), this.transform);
                m.GetComponent<MeshRenderer>().material = Money_Material[i];
                int v_x = Random.Range(1, 10)*dispersion;
                int v_y = dispersion*10-v_x;
                m.GetComponent<Rigidbody>().AddForce(v_x*plus_minus[Random.Range(0, 2)], 500, v_y*plus_minus[Random.Range(0, 2)]);
                yield return new WaitForSeconds(0.1f);
            }
            amount=amount-Money_List[i]*count;
        }
    }
}
