using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrandAnimation : MonoBehaviour
{
    private bool active;
    void Start()
    {
        active = false;
        InvokeRepeating("BackGraundActive", 1, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void BackGraundActive(){
        if (active == true){
                // ball.GetComponent<BackGrandAnimation>().BackGraundActive();
                gameObject.SetActive (true);//表示
                active = false;
            }else{
                gameObject.SetActive (false);//非表示
                active = true;
            }
    }
}
