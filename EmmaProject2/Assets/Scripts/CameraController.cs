using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject subject;
  public Vector3 maxDistance = new Vector3(0, 2, -5);
  // Las etiquetas que el control de colisiones usará para saber si debe o no colisionar
  public string[] colliderTags = new string[1] { "CameraObstacle" };
  public float minTransparency = 0.3f;
  public float minDistance = 1;
  private Vector3 currentOffset;
  private Renderer[] subjectRenderers;


  // Siempre cargar el material del sujeto al que se está siguiendo con la cámara
  void Start()
  {
    this.subjectRenderers = subject.gameObject.GetComponentsInChildren<Renderer>();
  }

  // Actualizar la posición y rotación de la cámara, y además ajustar la transparencia del material del sujeto
  void LateUpdate()
  {
    UpdatePosition();
    UpdateRotation();
    // UpdateAlpha();
  }

  // Control de colisión
  // Al entrar en colisión, la cámara no debe alejarse más del sujeto de lo que permite el objeto con el que se ha colisionado.
  // Solo se reajustará la distancia si el objecto con el que está colisionando tiene la etiqueta "Obstacle".
  void OnCollisionEnter(Collision collision)
  {
    if (colliderTags.Contains(collision.collider.tag))
    {
      Vector3 headingVector = this.subject.transform.position - this.transform.position;
      this.currentOffset = -headingVector;
    }
  }

  // Al salir de la colisión, reajustar la posición a lo máximo permitido
  void OnCollisionExit(Collision collision)
  {
    Vector3 headingVector = this.subject.transform.position - this.transform.position;
    this.currentOffset = headingVector;
  }

  // Control de la posición de la cámara
  private void UpdatePosition()
  {
    Vector3 cameraPos = subject.transform.position + maxDistance;
    this.transform.position = cameraPos - currentOffset;
    Debug.Log(currentOffset);
  }

  // Control de la rotación de la cámara
  private void UpdateRotation()
  {
    transform.LookAt(subject.transform);
    transform.RotateAround(subject.transform.position, Vector3.up, subject.transform.rotation.eulerAngles.y);
  }

  // Control de transparencia del sujeto
  private void ChangeAlpha(Renderer[] renderers, float alphaVal)
  {
    foreach (Renderer renderer in renderers)
    {
      Color oldColor = renderer.material.color;
      Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
      renderer.material.SetColor("_Color", newColor);
    }
  }

  private void UpdateAlpha()
  {
    if (this.transform.position.z <= minDistance)
    {
      ChangeAlpha(subjectRenderers, minTransparency);
    }
    else if (this.transform.position.z > minDistance)
    {
      ChangeAlpha(subjectRenderers, 1);
    }
  }
}
