using UnityEngine;
using System.Collections;

public class ProjectileInfo : MonoBehaviour
{
    [SerializeField]
    int damage;

    [SerializeField]
    float speed;

    public GameObject owner { get; set; } //This should only be set at runtime by the actual owner.

    public int GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetDamage(int _dmg)
    {
        damage = _dmg;
    }

    public void SetSpeed(float _spd)
    {
        speed = _spd;
    }
}
