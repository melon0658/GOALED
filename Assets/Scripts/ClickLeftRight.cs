using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine.UI;
using UnityEngine;

public class ClickLeftRight : MonoBehaviour
{
    public GameObject stopButton;
    public GameObject upButton;
    public GameObject rightButton;
    public GameObject leftButton;

    //仮で車のマテリアルを戻す
    public GameObject car1;
    public Material carMaterial;

    public PathCreator pathStartLeft;
    public PathCreator pathStartRight;
    public PathCreator pathSecondLeft;
    public PathCreator pathSecondUp;
    public PathCreator pathLastLeft;
    public PathCreator pathLastUp;

    public MovementBaseScript mbScript;
    public Roulette rScript;
    public Action ActionScript;

    // Start is called before the first frame update
    void Start()
    {
        stopButton.GetComponent<Button>().interactable = false;
        upButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickUp()
    {
        //仮で車のマテリアルを戻す
        car1.GetComponent<Renderer>().material = this.carMaterial;

        //Debug.Log("Up");
        stopButton.GetComponent<Button>().interactable = true;
        if (ActionScript.GetCheckPointName() == "CheckPosition2")
        {
            mbScript.SetPathCreator(pathSecondUp);
        }
        else
        {
            mbScript.SetPathCreator(pathLastUp);
        }
        
        rScript.PowerBarStart();
        HiddenLU();

    }

    public void OnClickLeft()
    {
        //仮で車のマテリアルを戻す
        car1.GetComponent<Renderer>().material = this.carMaterial;

        //Debug.Log("Left");
        stopButton.GetComponent<Button>().interactable = true;
        if (ActionScript.GetCheckPointName() == "CheckPosition")
        {
            mbScript.SetPathCreator(pathStartLeft);
            HiddenLR();
        }
        else if (ActionScript.GetCheckPointName() == "CheckPosition2")
        {
            mbScript.SetPathCreator(pathSecondLeft);
            HiddenLU();
        }
        else
        {
            mbScript.SetPathCreator(pathLastLeft);
            HiddenLU();
        }
        
        rScript.PowerBarStart();
    }

    public void OnClickRight()
    {
        //仮で車のマテリアルを戻す
        car1.GetComponent<Renderer>().material = this.carMaterial;

        //Debug.Log("Right");
        stopButton.GetComponent<Button>().interactable = true;
        mbScript.SetPathCreator(pathStartRight);
        rScript.PowerBarStart();
        HiddenLR();
    }

    void HiddenLR()
    {
        //左ボタンと右ボタンを隠す
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    void HiddenLU()
    {
        //左ボタンと上ボタンを隠す
        leftButton.SetActive(false);
        upButton.SetActive(false);
    }
}
