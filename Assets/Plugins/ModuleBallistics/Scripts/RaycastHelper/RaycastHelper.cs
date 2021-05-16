using UnityEngine;

/// <summary>
/// Wrapper class
/// </summary>
public class RaycastHelper
{
    /// <summary>
    /// Check hit between points
    /// </summary>
    /// <param name="pointA">Point A</param>
    /// <param name="pointB">Point B</param>
    /// <param name="hit">hit != null if its happened</param>
    /// <returns>true if hit happened</returns>
    public static bool CheckHitBetweenPoints(Vector3 pointA, Vector3 pointB, out RaycastHit hit)
    {
        Vector3 direction = pointB - pointA;
        float distance = direction.magnitude;

        Ray ray = new Ray(pointA, direction);

        if (Physics.Raycast(ray, out hit, distance))
        {
            //Did hit
            return true;
        }

        //Did not hit
        return false;
    }
}
