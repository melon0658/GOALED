using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event82 : MonoBehaviour
{
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private TextMeshProUGUI countText;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;


  // Start is called before the first frame update
  void Start()
  {
    canvas = GameObject.Find("Canvas");

  }

  public void execution()
  {
    //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();

    //Debug.Log(turnSystemScript.GetnowTurnPlayerNum());
    //���݂̃^�[�����N�����擾���āA����ɉ����ăv���C���[�X�N���v�g���擾
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        break;
      default:
        break;
    }
    //Debug.Log(playerScript);
    //�C�x���g�ŗL

    if (playerScript.CheckGoal)
    {
      //���ɃS�[�����Ă���Ȃ烋�[���b�g�̃}�X�ځ~1000�h���������v���X
      countText = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
      int count = int.Parse(countText.text);
      playerScript.Money = playerScript.Money + (count * 1000);
      //�e�L�X�g�\��
      //Debug.Log("A");

    }
    else
    {
      playerScript.CheckGoal = true;
      //�������Ԃɉ����ď������v���X
      switch (turnSystemScript.GetgoalPlayerNum())
      {
        case 0:
          playerScript.Money = playerScript.Money + 100000;
          break;
        case 1:
          playerScript.Money = playerScript.Money + 80000;
          break;
        case 2:
          playerScript.Money = playerScript.Money + 50000;
          break;
        case 3:
          playerScript.Money = playerScript.Money + 10000;
          break;
        default:
          break;
      }
      //�S�[�������l���𑝂₷
      int goalPlayerNum = turnSystemScript.GetgoalPlayerNum() + 1;
      turnSystemScript.SetgoalPlayerNum(goalPlayerNum);
      //�e�L�X�g�\��
      //Debug.Log("B");

    }

    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("�����ɃC�x���g�e�L�X�g��\��t��"); 

    textDialogManegerScript.HiddentextDialogBox();

    //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
    turnSystemScript.TurnEndSystemMaster(); //�^�[�����I��
  }
}

