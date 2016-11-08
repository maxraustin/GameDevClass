using UnityEngine;
using System.Collections;

/// <summary>
/// Contains information about a unit's maxHealth, maxShields, maxSpeed, weapon cooldowns, explosion object, and if it is a player controlled ship.
/// </summary>
public class UnitInfo : MonoBehaviour
{
	[SerializeField]
	string shipTitle;

    [SerializeField]
    UnitType unitType;

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
    float maxPitchSpeed;

    [SerializeField]
    float maxRollSpeed;

    [SerializeField]
    float maxYawSpeed;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject notTargettable;

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

    public float MaxPitchSpeed { get { return maxPitchSpeed; } }

    public float MaxRollSpeed { get { return maxRollSpeed; } }

    public float MaxYawSpeed { get { return maxYawSpeed; } }

    public bool NotTargettable { get { return notTargettable; } }

    public float ShieldRegenRate
    {
        get { return shieldRegenRate; }
        set
        {
            if (value >= 0)
                shieldRegenRate = value;
            else
                Debug.LogError("Shield Regen Rate can not be negative.");
        }
    }

    public string ShipTitle {  get { return shipTitle; } }

    public UnitType UnitType { get { return unitType; } }

    public int TeamID
    {
        get { return teamID; }
        set { teamID = value; }
    }
}
