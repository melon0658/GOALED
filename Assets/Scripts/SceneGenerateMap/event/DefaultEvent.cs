public class DefaultEvent : Event
{
  public override string GetEventName()
  {
    return "empty";
  }

  public override void OnEvent(Player player)
  {
    return;
  }
}