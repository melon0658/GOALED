using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEvent : MonoBehaviour
{

  [SerializeField] private AudioClip sound1;
  private AudioSource audioSource;
  void Start()
  {
    gameObject.AddComponent<EventTrigger>();
    EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
    EventTrigger.Entry entry = new EventTrigger.Entry();
    entry.eventID = EventTriggerType.PointerEnter;
    entry.callback.AddListener((data) => { OnMouseHover(); });
    trigger.triggers.Add(entry);
  }

  void Update()
  {

  }

  private void OnMouseHover()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.PlayOneShot(sound1);
  }
}
