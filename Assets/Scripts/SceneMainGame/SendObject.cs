using UnityEngine;

public class SendObject : MonoBehaviour
{
  // private string ObjectId = System.Guid.NewGuid().ToString();
  private string ObjectId;
  [SerializeField] private string Prefub;
  private Vector3 positionBefore = Vector3.zero;
  private Vector3 rotationBefore = Vector3.zero;

  void Start()
  {
    ObjectId = System.Guid.NewGuid().ToString();
    var mainGame = GameObject.Find("MainGameManager").GetComponent<MainGame>();
    mainGame.AddSendObjects(gameObject);
  }

  public string getID()
  {
    return ObjectId;
  }

  public string getPrefub()
  {
    return Prefub;
  }

  public void setTransform()
  {
    positionBefore = transform.position;
    rotationBefore = transform.rotation.eulerAngles;
  }

  public bool isTransformChanged()
  {
    return positionBefore != transform.position || rotationBefore != transform.rotation.eulerAngles;
  }
}
