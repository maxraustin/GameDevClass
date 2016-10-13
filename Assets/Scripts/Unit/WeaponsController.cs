using UnityEngine;
using System.Collections;

/// <summary>
/// Controls firing of a units' weapons.
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
    GameObject secondaryWeapon;

    [SerializeField]
    float secondaryCooldown;

    [SerializeField]
    bool secondaryAmmoLimited;

    [SerializeField]
    int secondaryAmmoMax;

    [SerializeField]
    GameObject tertiaryWeapon;

    [SerializeField]
    float tertiaryCooldown;

    [SerializeField]
    bool tertiaryAmmoLimited;

    [SerializeField]
    int tertiaryAmmoMax;

    [SerializeField]
    Transform[] primaryWeaponShotSpawns;

    [SerializeField]
    Transform[] secondaryWeaponShotSpawns;

    [SerializeField]
    Transform[] tertiaryWeaponShotSpawns;

    UnitInfo myInfo;

    float nextFirePrimary = 0, nextFireSecondary = 0, nextFireTertiary = 0;
    int primaryAmmoCurrent, secondaryAmmoCurrent, tertiaryAmmoCurrent;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();

        if (primaryAmmoLimited)
            primaryAmmoCurrent = primaryAmmoMax;
        if (secondaryAmmoLimited)
            secondaryAmmoCurrent = secondaryAmmoMax;
        if (tertiaryAmmoLimited)
            tertiaryAmmoCurrent = tertiaryAmmoMax;

        AdjustWeaponsDisplay();
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
            GameObject projectile = Instantiate(primaryWeapon, tf.position, tf.rotation) as GameObject;
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
            GameObject projectile = Instantiate(secondaryWeapon, tf.position, tf.rotation) as GameObject;
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
}
