using UnityEngine;
using UnityEngine.SceneManagement;
using Grpc.Core;
using System.Text.RegularExpressions;

public class CreatePrivateRoom : MonoBehaviour
{
  //     private Channel channel;
  //     private Multiplay.RoomService.RoomServiceClient roomClient;
  //     private string password;
  //     [SerializeField] private ServerInfo serverInfo;
  //     [SerializeField] private PlayerInfo playerInfo;
  //     void Start()
  //     {
  //         channel = new Channel(serverInfo.host, serverInfo.port, ChannelCredentials.Insecure);
  //         roomClient = new Multiplay.RoomService.RoomServiceClient(channel);
  //     }

  //     void OnDestroy()
  //     {
  //         channel.ShutdownAsync().Wait();
  //     }

  //     private bool passwordCheck(string password)
  //     {
  //         if (password.Length < 8)
  //         {
  //             Debug.Log("Password is too short");
  //             return false;
  //         }
  //         if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
  //         {
  //             Debug.Log("Password must be half-width alphanumeric");
  //             return false;
  //         }
  //         return true;
  //     }

  //     public void onChangePassword(string password)
  //     {
  //         this.password = password;
  //     }

  //     public async void onClickCreateRoom()
  //     {
  //         if (!passwordCheck(password))
  //         {
  //             return;
  //         }
  //         var _password = password;
  //         var response = await roomClient.CreatePrivateRoomAsync(new Multiplay.CreatePrivateRoomRequest { PlayerId = playerInfo.player.PlayerId, RoomId = _password, RoomName = "private" });
  //         playerInfo.player.RoomId = _password;
  //         Debug.Log("Create private room success");
  //         var res = await roomClient.JoinRoomAsync(new Multiplay.JoinRoomRequest { RoomId = _password, Player = playerInfo.player });
  //         SceneManager.LoadScene("RoomDetailScene");
  //     }

      public void onClickBack()
      {
          SceneManager.LoadScene("SelectRoomScene");
      }
}
