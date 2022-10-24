using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string plyaerName; //プレイヤー名
    private int money; //所持金
    private int nowPosIndex; //現在いるマス
    private string color; //プレイヤーカラー
    private string job; //職業
    private string spouse; //配偶者
    private string child; //子供
    private int houseNumber; //持ち家の番号

    public Player(string plyaerName, int money, int nowPosIndex, string color, string job, string spouse, string child, int houseNumber)
    {
        this.PlyaerName = plyaerName;
        this.Money = money;
        this.NowPosIndex = nowPosIndex;
        this.Color = color;
        this.Job = job;
        this.Spouse = spouse;
        this.Child = child;
        this.HouseNumber = houseNumber;
    }

    public string PlyaerName { get => plyaerName; set => plyaerName = value; }
    public int Money { get => money; set => money = value; }
    public int NowPosIndex { get => nowPosIndex; set => nowPosIndex = value; }
    public string Color { get => color; set => color = value; }
    public string Job { get => job; set => job = value; }
    public string Spouse { get => spouse; set => spouse = value; }
    public string Child { get => child; set => child = value; }
    public int HouseNumber { get => houseNumber; set => houseNumber = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
