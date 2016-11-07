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

    /// <summary>
    /// Gets a rotation to use for accurately aiming at a target.
    /// </summary>
    /// <param name="shooter">The GameObject that is firing the projectile.</param>
    /// <param name="projectile">The Projectile to be fired.</param>
    /// <param name="target">The GameObject to shoot at.</param>
    /// <returns>Quaternion for shooter GameObject to accurately shoot target GameObject.</returns>
    public static Quaternion RotationToHitTarget(GameObject shooter, GameObject projectile, GameObject target)
    {
        return RotationToHitTarget(shooter, projectile, target, RandomOffset.NONE, Vector3.zero, false);
    }

    /// <summary>
    /// Gets a rotation to use for accurately aiming at a target with a small random offset.
    /// </summary>
    /// <param name="shooter">The GameObject that is firing the projectile.</param>
    /// <param name="projectile">The Projectile to be fired.</param>
    /// <param name="target">The GameObject to shoot at.</param>
    /// <param name="offsetAmount">Amount of random offset to apply to the rotation.</param>
    /// <param name="aimOffset">Distance from target position to aim at.</param>
    /// <returns>Quaternion for shooter GameObject to accurately shoot target GameObject.</returns>
    public static Quaternion RotationToHitTarget(GameObject shooter, GameObject projectile, GameObject target, RandomOffset offsetAmount, Vector3 aimOffset, bool rotationInstant)
    {
        Vector3 p0 = target.transform.position + aimOffset;
        Vector3 v0 = target.GetComponent<Rigidbody>().velocity.normalized;
        float s0 = target.GetComponent<Rigidbody>().velocity.magnitude;
        Vector3 p1 = shooter.transform.position;
        float s1 = projectile.GetComponent<ProjectileInfo>().Speed;

        //float a = (v0.x * v0.x) + (v0.y * v0.y) - (s1 * s1);
        //float b = 2 * ((p0.x * v0.x) + (p0.y * v0.y) - (p1.x * v0.x) - (p1.y * v0.y));
        //float c = (p0.x * p0.x) + (p0.y * p0.y) + (p1.x * p1.x) + (p1.y * p1.y) - (2 * p1.x * p0.x) - (2 * p1.y * p0.y);
        float a = (v0.x * v0.x) + (v0.y * v0.y) + (v0.z * v0.z) - (s1 * s1);
        float b = 2 * ((p0.x * v0.x) + (p0.y * v0.y) + (p0.z * v0.z) - (p1.x * v0.x) - (p1.y * v0.y) - (p1.z * v0.z));
        float c = (p0.x * p0.x) + (p0.y * p0.y) + (p0.z * p0.z) + (p1.x * p1.x) + (p1.y * p1.y) + (p1.z * p1.z) - (2 * p1.x * p0.x) - (2 * p1.y * p0.y) - (2 * p1.z * p0.z);

        float t1 = (-b + Mathf.Sqrt((b * b) - (4 * a * c))) / (2 * a);
        float t2 = (-b - Mathf.Sqrt((b * b) - (4 * a * c))) / (2 * a);

        float t = Mathf.Min(t1, t2);
        if (t < 0) t = Mathf.Max(t1, t2);
        if (t < 0) Debug.Log("t < 0");

        float vx = (p0.x - p1.x + (t * s0 * v0.x)) / (t * s1);
        float vy = (p0.y - p1.y + (t * s0 * v0.y)) / (t * s1);
        float vz = (p0.z - p1.z + (t * s0 * v0.z)) / (t * s1);

        Vector3 targetVector = new Vector3(vx, vy, vz);

        float randomnessFactor = 0;
        if (offsetAmount == RandomOffset.TINY) randomnessFactor = 0.01f;
        else if(offsetAmount == RandomOffset.SMALL) randomnessFactor = 0.02f;
        else if (offsetAmount == RandomOffset.MEDIUM) randomnessFactor = 0.03f;
        else if (offsetAmount == RandomOffset.LARGE) randomnessFactor = 0.05f;
        else if (offsetAmount == RandomOffset.GIANT) randomnessFactor = 0.1f;

        if (!rotationInstant)
            randomnessFactor *= 7.5f;

        Vector3 lookVector = targetVector + new Vector3(Random.Range(-randomnessFactor, randomnessFactor), Random.Range(-randomnessFactor, randomnessFactor), Random.Range(-randomnessFactor, randomnessFactor));
        Quaternion lookRotation = Quaternion.LookRotation(lookVector);
        
        return lookRotation;
    }
}
