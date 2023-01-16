using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PayDayEvent : TileEvent
{
  private Dictionary<string, int> moneyTable = new Dictionary<string, int>()
  {
    { "建築家", 60000 },
    { "弁護士", 65000 },
    { "医者", 70000 },
    { "プロゲーマー", 20000 },
    { "エンジニア", 25000 },
    { "作家", 30000 },
    { "アスリート", 35000 },
    { "パイロット", 40000 },
    { "パティシエ", 45000 },
    { "科学者", 50000 },
    { "俳優", 55000 },
    { "フリーター", 10000}
  };

  public override EventType GetEventType()
  {
    return EventType.EVENT_PASS;
  }

  public override EventEffectType GetEventEffectType()
  {
    return EventEffectType.EventEffectType_Stetus;
  }

  public override string GetEventName()
  {
    return "給料日";
  }

  public override string GetEventDescription()
  {
    return "給料日です。";
  }

  public override void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId)
  {
    playerStetus[triggerPlayerId].Money += moneyTable[playerStetus[triggerPlayerId].Job];
  }

  public override async Task OnEventAnimation()
  {
    // var videoUI = GameObject.Find("VideoPlayer");
    // videoUI.transform.localScale = Vector3.one;
    // var videoPlayer = videoUI.GetComponent<VideoPlayer>();
    // videoPlayer.clip = Resources.Load<VideoClip>("Event_Resources/Videos/" + this.videoPath);
    // // awaitting video end
    // videoPlayer.Play();
    // // get video length
    // float videoLength = (float)videoPlayer.length;
    // // wait video length
    // await Task.Delay((int)(videoLength * 1000));
    // videoPlayer.Stop();
    // videoUI.transform.localScale = Vector3.zero;
  }
}