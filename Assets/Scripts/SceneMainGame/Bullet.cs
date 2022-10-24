using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
  private float time = 0f;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    time += Time.deltaTime;
    if (time > 5f)
    {
      this.GetComponent<SendObject>().setRPC("Delete", new Dictionary<string, string>());
      Destroy(gameObject);
    }
  }
}
