using UnityEngine;

public class DayCicleManager : MonoBehaviour
{
  public new Light light;
  public Color dayLightColor;
  public float dayLightIntensity;
  public float nightLightIntensity;
  public Color nightLightColor;
  public bool isDayTime = true;

  public void SetDay()
  {
    light.color = dayLightColor;
    light.intensity = dayLightIntensity;
    RenderSettings.fogColor = dayLightColor;
    RenderSettings.fogDensity = 0.00034f;
  }

  public void SetNight()
  {
    light.color = nightLightColor;
    light.intensity = nightLightIntensity;
    RenderSettings.fogColor = nightLightColor;
    RenderSettings.fogDensity = 0.0034f;
  }

  public void ManageTimeChange()
  {
    if (isDayTime) SetNight();
    else SetDay();

    isDayTime = !isDayTime;
  }

  void Update()
  {
    if (Input.GetKeyDown("e"))
    {
      this.ManageTimeChange();
    }
  }
}
