using UnityEngine;
using System.Collections;

public class UnitInfo : MonoBehaviour
{
    [SerializeField]
    int maxHealth;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float cooldownPrimary;

    [SerializeField]
    float cooldownSecondary;

    [SerializeField]
    float cooldownTertiary;

    [SerializeField]
    GameObject explosion;

    public float GetCooldownPrimary()
    {
        return cooldownPrimary;
    }

    public float GetCooldownSecondary()
    {
        return cooldownSecondary;
    }

    public float GetCooldownTertiary()
    {
        return cooldownTertiary;
    }

    public GameObject GetExplosion()
    {
        return explosion;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public void SetCooldownPrimary(float _cool)
    {
        cooldownPrimary = _cool;
    }

    public void SetCooldownSecondary(float _cool)
    {
        cooldownSecondary = _cool;
    }

    public void SetCooldownTertiary(float _cool)
    {
        cooldownTertiary = _cool;
    }

    public void SetMaxHealth(int _mh)
    {
        maxHealth = _mh;
    }

    public void SetMaxSpeed(int _spd)
    {
        maxSpeed = _spd;
    }
}
