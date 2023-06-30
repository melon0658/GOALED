using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winneranimation : MonoBehaviour
{
  private GameObject player;
  private Animator anim;

  private Vector3 cylinderPos;
  private Vector3 velocity = Vector3.zero;
  private float time = 0.8F;
  private bool isAnimationStarted = false;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.Find("ch0");
    anim = player.GetComponent<Animator>();

    cylinderPos = GameObject.Find("Cylinder").GetComponent<Transform>().position;
    cylinderPos.y += 1.6f;

    StartCoroutine("goal");
  }

  // Update is called once per frame
  void Update()
  {
    if (isAnimationStarted)
    {
      player.GetComponent<Transform>().position = Vector3.SmoothDamp(player.GetComponent<Transform>().position, cylinderPos, ref velocity, time);
      if (player.GetComponent<Transform>().position.y - 9.8f < 0.01f)
      {
        isAnimationStarted = false;
      }
      Debug.Log("aa");
    }
    
  }

  private IEnumerator goal()
  {
    yield return new WaitForSeconds(3f);
    //player.GetComponent<Rigidbody>().isKinematic = false;
    anim.SetBool("onWinAnimation", true);
    yield return new WaitForSeconds(1f);
    isAnimationStarted = true;

    //エモート&落下
    //rb = player.GetComponent<Rigidbody>();
    //cylinderPos = GameObject.Find("Cylinder").GetComponent<Transform>().position;
    //cylinderPos.y += 2.0f; 

    //rb.isKinematic = false;
    //anim.SetBool("onWinAnimation", true);
    //isAnimationStarted = true;
  }
}
