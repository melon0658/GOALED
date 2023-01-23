using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MoveEvent : TileEvent
{
  private string eventName;
  private string eventDescription;
  private int step;

  public MoveEvent(string eventName, string eventDescription, int step)
  {
    this.eventName = eventName;
    this.eventDescription = eventDescription;
    this.step = step;
  }

  public override EventType GetEventType()
  {
    return EventType.EVENT_STOP;
  }

  public override EventEffectType GetEventEffectType()
  {
    return EventEffectType.EventEffectType_Move;
  }

  public override string GetEventName()
  {
    return eventName;
  }

  public override string GetEventDescription()
  {
    return eventDescription;
  }

  public int getStep()
  {
    return step;
  }

  public override void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId)
  {
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