using UnityEngine;
using UnityEngine.SceneManagement;
using Grpc.Core;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RoomDetail : MonoBehaviour
{
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private GameObject scrollView;
  [SerializeField] private GameObject userDetail;
  [SerializeField] private GameObject gameStartButton;
  private Dictionary<string, MatchingService.Player> players = new Dictionary<string, MatchingService.Player>();
  // private AsyncServerStreamingCall<Multiplay.SyncRoomUsersResponse> call;
  void Start()
  {
    // channel = new Channel(serverInfo.host, serverInfo.port, ChannelCredentials.Insecure);
    // roomClient = new Multiplay.RoomService.RoomServiceClient(channel);
    SyncRoom();
    listenGameStart();
  }

  void Update()
  {

  }

  void onDestroy()
  {
    // channel.ShutdownAsync().Wait();
  }

  private async void SyncRoom()
  {
    // call = roomClient.SyncRoomUsers(new Multiplay.SyncRoomUsersRequest { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.PlayerId });
    // while (await call.ResponseStream.MoveNext())
    // {
    //   Debug.Log(call.ResponseStream.Current);
    //   var player = call.ResponseStream.Current.Player;
    //   if (call.ResponseStream.Current.IsLeft)
    //   {
    //     players.Remove(player.PlayerId);
    //   }
    //   else
    //   {
    //     players[player.PlayerId] = player;
    //   }
    //   UpdateUserList();
    // }
    while (true)
    {
      var response = await matchingServer.client.GetPublicRoomAsync(new MatchingService.GetPublicRoomRequest { });
      foreach (MatchingService.Room room in response.Rooms)
      {
        if (room.Id == playerInfo.player.RoomId)
        {
          players = new Dictionary<string, MatchingService.Player>();
          foreach (MatchingService.Player player in room.Players)
          {
            players[player.Id] = player;
          }
          UpdateUserList();
          Debug.Log(room + " " + playerInfo.player.Id);
          if (room.Owner == playerInfo.player.Id)
          {
            Debug.Log("owner");
            gameStartButton.SetActive(true);
          }
          break;
        }

      }
      UpdateUserList();
      await Task.Delay(1000);
    }
  }

  public void UpdateUserList()
  {
    foreach (Transform child in scrollView.transform)
    {
      Destroy(child.gameObject);
    }
    float width = scrollView.GetComponent<RectTransform>().rect.width;
    float height = userDetail.GetComponent<RectTransform>().rect.height;
    float y = -height / 2;
    float x = 0;
    foreach (MatchingService.Player player in players.Values)
    {
      GameObject info = Instantiate(userDetail, scrollView.transform);
      info.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
      info.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
      info.GetComponent<TextMeshProUGUI>().text = player.Name;
      y -= height;
    }
  }

  public async void listenGameStart()
  {
    var call = matchingServer.client.GetStartGameStream(new MatchingService.GetStartGameStreamRequest { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.Id });
    while (await call.ResponseStream.MoveNext())
    {
      Debug.Log(call.ResponseStream.Current);
      if (call.ResponseStream.Current.Success)
      {
        SceneManager.LoadScene("MainGameScene");
      }
    }
  }

  public async void onClickStartGame()
  {
    var response = await matchingServer.client.StartGameAsync(new MatchingService.StartGameRequest { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.Id });
  }

  public async void onClickBack()
  {
    await matchingServer.client.LeaveRoomAsync(new MatchingService.LeaveRoomRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    SceneManager.LoadScene("RoomMatchScene");
  }
}
