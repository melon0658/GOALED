using System;
using System.Reflection;
using System.Collections.Generic;

public class PlayerStetus
{
  private string playerName; //プレイヤー名
  private int money; //所持金
  private int nowPosIndex; //現在いるマス
  private string color; //プレイヤーカラー
  private string job; //職業
  private bool spouse; //配偶者
  private int child; //子供
  private int houseNumber; //持ち家の番号
  private bool checkGoal; //ゴールしているか
  public string PlayerName { get => playerName; set => playerName = value; }
  public int Money { get => money; set => money = value; }
  public int NowPosIndex { get => nowPosIndex; set => nowPosIndex = value; }
  public string Color { get => color; set => color = value; }
  public string Job { get => job; set => job = value; }
  public bool Spouse { get => spouse; set => spouse = value; }
  public int Child { get => child; set => child = value; }
  public int HouseNumber { get => houseNumber; set => houseNumber = value; }
  public bool CheckGoal { get => checkGoal; set => checkGoal = value; }

  public PlayerStetus(string playerName, int money, int nowPosIndex, string color, string job, bool spouse, int child, int houseNumber, bool checkGoal)
  {
    this.PlayerName = playerName;
    this.Money = money;
    this.NowPosIndex = nowPosIndex;
    this.Color = color;
    this.Job = job;
    this.Spouse = spouse;
    this.Child = child;
    this.HouseNumber = houseNumber;
    this.CheckGoal = checkGoal;
  }

  public GameService.PlayerData Serialize(string playerId)
  {
    var members = typeof(PlayerStetus).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
    var playerData = new GameService.PlayerData();
    foreach (var member in members)
    {
      var value = member.GetValue(this);
      var memberName = member.Name;
      var memberType = member.FieldType;
      var str = value.ToString();
      playerData.Key.Add(memberName);
      playerData.Value.Add(str);
    }
    playerData.Id = playerId;
    return playerData;
  }

  public void Deserialize(Dictionary<string, string> playerData)
  {
    var members = typeof(PlayerStetus).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
    foreach (var member in members)
    {
      var memberName = member.Name;
      if (!playerData.ContainsKey(memberName))
      {
        continue;
      }
      var memberType = member.FieldType;
      var str = playerData[memberName];
      var value = Convert.ChangeType(str, memberType);
      member.SetValue(this, value);
    }
  }

  public override bool Equals(object obj)
  {
    if (obj == null || GetType() != obj.GetType())
    {
      return false;
    }
    var other = (PlayerStetus)obj;
    return PlayerName == other.PlayerName && Money == other.Money && NowPosIndex == other.NowPosIndex && Color == other.Color && Job == other.Job && Spouse == other.Spouse && Child == other.Child && HouseNumber == other.HouseNumber && CheckGoal == other.CheckGoal;
  }

  public override int GetHashCode()
  {
    HashCode hash = new HashCode();
    hash.Add(playerName);
    hash.Add(money);
    hash.Add(nowPosIndex);
    hash.Add(color);
    hash.Add(job);
    hash.Add(spouse);
    hash.Add(child);
    hash.Add(houseNumber);
    hash.Add(checkGoal);
    return hash.ToHashCode();
  }
}