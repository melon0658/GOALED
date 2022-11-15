using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Grpc.Core;
using TMPro;

public class RoomMatch : MonoBehaviour
{
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private GameObject RoomInfo;
  [SerializeField] private GameObject ScrollView;
  private GameObject selectedRoom = null;
  void Start()
  {
    // channel = new Channel(serverInfo.host, serverInfo.port, ChannelCredentials.Insecure);
    // roomClient = new Multiplay.RoomService.RoomServiceClient(channel);
    // Debug.Log(playerInfo.player);
    onClickReloadRoom();
  }

  void OnDestroy()
  {
    // channel.ShutdownAsync().Wait();
  }

  public void TurnSelectRoom(GameObject room)
  {
    if (selectedRoom != null)
    {
      selectedRoom.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
    selectedRoom = room;
    room.GetComponent<TextMeshProUGUI>().color = Color.red;
  }

  public void DisplayRoomList(Google.Protobuf.Collections.RepeatedField<MatchingService.Room> rooms)
  {
    foreach (Transform child in ScrollView.transform)
    {
      Destroy(child.gameObject);
    }
    float width = ScrollView.GetComponent<RectTransform>().rect.width;
    float height = RoomInfo.GetComponent<RectTransform>().rect.height;
    float y = -height / 2;
    float x = 0;
    foreach (MatchingService.Room room in rooms)
    {
      GameObject roomInfo = Instantiate(RoomInfo, ScrollView.transform);
      roomInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
      roomInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
      roomInfo.GetComponent<TextMeshProUGUI>().text = room.Id + " " + room.CurrentPlayer + "/" + room.MaxPlayer;
      roomInfo.GetComponent<Button>().onClick.AddListener(() => { TurnSelectRoom(roomInfo); });
      y -= height;
    }
  }

  public async void onClickReloadRoom()
  {
    var response = await matchingServer.client.GetPublicRoomAsync(new MatchingService.GetPublicRoomRequest { });
    DisplayRoomList(response.Rooms);
  }

  public async void onClickCreateRoom()
  {
    // Debug.Log(playerInfo.player);
    var response = await matchingServer.client.CreatePublicRoomAsync(new MatchingService.CreatePublicRoomRequest { Name = "test", MaxPlayer = 4, Owner = playerInfo.player.Id });
    // var res = await roomClient.JoinRoomAsync(new Multiplay.JoinRoomRequest { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
    // SceneManager.LoadScene("MainGameScene");
    // SceneManager.LoadScene("RoomDetailScene");
  }

  public async void onClickJoinRoom()
  {
    playerInfo.player.RoomId = selectedRoom.GetComponent<TextMeshProUGUI>().text.Split(' ')[0];
    var response = await matchingServer.client.JoinPublicRoomAsync(new MatchingService.JoinPublicRoomRequest { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
    // SceneManager.LoadScene("MainGameScene");
    SceneManager.LoadScene("RoomDetailScene");
  }

  public void onClickBack()
  {
    SceneManager.LoadScene("SelectRoomScene");
  }
}
