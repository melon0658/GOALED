using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Player", menuName = "Player", order = 1)]
public class PlayerInfo : ScriptableObject, ISerializationCallbackReceiver
{
  [NonSerialized] public MatchingService.Player player;

  public void OnEnable()
  {
    if (player == null)
    {
      player = new MatchingService.Player();
    }
  }

  public void OnBeforeSerialize()
  {

  }

  public void OnAfterDeserialize()
  {

  }
}

