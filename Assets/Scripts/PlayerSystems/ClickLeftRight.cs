using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine.UI;
using UnityEngine;

//ルート選択を処理するスクリプト
public class ClickLeftRight : MonoBehaviour
{
    public GameObject stopButton;
    public GameObject upButton;
    public GameObject rightButton;
    public GameObject leftButton;

    //仮で車のマテリアルを戻す
    public GameObject car1;
    public Material carMaterial;

    //パスクリエイター用変数
    private PathCreator pathStartLeft;
    private PathCreator pathStartRight;
    private PathCreator pathSecondLeft;
    private PathCreator pathSecondUp;
    private PathCreator pathLastLeft;
    private PathCreator pathLastUp;

    public MovementBaseScript mbScript;
    public Roulette rScript;
    public Action actionScript;

    // Start is called before the first frame update
    void Start()
    {
      //パスクリエイターを取得して設定
      pathStartLeft = GameObject.Find("StartLeft").GetComponent<PathCreator>();
      pathStartRight = GameObject.Find("StartRight").GetComponent<PathCreator>();
      pathSecondLeft = GameObject.Find("SecondLeft").GetComponent<PathCreator>();
      pathSecondUp = GameObject.Find("SecondUp").GetComponent<PathCreator>();
      pathLastLeft = GameObject.Find("LastLeft").GetComponent<PathCreator>();
      pathLastUp = GameObject.Find("LastUp").GetComponent<PathCreator>();

      stopButton.GetComponent<Button>().interactable = false;
      //upButton.SetActive(false);
    }

    public void OnClickUp()
    {
        //仮で車のマテリアルを戻す
        //car1.GetComponent<Renderer>().material = this.carMaterial;

        //Debug.Log("Up");
        stopButton.GetComponent<Button>().interactable = true;
        if (actionScript.GetCheckPointName() == "CheckPosition2")
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
        //car1.GetComponent<Renderer>().material = this.carMaterial;

        //Debug.Log("Left");
        stopButton.GetComponent<Button>().interactable = true;
        if (actionScript.GetCheckPointName() == "CheckPosition")
        {
            mbScript.SetPathCreator(pathStartLeft);
            HiddenLR();
        }
        else if (actionScript.GetCheckPointName() == "CheckPosition2")
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
        //car1.GetComponent<Renderer>().material = this.carMaterial;

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
