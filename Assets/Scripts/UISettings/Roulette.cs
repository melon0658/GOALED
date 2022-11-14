using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Roulette : MonoBehaviour
{
    public GameObject roulette;
    public Slider slider;
    public GameObject button;
    public GameObject rightButton;
    public GameObject leftButton;

    //仮で車のマテリアルを戻す
    public GameObject car1;
    public Material carMaterial;

    private int ang;
    private bool maxValue;
    private bool isClicked;
    private bool rotate;
    private float speed;
    private float slowDownSpeed = 0.99f;

    //public GameObject sphere;

    public Action actionScript;

    public MovementBaseScript mbScript;

    private EventSystem eventSystemScript;

    public int Rcount()
    {
        return ang;
    }

    public void SetisClicked()
    {
        isClicked = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // 30fpsに設定

        slider.value = 0;
        maxValue = false;
        isClicked = false;
        rotate = false;

        eventSystemScript = GameObject.Find("EventScripts").GetComponent<EventSystem>();
    }

    public void PowerBarStart()
    {
        if(button.GetComponent<Button>().interactable == false)
        {
            button.GetComponent<Button>().interactable = true;
        }

        InvokeRepeating("MoveBar", 0.0f, 0.015f);
    }

    void MoveBar()
    {
        AutoMoveBar();
    }

    //目的地まで自動で移動
    public void AutoMoveBar()
    {
        if (!isClicked)
        {
            //5に達した場合と、0に戻った場合のフラグ切替え
            if (slider.value == 1)
            {
                maxValue = true;
            }

            if (slider.value == 0)
            {
                maxValue = false;
            }

            //フラグによるスライダー値の増減
            if (maxValue)
            {
                slider.value -= 0.04f;
            }
            else
            {
                slider.value += 0.04f;
            }
        }

        if (rotate)
        {
            roulette.transform.Rotate(0, speed, 0);
            speed *= slowDownSpeed;
            slowDownSpeed -= 0.001f * Time.deltaTime;

            if (speed < 0.01f)
            {
                rotate = false;
                count();
            }
        }
    }

    public void OnClick()
    {
        isClicked = true;
        rotate = true;
        button.GetComponent<Button>().interactable = false;
        speed = (1 - slider.value) * 40;
    }

    public void count()
    {
        // マテリアルの付け替え
        //car1.GetComponent<Renderer>().material = this.carMaterial;

        CancelInvoke("MoveBar");
        //Debug.Log("終わり");

        if (roulette.transform.localEulerAngles.y > 0)
        {
            ang = (int)roulette.transform.localEulerAngles.y;
        }
        else
        {
            ang = (int)(-1.0 * roulette.transform.localEulerAngles.y + 180);
        }
        ang = 10 - ang / 36;
        TextMeshProUGUI tt = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
        tt.text = ang.ToString();
        
        //ゴールしているかチェックしてしていたらEventSystemに直に飛ぶ
        if (car1.GetComponent<Player>().CheckGoal)
        {
          eventSystemScript.EventExecutionManager();
        }
        else
        {
            mbScript.SetEndPoth();
            mbScript.moveStart();
        }
        
    }

    
}
