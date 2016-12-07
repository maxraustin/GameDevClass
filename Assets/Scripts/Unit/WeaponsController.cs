using UnityEngine;
using System.Collections;

/// <summary>
/// Controls firing a units' weapons.
/// </summary>
public class WeaponsController: MonoBehaviour
{
    [SerializeField]
    GameObject primaryWeapon;

    [SerializeField]
    float primaryCooldown;

    [SerializeField]
    bool primaryAmmoLimited;

    [SerializeField]
    int primaryAmmoMax;

    [SerializeField]
    bool primaryAmmoRegenerates;

    [SerializeField]
    float primaryAmmoRegenRate; //Ammo per second.

    [SerializeField]
    GameObject secondaryWeapon;

    [SerializeField]
    float secondaryCooldown;

    [SerializeField]
    bool secondaryAmmoLimited;

    [SerializeField]
    int secondaryAmmoMax;

    [SerializeField]
    bool secondaryAmmoRegenerates;

    [SerializeField]
    float secondaryAmmoRegenRate; //Ammo per second.

    [SerializeField]
    GameObject tertiaryWeapon;

    [SerializeField]
    float tertiaryCooldown;

    [SerializeField]
    bool tertiaryAmmoLimited;

    [SerializeField]
    int tertiaryAmmoMax;

    [SerializeField]
    bool tertiaryAmmoRegenerates;

    [SerializeField]
    float tertiaryAmmoRegenRate; //Ammo per second.

    [SerializeField]
    Transform[] primaryWeaponShotSpawns;

    [SerializeField]
    Transform[] secondaryWeaponShotSpawns;

    [SerializeField]
    Transform[] tertiaryWeaponShotSpawns;

	//SFX FOR WEAPONS
	[SerializeField]
	AudioSource primarySource;
	[SerializeField]
	AudioSource secondarySource;

    UnitInfo myInfo;

    float nextFirePrimary = 0, nextFireSecondary = 0, nextFireTertiary = 0;
    int primaryAmmoCurrent, secondaryAmmoCurrent, tertiaryAmmoCurrent;
    float nextRegenPrimary, nextRegenSecondary, nextRegenTertiary;

    public GameObject PrimaryWeapon { get { return primaryWeapon; } }
    public GameObject SecondaryWeapon { get { return secondaryWeapon; } }
    public GameObject TertiaryWeapon { get { return tertiaryWeapon; } }

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();

        if (primaryAmmoLimited)
            primaryAmmoCurrent = primaryAmmoMax;
        if (secondaryAmmoLimited)
            secondaryAmmoCurrent = secondaryAmmoMax;
        if (tertiaryAmmoLimited)
            tertiaryAmmoCurrent = tertiaryAmmoMax;

        if (primaryAmmoRegenRate >= 0)
            nextRegenPrimary = Time.time + (1 / primaryAmmoRegenRate);
        else
            nextRegenPrimary = float.MaxValue;

        if (secondaryAmmoRegenRate >= 0)
            nextRegenSecondary= Time.time + (1 / secondaryAmmoRegenRate);
        else
            nextRegenSecondary = float.MaxValue;

        if (tertiaryAmmoRegenRate >= 0)
            nextRegenTertiary = Time.time + (1 / tertiaryAmmoRegenRate);
        else
            nextRegenTertiary = float.MaxValue;

