using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Grpc.Core;

public class DebugMode : MonoBehaviour
{
  [SerializeField] private PlayerInfo playerInfo;
  [SerializeField] private MatchingServer matchingServer;
  [SerializeField] private bool isDebugMode = false;
  [SerializeField] private bool isDebugModeOwner = false;
  [SerializeField] private int waitTime = 5;

  private void Debugmode()
  {
    var res = matchingServer.client.GetPlayerId(new MatchingService.GetPlayerIdRequest());
    playerInfo.player.Id = res.PlayerId;
    playerInfo.player.Name = "test";
    if (isDebugModeOwner)
    {
      var room = matchingServer.client.CreatePublicRoom(new MatchingService.CreatePublicRoomRequest() { Name = "test", Owner = playerInfo.player.Id, MaxPlayer = 4 });
      playerInfo.player.RoomId = room.Room.Id;
      matchingServer.client.JoinPublicRoom(new MatchingService.JoinPublicRoomRequest() { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
      Thread.Sleep(1000 * waitTime);
      matchingServer.client.StartGame(new MatchingService.StartGameRequest() { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.Id });
    }
    else
    {
      var rooms = matchingServer.client.GetPublicRooms(new MatchingService.GetPublicRoomsRequest());
      foreach (var room in rooms.Rooms)
      {
        if (room.Name == "test")
        {
          playerInfo.player.RoomId = room.Id;
          break;
        }
      }
      if (playerInfo.player.RoomId == "")
      {
        Debug.Log("room not found");
        return;
      }
      matchingServer.client.JoinPublicRoom(new MatchingService.JoinPublicRoomRequest() { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
      Thread.Sleep(1000 * waitTime);
    }
  }

  void Start()
  {
    Debugmode();
    playerInfo.isRoomOwner = isDebugModeOwner;
    Debug.Log(playerInfo.player.Id);
  }
}
