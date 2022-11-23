using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingApper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SettingActive()
    {
        gameObject.SetActive (true);//表示
    }
    public void SettingHide()
    {
        gameObject.SetActive (false);//非表示
    }
}
