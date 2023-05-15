using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  public Rigidbody playerBody;
  public Vector3[] bounds = new Vector3[2];
  public bool playerCanMove = true;
  public float forwardForce = 10f;
  public float sideForce = 10f;
  public float jumpForce = 2f;
  public float rotationSpeed = 45f;
  public Vector3 minSpeed = new Vector3(-10, -10, -10);
  public Vector3 maxSpeed = new Vector3(10, 10, 10);
  public Vector3 startPos = new Vector3(0, 0, 0);
  public Vector3 stopVec = new Vector3(0, 0, 0);

  /** 
   * Move the player back to the start, reset its transform.
   */
  public void RestartGame()
  {
    this.playerBody.MovePosition(position: startPos);
    this.playerBody.transform.rotation = new Quaternion(0, 0, 0, 0);
    this.playerBody.velocity = stopVec;
    this.playerCanMove = true;
  }

  // FixedUpdate is called once per frame (for physics updates, for everything else use Update)
  void FixedUpdate()
  {
    if (Input.GetKey("escape"))
    {
      SceneManager.LoadScene(0);
    }

    if (!playerCanMove) return;

    if (Input.GetKey("w"))
    {
      playerBody.AddForce(Utils.Utils.multiplyVector3(transform.forward, forwardForce * Time.deltaTime), ForceMode.VelocityChange);
    }
    if (Input.GetKey("s"))
    {
      playerBody.AddForce(Utils.Utils.multiplyVector3(transform.forward, -forwardForce * Time.deltaTime), ForceMode.VelocityChange);
    }
    if (Input.GetKey("a"))
    {
      transform.Rotate(new Vector3(playerBody.rotation.x, playerBody.rotation.x - rotationSpeed, playerBody.rotation.z) * Time.deltaTime);
    }
    if (Input.GetKey("d"))
    {
      transform.Rotate(new Vector3(playerBody.rotation.x, playerBody.rotation.x + rotationSpeed, playerBody.rotation.z) * Time.deltaTime);
    }
    if (Input.GetKey("space"))
    {
      playerBody.AddForce(Utils.Utils.multiplyVector3(Vector3.up, jumpForce), ForceMode.VelocityChange);
    }

    this.playerBody.velocity = Utils.Utils.clampVector3(minSpeed, this.playerBody.velocity, maxSpeed);
    this.playerBody.position = Utils.Utils.clampVector3(bounds[0], this.playerBody.position, bounds[1]);
  }
}
