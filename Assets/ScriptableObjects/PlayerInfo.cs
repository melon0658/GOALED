using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Player", menuName = "Player", order = 1)]
public class PlayerInfo : ScriptableObject, ISerializationCallbackReceiver
{
  [NonSerialized] public MatchingService.Player player;
  [NonSerialized] public bool isRoomOwner;
  [NonSerialized] public string password;

  public void OnEnable()
  {
    if (player == null)
    {
      player = new MatchingService.Player();
      isRoomOwner = false;
      password = "";
    }
  }

  public void OnBeforeSerialize()
  {

  }

  public void OnAfterDeserialize()
  {

  }
}

