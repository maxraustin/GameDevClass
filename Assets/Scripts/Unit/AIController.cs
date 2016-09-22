using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    [SerializeField]
    float turnRate;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;

    GameObject target;
    int throttlePercentage = 15;

    void Awake()
    {
        UnitTracker.AddEnemy(gameObject);
    }

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        target = UnitTracker.playerShip;
    }

    void Update()
    {
        weaponsController.FirePrimaryWeapon();
    }

    void FixedUpdate()
    {
        AdjustThrottle();
        Rotate();
        SetVelocity();
    }

    void OnDisable()
    {
        UnitTracker.RemoveEnemy(gameObject);
    }

    void AdjustThrottle()
    {
        if (throttlePercentage < 100)
            throttlePercentage++;
    }

    void Rotate()
    {
        if (target == null)
            return;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRate);
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }
}
