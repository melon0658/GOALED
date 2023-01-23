using UnityEngine;
using UnityEngine.SceneManagement;
using Grpc.Core;
using System.Text.RegularExpressions;

public class CreatePrivateRoom : MonoBehaviour
{
  private string password;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private PlayerInfo playerInfo;
  void Start()
  {
  }

  void OnDestroy()
  {
  }

  private bool passwordCheck(string password)
  {
    if (password.Length < 8)
    {
      Debug.Log("Password is too short");
      return false;
    }
    if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
    {
      Debug.Log("Password must be half-width alphanumeric");
      return false;
    }
    return true;
  }

  public void onChangePassword(string password)
  {
    this.password = password;
  }

  public async void onClickCreateRoom()
  {
    if (!passwordCheck(password))
    {
      return;
    }
    var _password = password;
    var response = await matchingServer.client.CreatePrivateRoomAsync(new MatchingService.CreatePrivateRoomRequest { Name = "", Password = _password, Owner = playerInfo.player.Id, MaxPlayer = 4 });
    playerInfo.player.RoomId = response.Room.Id;
    playerInfo.password = _password;
    await matchingServer.client.JoinPrivateRoomAsync(new MatchingService.JoinPrivateRoomRequest { Player = playerInfo.player, Password = _password });
    SceneManager.LoadScene("RoomDetailScene");
  }

  public void onClickBack()
  {
    SceneManager.LoadScene("SelectRoomScene");
  }
}
