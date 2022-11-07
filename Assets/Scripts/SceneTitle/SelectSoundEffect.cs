using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSoundEffect : MonoBehaviour
{
    public GameObject button;
    AudioSource audioSource;
    public AudioClip SE1; //1つ目の効果音
    public AudioClip SE2; //2つ目の効果音
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.AddComponent<EventTrigger>();
        // EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerEnter;
        // entry.callback.AddListener((data) => { OnMouseHover(); });
        // trigger.triggers.Add(entry);
        // audioSource = GetComponent<AudioSource>();
        // button = GameObject.Find("Start_button");
        Debug.Log("c");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter() {
        Debug.Log("a");
        audioSource.PlayOneShot(SE1);
    }

    public void OnMouseDown() {
        Debug.Log("b");
        audioSource.PlayOneShot(SE2);
    }
}
