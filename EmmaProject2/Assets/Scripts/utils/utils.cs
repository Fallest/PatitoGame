using UnityEngine;

namespace Utils
{

  public static class Utils
  {
    public static Vector3 clampVector3(Vector3 min, Vector3 val, Vector3 max)
    {
      if (val.x < min.x) val.x = min.x;
      else if (val.x > max.x) val.x = max.x;

      if (val.y < min.y) val.y = min.y;
      else if (val.y > max.y) val.y = max.y;

      if (val.z < min.z) val.z = min.z;
      else if (val.z > max.z) val.z = max.z;

      return val;
    }

    public static Vector3 multiplyVector3(Vector3 vec1, Vector3 vec2)
    {
      return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
    }

    public static Vector3 multiplyVector3(Vector3 vector, float value)
    {
      return new Vector3(vector.x * value, vector.y * value, vector.z * value);
    }
  }

}