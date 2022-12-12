using UnityEngine;

public class PlayMovieVP : MonoBehaviour
{
    public UnityEngine.Video.VideoClip videoClip;
    public RenderTexture targetTexture;
    public UnityEngine.UI.RawImage eventVideo;
    
    void Start()
    {
        eventVideo.enabled = false;
    }

    public void showVideoPlayer(string videoName){
        eventVideo.enabled = true;
        var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = "Assets/Resources/Event_Resources/Videos/" + videoName;
        videoPlayer.loopPointReached += LoopPointReached;
        videoPlayer.playOnAwake = true;
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = targetTexture;
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
    }

    public void LoopPointReached(UnityEngine.Video.VideoPlayer vp)
    {
        eventVideo.enabled = false;
    }

    
}