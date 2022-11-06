public class GetMoney : Event
{
  public override string GetEventName()
  {
    return "GetMoney";
  }

  public override void OnEvent(Player player)
  {
    player.Money += 100;
  }
}