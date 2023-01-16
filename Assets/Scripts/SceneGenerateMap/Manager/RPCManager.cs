using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RPCManager : MonoBehaviour
{
  [CustomRPC]
  public void TestRPC(string id)
  {
    // Debug.Log("TestRPC: " + id);
  }

  [CustomRPC]
  public void ResolveTurn(string id, string step, Direction direction)
  {
    // Debug.Log("MoveCar: " + id + ", " + step);
    GameManager.instance.ResolveTurn(id, step, direction);
  }

  [CustomRPC]
  public void ActiveTurn(string id)
  {
    // Debug.Log("ActiveTurn: " + id);
    GameManager.instance.ActiveTurn(id);
  }

  [CustomRPC]
  public void Refresh()
  {
    if (GameManager.instance.playerInfo.isRoomOwner)
    {
      SyncManager.instance.isRefreshPlayerData = true;
      SyncManager.instance.isRefreshObject = true;
      Debug.Log("Refresh");
    }
  }

  [CustomRPC]
  public void EventStart(string tileIDs, string playerID)
  {
    GameManager.instance.EventStart(tileIDs, playerID);
  }
}
