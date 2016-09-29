using UnityEngine;
using System.Collections;

/// <summary>
/// Provides methods that provide Quaternions for one location to look at another location.
/// </summary>
public class RotationCalculator : MonoBehaviour {
    /// <summary>
    /// Gets a Quaternion that's looking from the given origin toward the given location.
    /// </summary>
    /// <param name="origin">Location to look from.</param>
    /// <returns>Quaternion look from origin toward location.</returns>
    public static Quaternion RotationTowardLocation(Vector3 origin, Vector3 targetLocation)
    {
        Vector3 lookDirection = (targetLocation - origin).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        return lookRotation;
    }

    /// <summary>
    /// Gets a Quaternion that's looking from the given origin toward the player ship. If no player ship in scene, looks toward position (0,0,0).
    /// </summary>
    /// <param name="origin">Location to look from.</param>
    /// <returns>Quaternion look from origin toward player ship.</returns>
    public static Quaternion RotationTowardPlayerShip(Vector3 origin)
    {
        Quaternion lookRotation = Quaternion.identity;
        if (UnitTracker.PlayerShip != null) lookRotation = RotationTowardLocation(origin, UnitTracker.PlayerShip.transform.position);
        else lookRotation = RotationTowardZero(origin);

        return lookRotation;
    }

    /// <summary>
    /// Gets a Quaternion that's looking from the given origin toward position (0,0,0).
    /// </summary>
    /// <param name="origin">Location to look from.</param>
    /// <returns>Quaternion look from origin toward (0,0,0).</returns>
    public static Quaternion RotationTowardZero(Vector3 origin)
    {
        return RotationTowardLocation(origin, Vector3.zero);
    }
}
