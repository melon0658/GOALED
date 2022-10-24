using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float mainSPEED;
  [SerializeField] private float x_sensi;
  [SerializeField] private float y_sensi;
  [SerializeField] private new GameObject camera;
  [SerializeField] private GameObject bullet;
  [SerializeField] private GameObject manager;
  public int balletCount = 0;
  void Start()
  {
  }

  void Update()
  {
    movecon();
    cameracon();
    shot();
  }

  void shot()
  {
    if (Input.GetMouseButtonDown(0))
    {
      var bulletobj = Instantiate(bullet, transform.position, transform.rotation);
      bulletobj.GetComponent<Rigidbody>().AddForce(camera.transform.TransformDirection(Vector3.forward) * 1000f);
      balletCount++;
      var playerData = new GameService.PlayerData();
      playerData.Id = manager.GetComponent<Manager>().playerInfo.player.Id;
      playerData.Key.Add("balletCount");
      playerData.Value.Add(balletCount.ToString());
      // manager.GetComponent<Manager>().AddPlayerData(playerData);
    }
  }

  void movecon()
  {
    Transform trans = transform;
    transform.position = trans.position;
    trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * mainSPEED;
    trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * mainSPEED;
  }

  void cameracon()
  {
    float x_Rotation = Input.GetAxis("Mouse X");
    float y_Rotation = Input.GetAxis("Mouse Y");
    x_Rotation = x_Rotation * x_sensi;
    y_Rotation = y_Rotation * y_sensi;
    this.transform.Rotate(0, x_Rotation, 0);
    camera.transform.Rotate(-y_Rotation, 0, 0);
  }
}
