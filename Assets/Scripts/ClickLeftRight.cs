using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine.UI;
using UnityEngine;

public class ClickLeftRight : MonoBehaviour
{
    public GameObject stopButton;
    public GameObject rightButton;
    public GameObject leftButton;
    public PathCreator pathLeft;
    public PathCreator pathRight;
    public MovementBaseScript mbScript;

    // Start is called before the first frame update
    void Start()
    {
        //stopButton.GetComponent<Button>().interactable = false;
        //左右ボタンを隠す
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeft()
    {
        Debug.Log("Left");
        stopButton.GetComponent<Button>().interactable = true;
        mbScript.SetPathCreator(pathLeft);
        HiddenLR();
        
    }

    public void OnClickRight()
    {
        Debug.Log("Right");
        stopButton.GetComponent<Button>().interactable = true;
        mbScript.SetPathCreator(pathRight);
        HiddenLR();
    }

    void HiddenLR()
    {
        //左右ボタンを隠す
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        mbScript.moveStart();
    }
}
