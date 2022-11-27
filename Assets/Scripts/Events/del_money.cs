using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class del_money : MonoBehaviour
{
    void OnTriggerEnter(Collider collider){
        string target = collider.gameObject.name;
        if(!(target.Contains("pre_money"))){
            Destroy(this.gameObject);
        }
    }
}
