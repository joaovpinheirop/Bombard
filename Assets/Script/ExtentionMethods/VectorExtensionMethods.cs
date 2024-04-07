using UnityEngine;
public static class VectorExtensionMethods
{
  public static bool IsZero(this Vector2 v, float sqrEpslon = Vector2.kEpsilon)
  {
    return v.sqrMagnitude <= sqrEpslon;
  }
  public static bool IsZero(this Vector3 v, float sqrEpslon = Vector2.kEpsilon)
  {
    return v.sqrMagnitude <= sqrEpslon;
  }
}