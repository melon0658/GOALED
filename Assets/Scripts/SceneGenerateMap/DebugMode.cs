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

  private void Debugmode()
  {
    // var res = await matchingServer.client.GetPlayerIdAsync(new MatchingService.GetPlayerIdRequest());
    var res = matchingServer.client.GetPlayerId(new MatchingService.GetPlayerIdRequest());
    playerInfo.player.Id = res.PlayerId;
    playerInfo.player.Name = "test";
    if (isDebugModeOwner)
    {
      // var room = await matchingServer.client.CreatePublicRoomAsync(new MatchingService.CreatePublicRoomRequest() { Name = "test", Owner = playerInfo.player.Id, MaxPlayer = 4 });
      var room = matchingServer.client.CreatePublicRoom(new MatchingService.CreatePublicRoomRequest() { Name = "test", Owner = playerInfo.player.Id, MaxPlayer = 4 });
      playerInfo.player.RoomId = room.Room.Id;
      // await matchingServer.client.JoinPublicRoomAsync(new MatchingService.JoinPublicRoomRequest() { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
      matchingServer.client.JoinPublicRoom(new MatchingService.JoinPublicRoomRequest() { RoomId = playerInfo.player.RoomId, Player = playerInfo.player });
      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(1000);
      }
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
      // var call = matchingServer.client.GetStartGameStream(new MatchingService.GetStartGameStreamRequest { RoomId = playerInfo.player.RoomId, PlayerId = playerInfo.player.Id });
      // var task = Task.Run(async () =>
      // {
      //   await call.ResponseStream.MoveNext();
      // });
      // task.Wait();
      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(1000);
      }
      Debug.Log("join room");
    }
  }


  void Start()
  {
    Debugmode();
    playerInfo.isRoomOwner = isDebugModeOwner;
    Debug.Log(playerInfo.player.Id);
  }
}
