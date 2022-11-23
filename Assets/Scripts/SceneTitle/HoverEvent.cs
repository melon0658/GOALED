using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverEvent : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip SE1; //1つ目の効果音
    public AudioClip SE2;
    void Start()
    {
        gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnMouseHover(); });
        trigger.triggers.Add(entry);
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

    }

    public void OnMouseHover()
    {
        // Debug.Log("あるぱか　あるぱか");
        audioSource.PlayOneShot(SE1);
    }

    public void OnMouseDown() 
    {
        Debug.Log("b");
        audioSource.PlayOneShot(SE2);
    }
}