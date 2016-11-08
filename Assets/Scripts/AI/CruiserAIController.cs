using UnityEngine;
using System.Collections;

public class CruiserAIController : MonoBehaviour {

    GameObject target;
    WeaponsController wc;

    float nextTargetAcquireTime = 0;
    float targetAcquireInterval = 3;

    void Start () {
        wc = GetComponent<WeaponsController>();
	}
	
	void Update () {
	    if (target == null && Time.time > nextTargetAcquireTime)
            AcquireNewTarget();

        if (target != null)
            wc.FirePrimaryWeapon();
    }

    void FixedUpdate()
    {
        Rotate();
    }

    void AcquireNewTarget()
    {
        nextTargetAcquireTime = Time.time + targetAcquireInterval;

        UnitType[] targetableTypes = { UnitType.BATTLESHIP, UnitType.CRUISER, UnitType.STRUCTURE, UnitType.TURRET };

        target = TargetAcquirer.GetClosestEnemy(gameObject, targetableTypes, true);

        if (target == null)
            target = TargetAcquirer.GetClosestEnemy(gameObject, targetableTypes, false);

        Debug.Log(target);
    }

    void Rotate()
    {
        if (target == null)
            return;

        Quaternion lookRotation = RotationCalculator.RotationToHitTarget(gameObject, wc.PrimaryWeapon, target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.25f);
    }
}
