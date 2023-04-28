using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  public Transform player;
  public Vector3 offset;
  // Update is called once per frame
  void Update()
  {
    transform.position = player.position + offset;
    transform.LookAt(player);
    transform.RotateAround(player.position, Vector3.up, player.rotation.eulerAngles.y);
  }
}
