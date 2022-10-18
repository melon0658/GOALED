using UnityEngine;

public class SendObject : MonoBehaviour
{
  private string ObjectId;
  [SerializeField] private string Prefub;
  [SerializeField] private bool isSyncPosition = true;
  [SerializeField] private float syncPositionPeriod = 0.001f;
  [SerializeField] private bool isSyncRotation = true;
  [SerializeField] private float syncRotationPeriod = 0.001f;
  [SerializeField] private bool isSyncScale = true;
  [SerializeField] private float syncScalePeriod = 0.001f;
  private Vector3 positionBefore = Vector3.zero;
  private Vector3 rotationBefore = Vector3.zero;
  private Vector3 scaleBefore = Vector3.zero;
  private GameService.Object go = new GameService.Object();

  void Start()
  {
    ObjectId = System.Guid.NewGuid().ToString();
    var mainGame = GameObject.Find("MainGameManager").GetComponent<MainGame>();
    go.Id = ObjectId;
    go.Prefub = Prefub;
    go.Owner = mainGame.playerInfo.player.Id;
    mainGame.AddSendObjects(gameObject);
  }

  public string getID()
  {
    return ObjectId;
  }

  public void setTransform()
  {
    positionBefore = transform.position;
    rotationBefore = transform.rotation.eulerAngles;
    scaleBefore = transform.localScale;
  }

  public bool isTransformChanged()
  {
    return (positionBefore - transform.position).magnitude > syncPositionPeriod || (rotationBefore - transform.rotation.eulerAngles).magnitude > syncRotationPeriod || (scaleBefore - transform.localScale).magnitude > syncScalePeriod;
  }

  public GameService.Object toObject()
  {
    if (isSyncPosition)
    {
      go.Position = new GameService.Vec3 { X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
    }
    if (isSyncRotation)
    {
      go.Rotation = new GameService.Vec3 { X = transform.rotation.eulerAngles.x, Y = transform.rotation.eulerAngles.y, Z = transform.rotation.eulerAngles.z };
    }
    if (isSyncScale)
    {
      go.Scale = new GameService.Vec3 { X = transform.localScale.x, Y = transform.localScale.y, Z = transform.localScale.z };
    }
    return go;
  }
}
