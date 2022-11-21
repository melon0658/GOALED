using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventTest100 : MonoBehaviour
{
  //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
  private TurnSystem turnSystemScript;
  private Player playerScript;
  private GameObject canvas;
  private TextDialogManager textDialogManegerScript;

  //�C�x���g�ŗL
  private MovementBaseScript moveScript;

  // Start is called before the first frame update
  void Start()
  {
    //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
    canvas = GameObject.Find("Canvas");
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  public void execution()
  {
    //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
    turnSystemScript = GameObject.Find("GameScripts").GetComponent<TurnSystem>();
    //���݂̃^�[�����N�����擾���āA����ɉ����ăv���C���[�X�N���v�g���擾
    switch (turnSystemScript.GetnowTurnPlayerNum())
    {
      case 1:
        playerScript = GameObject.Find("defaultCar1").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar1").GetComponent<MovementBaseScript>();
        break;
      case 2:
        playerScript = GameObject.Find("defaultCar2").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar2").GetComponent<MovementBaseScript>();
        break;
      case 3:
        playerScript = GameObject.Find("defaultCar3").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar3").GetComponent<MovementBaseScript>();
        break;
      case 4:
        playerScript = GameObject.Find("defaultCar4").GetComponent<Player>();
        moveScript = GameObject.Find("defaultCar4").GetComponent<MovementBaseScript>();
        break;
      default:
        break;
    }

    //�C�x���g�ŗL
    textDialogManegerScript = canvas.transform.Find("TextDialogBox").GetComponent<TextDialogManager>();
    textDialogManegerScript.ShowtextDialogBox();
    textDialogManegerScript.SetdialogText("�W���u����");

    StartCoroutine("sleep");

    //player�̂������擾���ĕύX�������I(get��set�����̏�����)
    //playerScript.Money = playerScript.Money + 10000;
  }

  private IEnumerator sleep()
  {
    //�C�x���g�ŗL
    yield return new WaitForSeconds(1f);  //1�b�҂�
    textDialogManegerScript.HiddentextDialogBox();

    moveScript.jobEventAfterMove();

    //�ǂ̃C�x���g�ɂ��K�v�Ȃ��
    //turnSystemScript.TurnEndSystemMaster(); //�^�[�����I��
  }
}
