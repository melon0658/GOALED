using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Room", menuName = "Room", order = 1)]
public class RoomInfo : ScriptableObject, ISerializationCallbackReceiver
{
  [NonSerialized] public MatchingService.Room room;

  public void OnEnable()
  {
    if (room == null)
    {
      room = new MatchingService.Room();
    }
  }

  public void OnBeforeSerialize()
  {

  }

  public void OnAfterDeserialize()
  {

  }
}

