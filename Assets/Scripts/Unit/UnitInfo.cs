using UnityEngine;
using System.Collections;

/// <summary>
/// Contains information about a unit's maxHealth, maxShields, maxSpeed, weapon cooldowns, explosion object, and if it is a player controlled ship.
/// </summary>
public class UnitInfo : MonoBehaviour
{
    [SerializeField]
    int teamID;

    [SerializeField]
    int maxHealth;

    [SerializeField]
    int maxShields;

    [SerializeField]
    float shieldRegenRate; //Rate in shields regenerated per second.

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    bool isPlayerShip;

    public GameObject Explosion
    {
        get { return explosion; }
    }

    public bool IsPlayerShip
    {
        get { return isPlayerShip; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int MaxShields
    {
        get { return maxShields; }
        set { maxShields = value; }
    }

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    public float ShieldRegenRate
    {
        get { return shieldRegenRate; }
        set { shieldRegenRate = value; }
    }

    public int TeamID
    {
        get { return teamID; }
        set { teamID = value; }
    }
}
