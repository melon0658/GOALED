using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class del_money : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
        string target = collision.gameObject.name;
        if(!(target.Contains("pre_money(Clone)"))){
            Destroy(this.gameObject);
        }
    }
}
