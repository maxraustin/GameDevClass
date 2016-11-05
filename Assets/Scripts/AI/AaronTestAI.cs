using UnityEngine;
using System.Collections;

public class AaronTestAI : MonoBehaviour
{
    [SerializeField]
    float turnRate;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;

    GameObject target;
    int throttlePercentage = 15;

    // raycast used to get more info about collisions
    RaycastHit hit;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        weaponsController.FirePrimaryWeapon();
    }

    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = RotationCalculator.RotationToHitTarget(gameObject, weaponsController.PrimaryWeapon, target, RandomOffset.SMALL);
            transform.rotation = lookRotation;//Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRate);
        }
        else
            target = UnitTracker.PlayerShip;
    }

}