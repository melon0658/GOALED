using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GetMoneyEvent : TileEvent
{
  private string eventName;
  private string eventDescription;
  private int money;
  private string videoPath;

  public GetMoneyEvent(string eventName, string eventDescription, int money, string videoPath)
  {
    this.eventName = eventName;
    this.eventDescription = eventDescription;
    this.money = money;
    this.videoPath = videoPath;
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
    playerStetus[triggerPlayerId].Money += money;
  }

  public override async Task OnEventAnimation()
  {
    var videoUI = GameObject.Find("VideoPlayer");
    videoUI.transform.localScale = Vector3.one;
    var videoPlayer = videoUI.GetComponent<VideoPlayer>();
    videoPlayer.clip = Resources.Load<VideoClip>("Event_Resources/Videos/" + this.videoPath);
    // awaitting video end
    videoPlayer.Play();
    // get video length
    float videoLength = (float)videoPlayer.length;
    // wait video length
    await Task.Delay((int)(videoLength * 1000));
    videoPlayer.Stop();
    videoUI.transform.localScale = Vector3.zero;
  }
}