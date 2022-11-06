public abstract class Event
{
  public abstract string GetEventName();
  public abstract void OnEvent(Player player);
}