using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GetJobEvent : TileEvent
{
  private string eventName;
  private string eventDescription;
  private string job;
  private int money;


  public GetJobEvent(string eventName, string eventDescription, string job, int money)
  {
    this.eventName = eventName;
    this.eventDescription = eventDescription;
    this.job = job;
    this.money = money;
  }

  public override EventEffectType GetEventEffectType()
  {
    return EventEffectType.EventEffectType_Stetus;
  }

  public override EventType GetEventType()
  {
    return EventType.EVENT_STOP;
  }

  public override string GetEventName()
  {
    return eventName;
  }

  public override string GetEventDescription()
  {
    return eventDescription;
  }

  public override void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId)
  {
    playerStetus[triggerPlayerId].Job = job;
  }

  public override async Task OnEventAnimation()
  {
    Debug.Log("GetJobEvent");
    await Task.Delay(1000);
  }
}