using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public string objName;

    private bool checkPoint = false;

    public bool GetCheckPoint()
    {
        return checkPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Q�[���I�u�W�F�N�g���m���ڐG�����^�C�~���O�Ŏ��s
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        // �����Փ˂�������I�u�W�F�N�g�̖��O��"Cube"�Ȃ��
        if (other.tag == "CheckPoint")
        {
            // �Փ˂�������I�u�W�F�N�g��Renderer�R���|�[�l���g��material�̐F�����ɕύX����
            Debug.Log("true");
            checkPoint = true;
        }
        else
        {
            Debug.Log("false");
            
            checkPoint = false;
            Debug.Log(checkPoint);
        }
    }

}
