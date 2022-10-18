using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    Button button;
    bool isCalledOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("down")) && (!isCalledOnce)) {
            isCalledOnce = true;
            Debug.Log("Start");
            button = GameObject.Find("Canvas/Start_button").GetComponent<Button>();
            //ボタンが選択された状態になる
            button.Select();
        }
    }
}
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //UI使うときに必要
// using UnityEngine.UI;

// public class Choice : MonoBehaviour
// {
//     Button button;

//     void Start()
//     {
//         button = GameObject.Find("Canvas/Start_button").GetComponent<Button>();
//         //ボタンが選択された状態になる
//         button.Select();
//     }
// }