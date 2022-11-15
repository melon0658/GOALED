using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class CarRpc : MonoBehaviour
{
  [SerializeField] private GameObject camera;
  [SerializeField] private GameObject LeftButton;
  [SerializeField] private GameObject CenterButton;
  [SerializeField] private GameObject RightButton;
  [SerializeField] private GameObject Button;

  [CustomRPC]
  public void ActiveCamera()
  {
    camera.SetActive(true);
  }

  [CustomRPC]
  public void ActiveButton()
  {
    Button.SetActive(true);
  }
}