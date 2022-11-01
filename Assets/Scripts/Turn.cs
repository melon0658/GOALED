using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    // Start is called before the first frame update

    public object a1;
    Rigidbody rigid;

    void Start()
    {
        Debug.Log("aaaa");
        //rigid = GameObject.Find("R1").GetComponent<Rigidbody>();
        //rigid.transform.eulerAngles = new Vector3(0f, 120.0f * Time.deltaTime, 0f); // ìØè„
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 120.0f * Time.deltaTime, 0f);
    }
}
