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
  private bool isFinished = false;
  // private AsyncServerStreamingCall<Multiplay.SyncRoomUsersResponse> call;
  void Start()
  {
    SyncRoom();
    listenGameStart();
  }

  void Update()
  {

  }

  void onDestroy()
  {
  }

  private async void SyncRoom()
  {
    while (!isFinished)
    {
      var response = await matchingServer.client.GetRoomDetailAsync(new MatchingService.GetRoomDetailRequest { RoomId = playerInfo.player.RoomId, Password = playerInfo.password });
      var room = response.Room;
      if (room.Id == playerInfo.player.RoomId)
      {
        players = new Dictionary<string, MatchingService.Player>();
        foreach (MatchingService.Player player in room.Players)
        {
          players[player.Id] = player;
        }
        UpdateUserList();
        if (room.Owner == playerInfo.player.Id)
        {
          gameStartButton.SetActive(true);
        }
      }
      UpdateUserList();
      await Task.Delay(1000);
    }
  }

  public void UpdateUserList()
  {
    if (scrollView == null)
    {
      return;
    }
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
      if (call.ResponseStream.Current.Success)
      {
        isFinished = true;
        SceneManager.LoadScene("GenerateMapScene");
      }
    }
  }

  public async void onClickStartGame()
  {
    var response = await matchingServer.client.StartGameAsync(new MatchingService.StartGameRequest { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.Id });
  }

  public async void onClickBack()
  {
    isFinished = true;
    await matchingServer.client.LeaveRoomAsync(new MatchingService.LeaveRoomRequest { PlayerId = playerInfo.player.Id, RoomId = playerInfo.player.RoomId });
    SceneManager.LoadScene("SelectRoomScene");
  }
}
