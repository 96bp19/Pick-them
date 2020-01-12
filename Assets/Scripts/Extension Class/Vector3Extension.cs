
using UnityEngine;

public static class Vector3Extension 
{
   public static   Vector3  with(this Vector3 original,float? x = null, float? y = null , float? z = null)
    {
        // if the given x y z value is null returns original val , else returns parameter value
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
    {
        return Vector3.Normalize(destination - source);
    }
}
