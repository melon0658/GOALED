using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RPCManager : MonoBehaviour
{
  [CustomRPC]
  public void TestRPC(string id)
  {
    Debug.Log("TestRPC: " + id);
  }

  [CustomRPC]
  public void MoveCamera(string id)
  {
    Debug.Log("MoveCamera: " + id);
    GameManager.instance.MoveCamera(id);
  }

  [CustomRPC]
  public void MoveCar(string id, string step)
  {
    Debug.Log("MoveCar: " + id + ", " + step);
    GameManager.instance.MoveCar(id, step);
  }
}
