﻿using UnityEngine;
using System.Collections;

public class CruiserAIController : MonoBehaviour {

    [SerializeField]
    float rotationFactor;

    [SerializeField]
    public GameObject[] goToPoints;
    public int WayPointNumber = 0;
    GameObject target;
    WeaponsController wc;
    Rigidbody myRigidbody;
    UnitInfo myInfo;

    float nextTargetAcquireTime = 0;
    float targetAcquireInterval = 3;

    int currentThrottle = 0;

    void Start () {
        wc = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();
        myInfo = GetComponent<UnitInfo>();
    }
	
	void Update () {
	    if (target == null && Time.time > nextTargetAcquireTime)
            AcquireNewTarget();

        if (target != null & goToPoints == null)
            wc.FirePrimaryWeapon();
    }

    void FixedUpdate()
    {
        AdjustThrottle();
        Rotate();
        SetVelocity();
    }

    void AcquireNewTarget()
    {
        if (goToPoints != null)
        {
            if (WayPointNumber < goToPoints.Length)
            {
                target = goToPoints[WayPointNumber];
            }
        }
        else {
            nextTargetAcquireTime = Time.time + targetAcquireInterval;

            UnitType[] targetableTypes = { UnitType.BATTLESHIP, UnitType.CRUISER, UnitType.STRUCTURE, UnitType.TURRET };

            target = TargetAcquirer.GetClosestEnemy(gameObject, targetableTypes, true);

            if (target == null)
                target = TargetAcquirer.GetClosestEnemy(gameObject, targetableTypes, false);

            //Debug.Log(target);
        }
    }

    void AdjustThrottle()
    {
        if (target == null)
        {
            if (currentThrottle > 0)
                currentThrottle--;
            return;
        }
        if (goToPoints != null)
        {
            if ((target.transform.position - transform.position).magnitude > 30)
            {
                if (currentThrottle < 75)
                    currentThrottle++;
            }
            else
            {
                if (currentThrottle > 0)
                {
                    currentThrottle = 0;
                    if (WayPointNumber < goToPoints.Length)
                    {
                        WayPointNumber++;
                        AcquireNewTarget();
                    }
                    else
                    {
                        goToPoints = null;
                        AcquireNewTarget();
                    }
                }
                
            }
        }
        else {
            if ((target.transform.position - transform.position).magnitude > 250)
            {
                if (currentThrottle < 100)
                    currentThrottle++;
            }
            else
            {
                if (currentThrottle > 0)
                    currentThrottle--;
            }
        }
    }

    void Rotate()
    {
        if (target == null)
            return;

        Quaternion lookRotation = RotationCalculator.RotationToHitTarget(gameObject, wc.PrimaryWeapon, target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationFactor);
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed  * ((float)currentThrottle / 100);
    }

    void onTriggerEnter(Collider other)
    {
        WayPointNumber++;
    }
}
