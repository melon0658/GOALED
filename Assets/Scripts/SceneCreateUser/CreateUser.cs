using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grpc.Core;

public class CreateUser : MonoBehaviour
{
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private PlayerInfo player;
  private string userName;

  void Start()
  {

  }

  void OnDestroy()
  {

  }

  public void onChangeUserName(string userName)
  {
    this.userName = userName;
  }

  public async void onClickCreateUser()
  {
    var response = await matchingServer.client.GetPlayerIdAsync(new MatchingService.GetPlayerIdRequest { });
    player.player.Id = response.PlayerId;
    player.player.Name = userName;
    Debug.Log(player.player);
    SceneManager.LoadScene("SelectRoomScene");
  }
}
