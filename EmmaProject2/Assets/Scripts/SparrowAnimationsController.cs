using UnityEngine;

public class SparrowAnimationsController : MonoBehaviour
{
  public Rigidbody playerBody;
  public Animator playerAnimator;

  void Update()
  {
    if ((int)Mathf.Abs(playerBody.velocity.y) > 0)
    {
      playerAnimator.Play("Fly");
    }
    else if ((int)Mathf.Abs(playerBody.velocity.magnitude) == 0)
    {
      playerAnimator.Play("Idle_A");
    }
    else if ((int)Mathf.Abs(playerBody.velocity.magnitude) > 5)
    {
      playerAnimator.Play("Run");
    }
    else
    {
      playerAnimator.Play("Walk");
    }
  }
}
