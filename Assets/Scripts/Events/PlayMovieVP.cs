using UnityEngine;
using UnityEngine.Video;

public class PlayMovieVP : MonoBehaviour
{
  //public UnityEngine.Video.VideoClip videoClip;
  //public RenderTexture targetTexture;
  //public UnityEngine.UI.RawImage eventVideo;

  //void Start()
  //{
  //  eventVideo.enabled = false;
  //}

  //public void showVideoPlayer(string videoName){
  //  eventVideo.enabled = true;
  //  var videoPlayer = gameObject.AddComponent<VideoPlayer>();
  //  videoPlayer.url = "Assets/Resources/Event_Resources/Videos/" + videoName;
  //  videoPlayer.loopPointReached += LoopPointReached;
  //  videoPlayer.playOnAwake = true;

  //  //string[] videoPath = videoName.Split('.');
  //  //videoPath[0] = "Event_Resources/Videos/" + videoPath[0];
  //  //Resources.Load<VideoClip>(videoPath[0]);
  //  //videoPlayer.clip = videoClip;

  //  videoPlayer.renderMode = VideoRenderMode.RenderTexture;
  //  videoPlayer.targetTexture = targetTexture;
  //  videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
  //}

  //public void LoopPointReached(VideoPlayer vp)
  //{
  //    eventVideo.enabled = false;
  //}
  public RenderTexture targetTexture;
  public UnityEngine.UI.RawImage eventVideo;

  private VideoPlayer videoPlayer;
  private AudioSource audioSource;

  private void Start()
  {
    eventVideo.enabled = false;

    audioSource = gameObject.AddComponent<AudioSource>();
  }

  public void showVideoPlayer(string videoName)
  {
    eventVideo.enabled = true;

    if (videoPlayer == null)
    {
      videoPlayer = gameObject.AddComponent<VideoPlayer>();

      videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
      videoPlayer.EnableAudioTrack(0, true);
      videoPlayer.SetTargetAudioSource(0, audioSource);
    }
    else
    {
      videoPlayer.Stop();
      videoPlayer.clip = null;
    }

    string[] splitStr = videoName.Split('.');
    string videoPath = "Event_Resources/Videos/" + splitStr[0];
    var videoClip = Resources.Load<VideoClip>(videoPath);

    if (videoClip != null)
    {
      videoPlayer.clip = videoClip;
      videoPlayer.loopPointReached += LoopPointReached;
      //videoPlayer.playOnAwake = false;

      videoPlayer.renderMode = VideoRenderMode.RenderTexture;
      videoPlayer.targetTexture = targetTexture;
      //videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

      videoPlayer.Play();
    }
    else
    {
      Debug.LogError("Failed to load video: " + videoPath);
      eventVideo.enabled = false;
    }
  }

  private void LoopPointReached(VideoPlayer vp)
  {
    eventVideo.enabled = false;
  }
}