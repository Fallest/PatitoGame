using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  // Main
  public Transform cameraTarget;
  public bool cameraEnabled = false;
  public float cameraTargetHeight = 1.5f;
  public float mouseSensitivity = 3f;
  public float lerpSpeed = 15f;
  public float rotateTargetOffsetY = 0f;
  // Zoom
  public float zoomSensitivity = 5;
  public float zoomStartDistance = 2;
  public float zoomMin = 1.5f;
  public float zoomMax = 20;
  public float zoomSmooth = 1;
  // Ground Collision
  public float maxCollisionDetectionDistance = 0.5f;
  public float collisionCorrectionIncrement = 0.2f;

  private float x = 0.0f;
  private float y = 0.0f;
  private float distance = 3.0f;
  private float currentDistance = 3.0f;
  void Start()
  {
    distance = zoomStartDistance;
    currentDistance = zoomStartDistance;
  }
  void LateUpdate()
  {
    if (!cameraEnabled)
      return;

    if (Input.GetMouseButton(1))
    {
      x += Input.GetAxis("Mouse X") * mouseSensitivity;
      y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
    }


    float targetRotationAngle = cameraTarget.eulerAngles.y + rotateTargetOffsetY;
    float cameraRotationAngle = transform.eulerAngles.y;

    x = Mathf.LerpAngle(cameraRotationAngle, targetRotationAngle, lerpSpeed * Time.deltaTime);

    y = ClampAngle(y, -50, 50);

    Quaternion rotation = Quaternion.Euler(y, x, 0);

    // Extremely basic collision where the current distance is reduced until the camera is above the ground
    RaycastHit hit;
    if (Physics.Raycast(transform.position, -transform.up, out hit, maxCollisionDetectionDistance))
    {
      currentDistance -= collisionCorrectionIncrement;
    }

    // Smooth zoom
    currentDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
    currentDistance = Mathf.Clamp(currentDistance, zoomMin, zoomMax);
    distance = Mathf.Lerp(distance, currentDistance, Time.deltaTime * zoomSmooth);

    Vector3 position = cameraTarget.position - (rotation * Vector3.forward * distance + new Vector3(0, -cameraTargetHeight, 0));

    transform.position = position;
    transform.rotation = rotation;
  }

  private float ClampAngle(float angle, float min, float max)
  {
    if (angle < -360)
    {
      angle += 360;
    }
    if (angle > 360)
    {
      angle -= 360;
    }
    return Mathf.Clamp(angle, min, max);
  }
}
