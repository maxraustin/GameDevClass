using UnityEngine;
using System.Collections;

/// <summary>
/// Contains information about a projectile's damage, speed, and owner.
/// </summary>
public class ProjectileInfo : MonoBehaviour
{
    [SerializeField]
    int damage;

    [SerializeField]
    float speed;

    public GameObject owner { get; set; } //This should only be set at runtime by the projectile's actual owner.

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
