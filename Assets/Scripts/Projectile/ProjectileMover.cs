using UnityEngine;
using System.Collections;

/// <summary>
/// Handles a projectile's movement.
/// </summary>
public class ProjectileMover : MonoBehaviour
{
    Vector3 adjustedVelocity;

    void Start()
    {
        SetVelocity();
    }

    /// <summary>
    /// Adds a base velocity vector to the projectile.
    /// </summary>
    /// <param name="velVector"></param>
    public void AddVelocity(Vector3 velVector)
    {
        adjustedVelocity = velVector;
        SetVelocity();
    }

    /// <summary>
    /// Change's projectile's rigidbody's velocity based on it's adjusted velocity vector and base speed.
    /// </summary>
    void SetVelocity()
    {
        ProjectileInfo pi = GetComponent<ProjectileInfo>();

        if (pi != null)
            GetComponent<Rigidbody>().velocity = (transform.forward * pi.Speed) + adjustedVelocity;
    }
}
