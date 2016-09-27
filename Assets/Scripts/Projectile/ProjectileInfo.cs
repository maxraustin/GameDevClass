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

    //Owner and TeamID should only be set at runtime by the unit that created this projectile.
    public GameObject Owner { get; set; } 
    public int TeamID { get; set; }

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