        AdjustWeaponsDisplay();
    }

    void Update()
    {
        CheckAmmoRegen();
    }

    void AdjustWeaponsDisplay()
    {
        if (myInfo.IsPlayerShip && HUDController.Instance != null)
        {
            if (primaryWeapon == null)
                HUDController.Instance.SetWeaponsEnabled(1, false);
            else if (primaryAmmoLimited)
                HUDController.Instance.SetWeaponsCount(1, primaryAmmoCurrent);
            else
                HUDController.Instance.SetWeaponsCount(1, true);

            if (secondaryWeapon == null)
                HUDController.Instance.SetWeaponsEnabled(2, false);
            else if (secondaryAmmoLimited)
                HUDController.Instance.SetWeaponsCount(2, secondaryAmmoCurrent);
            else
                HUDController.Instance.SetWeaponsCount(2, true);

            if (tertiaryWeapon == null)
                HUDController.Instance.SetWeaponsEnabled(3, false);
            else if (tertiaryAmmoLimited)
                HUDController.Instance.SetWeaponsCount(3, tertiaryAmmoCurrent);
            else
                HUDController.Instance.SetWeaponsCount(3, true);
        }
    }

    public void CheckAmmoRegen()
    {
        if (primaryAmmoRegenerates && Time.time > nextRegenPrimary)
        {
            if (primaryAmmoCurrent < primaryAmmoMax)
                primaryAmmoCurrent++;

            if (primaryAmmoRegenRate >= 0)
                nextRegenPrimary = Time.time + (1 / primaryAmmoRegenRate);
            else
                nextRegenPrimary = float.MaxValue;

            AdjustWeaponsDisplay();
        }
        if (secondaryAmmoRegenerates && Time.time > nextRegenSecondary)
        {
            if (secondaryAmmoCurrent < secondaryAmmoMax)
                secondaryAmmoCurrent++;

            if (secondaryAmmoRegenRate >= 0)
                nextRegenSecondary = Time.time + (1 / secondaryAmmoRegenRate);
            else
                nextRegenSecondary = float.MaxValue;

            AdjustWeaponsDisplay();
        }
        if (tertiaryAmmoRegenerates && Time.time > nextRegenTertiary)
        {
            if (tertiaryAmmoCurrent < tertiaryAmmoMax)
                tertiaryAmmoCurrent++;

            if (tertiaryAmmoRegenRate >= 0)
                nextRegenTertiary = Time.time + (1 / tertiaryAmmoRegenRate);
            else
                nextRegenTertiary = float.MaxValue;

            AdjustWeaponsDisplay();
        }
    }

    public void FirePrimaryWeapon()
    {
        if (Time.time < nextFirePrimary || (primaryAmmoLimited && primaryAmmoCurrent < primaryWeaponShotSpawns.Length))
            return;

        nextFirePrimary = Time.time + primaryCooldown;

        if (primaryWeapon == null || primaryWeaponShotSpawns.Length == 0)
        {
            Debug.LogError("No primary weapon or transform set, can't fire.");
            return;
        }

        foreach(Transform tf in primaryWeaponShotSpawns)
        {
            //Create projectile.
            GameObject projectile = PoolController.Instance.GetObject(primaryWeapon, tf.position, tf.rotation);
			primarySource.Play ();
            projectile.layer = 8;
            primaryAmmoCurrent--;

            //Set projectile's owner and teamID.
            if (projectile.GetComponent<ProjectileInfo>() != null)
            {
                projectile.GetComponent<ProjectileInfo>().Owner = gameObject;
                projectile.GetComponent<ProjectileInfo>().TeamID = myInfo.TeamID;
            }
            //Adjust projectile's velocity based on our velocity.
            if (projectile.GetComponent<ProjectileMover>() != null && GetComponent<Rigidbody>() != null)
                projectile.GetComponent<ProjectileMover>().AddVelocity(GetComponent<Rigidbody>().velocity);
        }

        AdjustWeaponsDisplay();
    }

    public void FireSecondaryWeapon()
    {
        if (Time.time < nextFireSecondary || (secondaryAmmoLimited && secondaryAmmoCurrent < secondaryWeaponShotSpawns.Length))
            return;

        nextFireSecondary = Time.time + secondaryCooldown;

        if (secondaryWeapon == null || secondaryWeaponShotSpawns.Length == 0)
        {
            Debug.LogError("No secondary weapon or transform set, can't fire.");
            return;
        }

        foreach (Transform tf in secondaryWeaponShotSpawns)
        {
            //Create projectile.
            GameObject projectile = PoolController.Instance.GetObject(secondaryWeapon, tf.position, tf.rotation);//Instantiate(secondaryWeapon, tf.position, tf.rotation) as GameObject;
            secondarySource.Play ();
            projectile.layer = 8;
            secondaryAmmoCurrent--;

            //Set projectile's owner and teamID.
            if (projectile.GetComponent<ProjectileInfo>() != null)
            {
                projectile.GetComponent<ProjectileInfo>().Owner = gameObject;
                projectile.GetComponent<ProjectileInfo>().TeamID = myInfo.TeamID;
            }
            //Adjust projectile's velocity based on our velocity.
            if (projectile.GetComponent<ProjectileMover>() != null && GetComponent<Rigidbody>() != null)
                projectile.GetComponent<ProjectileMover>().AddVelocity(GetComponent<Rigidbody>().velocity);
        }

        AdjustWeaponsDisplay();
    }

    public void FireTertiaryWeapon()
    {
        if (Time.time < nextFireTertiary || (tertiaryAmmoLimited && tertiaryAmmoCurrent < tertiaryWeaponShotSpawns.Length))
            return;

        nextFireTertiary = Time.time + tertiaryCooldown;

        if (tertiaryWeapon == null || tertiaryWeaponShotSpawns.Length == 0)
        {
            Debug.LogError("No tertiary weapon or transform set, can't fire.");
            return;
        }

        foreach (Transform tf in tertiaryWeaponShotSpawns)
        {
            //Create projectile.
            GameObject projectile = PoolController.Instance.GetObject(tertiaryWeapon, tf.position, tf.rotation);
            projectile.layer = 8;
            tertiaryAmmoCurrent--;

            //Set projectile's owner and teamID.
            if (projectile.GetComponent<ProjectileInfo>() != null)
            {
                projectile.GetComponent<ProjectileInfo>().Owner = gameObject;
                projectile.GetComponent<ProjectileInfo>().TeamID = myInfo.TeamID;
            }
            //Adjust projectile's velocity based on our velocity.
            if (projectile.GetComponent<ProjectileMover>() != null && GetComponent<Rigidbody>() != null)
                projectile.GetComponent<ProjectileMover>().AddVelocity(GetComponent<Rigidbody>().velocity);
        }

        AdjustWeaponsDisplay();
    }
}
