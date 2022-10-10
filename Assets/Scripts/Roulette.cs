using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public GameObject roulette;
    public Slider slider;
    public GameObject button;
    public GameObject rightButton;
    public GameObject leftButton;
    private int ang;
    private bool maxValue;
    private bool isClicked;
    private bool rotate;
    private float speed;
    private float slowDownSpeed = 0.99f;

    public GameObject sphere;

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
    }

    // Update is called once per frame
    void Update()
    {
        //クリックされていなければ実行
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
            //Debug.Log(slider.value);
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
        Debug.Log("stop!!!!!!!!!!!!");
        isClicked = true;
        rotate = true;
        speed = (1 - slider.value) * 40;
    }

    public void count()
    {
        if(roulette.transform.localEulerAngles.y > 0)
        {
            ang = (int)roulette.transform.localEulerAngles.y;
        }
        else
        {
            ang = (int)(-1.0 * roulette.transform.localEulerAngles.y + 180);
        }
        ang = 10 - ang / 36;
        Text tt = GameObject.Find("Text(Legacy)").GetComponent<Text>();
        tt.text = ang.ToString();
        //button.SetActive(false);
        rightButton.SetActive(true);
        leftButton.SetActive(true);

    }

    
}
