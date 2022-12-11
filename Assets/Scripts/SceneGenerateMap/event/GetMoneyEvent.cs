using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GetMoneyEvent : TileEvent
{
  public override string GetEventName()
  {
    return "GetMoney";
  }

  public override void OnEventChangeStetus(Dictionary<string, PlayerStetus> playerStetus, string triggerPlayerId)
  {
    playerStetus[triggerPlayerId].Money += 100;
  }

  public override async Task OnEventAnimation()
  {
    var videoUI = GameObject.Find("VideoPlayer");
    videoUI.transform.localScale = Vector3.one;
    var videoPlayer = videoUI.GetComponent<VideoPlayer>();
    videoPlayer.clip = Resources.Load<VideoClip>("Event_Resources/Videos/67_UFO");
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