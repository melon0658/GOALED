using UnityEngine;

public class SendObject : MonoBehaviour
{
  private string ObjectId;
  [SerializeField] private string Prefub;
  [SerializeField] private bool isSyncPosition = true;
  [SerializeField] private bool isSyncRotation = true;
  [SerializeField] private bool isSyncScale = true;
  private Vector3 positionBefore = Vector3.zero;
  private Vector3 rotationBefore = Vector3.zero;
  private Vector3 scaleBefore = Vector3.zero;
  private GameService.Object go = new GameService.Object();

  void Start()
  {
    ObjectId = System.Guid.NewGuid().ToString();
    var mainGame = GameObject.Find("MainGameManager").GetComponent<MainGame>();
    go.ObjectId = ObjectId;
    go.Prefub = Prefub;
    go.Owner = mainGame.playerInfo.player.Id;
    mainGame.AddSendObjects(gameObject);
  }

  public void setTransform()
  {
    positionBefore = transform.position;
    rotationBefore = transform.rotation.eulerAngles;
    scaleBefore = transform.localScale;
  }

  public bool isTransformChanged()
  {
    return positionBefore != transform.position || rotationBefore != transform.rotation.eulerAngles || scaleBefore != transform.localScale;
  }

  public GameService.Object toObject(){
    if (isSyncPosition)
    {
      go.Position = new GameService.Vec3 { X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
    }
    if (isSyncRotation)
    {
      go.Rotation = new GameService.Vec3 { X = transform.rotation.x, Y = transform.rotation.y, Z = transform.rotation.z };
    }
    if (isSyncScale)
    {
      go.Scale = new GameService.Vec3 { X = transform.localScale.x, Y = transform.localScale.y, Z = transform.localScale.z };
    }
    return go;
  }
}
