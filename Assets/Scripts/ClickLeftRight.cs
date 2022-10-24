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

    //仮で車のマテリアルを戻す
    public GameObject car1;
    public Material carMaterial;

    public PathCreator pathLeft;
    public PathCreator pathRight;
    public MovementBaseScript mbScript;
    public Roulette rScript;

    // Start is called before the first frame update
    void Start()
    {
        stopButton.GetComponent<Button>().interactable = false;
        //左右ボタンを隠す
        //leftButton.SetActive(false);
        //rightButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeft()
    {
        //仮で車のマテリアルを戻す
        car1.GetComponent<Renderer>().material = this.carMaterial;

        Debug.Log("Left");
        stopButton.GetComponent<Button>().interactable = true;
        mbScript.SetPathCreator(pathLeft);
        rScript.PowerBarStart();
        HiddenLR();
        
    }

    public void OnClickRight()
    {
        //仮で車のマテリアルを戻す
        car1.GetComponent<Renderer>().material = this.carMaterial;

        Debug.Log("Right");
        stopButton.GetComponent<Button>().interactable = true;
        mbScript.SetPathCreator(pathRight);
        rScript.PowerBarStart();
        HiddenLR();
    }

    void HiddenLR()
    {
        //左右ボタンを隠す
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        //mbScript.moveStart();
    }
}
