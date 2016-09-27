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
    GameObject secondaryWeapon;

    [SerializeField]
    GameObject tertiaryWeapon;

    [SerializeField]
    Transform[] primaryWeaponShotSpawns;

    [SerializeField]
    Transform[] secondaryWeaponShotSpawns;

    [SerializeField]
    Transform[] tertiaryWeaponShotSpawns;

    UnitInfo myInfo;

    float nextFirePrimary = 0;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
    }

    public void FirePrimaryWeapon()
    {
        if (Time.time < nextFirePrimary)
            return;

        nextFirePrimary = Time.time + myInfo.CooldownPrimary;

        if (primaryWeapon == null || primaryWeaponShotSpawns.Length == 0)
        {
            Debug.LogError("No primary weapon or transform set, can't fire.");
            return;
        }

        foreach(Transform tf in primaryWeaponShotSpawns)
        {
            //Create projectile.
            GameObject projectile = Instantiate(primaryWeapon, tf.position, tf.rotation) as GameObject;
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
    }
}
