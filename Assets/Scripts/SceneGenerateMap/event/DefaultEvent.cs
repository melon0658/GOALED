using System.Threading.Tasks;
using System.Collections.Generic;

public class DefaultEvent : TileEvent
{
  public override EventType GetEventType()
  {
    return EventType.EVENT_STOP;
  }

  public override EventEffectType GetEventEffectType()
  {
    return EventEffectType.EventEffectType_Stetus;
  }

  public override string GetEventName()
  {
    return "empty";
  }

  public override string GetEventDescription()
  {
    return "empty";
  }

  public override void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId)
  {
    return;
  }

  public override async Task OnEventAnimation()
  {
    await Task.Delay(1000);
  }
}