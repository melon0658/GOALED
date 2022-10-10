using UnityEngine;
using System.Collections.Generic;
public class Fps
{
  //測定用
  private float m_fps;
  private float m_updateInterval = 1.0f;
  private Queue<float> m_deltaTimeQueue = new Queue<float>();
  private float secondFlames = 0;
  private float m_beforeTime;
  public float GetFlames()
  {
    return secondFlames;
  }

  public float GetFPS()
  {
    return m_fps;
  }
  public void Update()
  {
    float timeNow = Time.realtimeSinceStartup;
    float deltaTime = timeNow - m_beforeTime;
    m_beforeTime = timeNow;
    m_deltaTimeQueue.Enqueue(timeNow);
    while (m_deltaTimeQueue.Count > 0 && m_deltaTimeQueue.Peek() < timeNow - m_updateInterval)
    {
      m_deltaTimeQueue.Dequeue();
    }
    secondFlames = m_deltaTimeQueue.Count;
    m_fps = 1.0f / deltaTime;
  }
}