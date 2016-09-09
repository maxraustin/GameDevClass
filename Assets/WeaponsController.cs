using UnityEngine;
using System.Collections;

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

        nextFirePrimary = Time.time + myInfo.GetCooldownPrimary();

        if (primaryWeapon == null || primaryWeaponShotSpawns.Length == 0)
        {
            Debug.LogError("No primary weapon or transform set, can't fire.");
            return;
        }

        foreach(Transform tf in primaryWeaponShotSpawns)
        {
            GameObject projectile = Instantiate(primaryWeapon, tf.position, tf.rotation) as GameObject;
            projectile.GetComponent<ProjectileInfo>().owner = gameObject;
        }
    }
}
