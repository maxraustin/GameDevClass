using UnityEngine;
using System.Collections;

public class TurretAIController : MonoBehaviour {

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    RandomOffset randomAimOffset;

    GameObject target, turretBody, turretHead;
    WeaponsController wc;

    Vector3 aimOffset = new Vector3(0, -2, 0);

	// Use this for initialization
	void Start () {
        turretBody = transform.Find("TurretBody").gameObject;
        turretHead = transform.Find("TurretHead").gameObject;
        wc = GetComponent<WeaponsController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
            wc.FirePrimaryWeapon();
	}

    void FixedUpdate()
    {
        if (target == null)
        {
            target = UnitTracker.PlayerShip;
            return;
        }


        Rotate();
    }

    void Rotate()
    {
        if (target == null)
            return;

        Quaternion lookRotation = RotationCalculator.RotationToHitTarget(turretHead, wc.PrimaryWeapon, target, randomAimOffset, aimOffset, false);
        turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }
}
