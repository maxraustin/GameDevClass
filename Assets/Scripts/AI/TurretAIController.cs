using UnityEngine;
using System.Collections;

public class TurretAIController : MonoBehaviour {

    [SerializeField]
    bool isChildTurret;

    [SerializeField]
    bool isInvulnerable;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    RandomOffset randomAimOffset;

    [SerializeField]
    int maxTargetDistance = 500;

    GameObject target, turretBody, turretHead;
    WeaponsController wc;

    Vector3 aimOffset = new Vector3(0, -2, 0);
    float nextTargetAcquireTime = 0;
    float targetAcquireInterval = 3;
    bool canFire = true;

	// Use this for initialization
	void Start () {
        turretBody = transform.Find("TurretBody").gameObject;
        turretHead = transform.Find("TurretHead").gameObject;
        wc = GetComponent<WeaponsController>();

        if (isChildTurret)
            GetComponent<UnitInfo>().TeamID = transform.parent.GetComponent<UnitInfo>().TeamID;

        if (isInvulnerable)
            GetComponent<CapsuleCollider>().enabled = false;
        else
            GetComponent<CapsuleCollider>().enabled = true;

        if (!GetComponent<UnitInfo>().NotTargettable)
            UnitTracker.AddUnit(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null && canFire)
            wc.FirePrimaryWeapon();

        if (Time.time > nextTargetAcquireTime)
            AcquireNewTarget();
	}

    void FixedUpdate()
    {
        Rotate();
    }

    void AcquireNewTarget()
    {
        nextTargetAcquireTime = Time.time + targetAcquireInterval;

        target = TargetAcquirer.GetClosestEnemy(gameObject, maxTargetDistance);
    }

    void Rotate()
    {
        if (target == null)
            return;

        Quaternion lookRotation = RotationCalculator.RotationToHitTarget(turretHead, wc.PrimaryWeapon, target, randomAimOffset, aimOffset, false);
        turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        if (turretHead.transform.localRotation.eulerAngles.x > 30 && turretHead.transform.localRotation.eulerAngles.x <= 90)
            canFire = false;
        else
            canFire = true;
    }
}
