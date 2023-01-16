using System.Threading.Tasks;
using System.Collections.Generic;

public abstract class TileEvent
{
  public abstract string GetEventName();
  public abstract string GetEventDescription();
  public abstract void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId);
  public abstract Task OnEventAnimation();
  public abstract EventType GetEventType();
  public abstract EventEffectType GetEventEffectType();
}