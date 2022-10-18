using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectRoom : MonoBehaviour
{
  [SerializeField] private PlayerInfo playerInfo;
  public void onClickPrivateMatch()
  {
    SceneManager.LoadScene("CreatePrivateRoomScene");
  }

  public void onClickRoomMatch()
  {
    SceneManager.LoadScene("RoomMatchScene");
  }

  public void onClickBack()
  {
    SceneManager.LoadScene("CreateUserScene");
  }
}
