public static class Util
{
  public static void AddPlayerData(GameService.PlayerData playerData, string key, string value)
  {
    playerData.Key.Add(key);
    playerData.Value.Add(value);
  }
}