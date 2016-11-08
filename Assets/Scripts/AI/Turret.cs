using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    // local vars
    public int lookSpeed = 10;
    public float turretRange = 300;

    private GameObject targetObject;
    private GameObject turretHead;
    private GameObject turretRadar;
    private Vector3 target;
    private float targetDistance;
    private Quaternion targetRotation;
    private bool startTracking = false;
    private bool turretHeadRotateSwitch = true;
    private int count = 0;
    private float turretRotateSpeed = .50f;
    private float turretHeadRotateSpeed = .25f;
    private float turretRadarRotateSpeed = 4f;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    public Rigidbody myRigidbody;
    SphereCollider sphereCollider;

    // called once when object is instantiated
    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        // reference to head of turret for proper movement
        turretHead = this.gameObject.transform.GetChild(0).gameObject;

        // reference to radar on head of turret for proper movement;
        turretRadar = turretHead.transform.GetChild(0).gameObject;

        // set sphere collider radius to that of turretRange
        sphereCollider.radius = turretRange - 155;
    }

    // called once every second
    void Update()
    {
        // rotate radar at all times
        turretRadar.transform.Rotate(Vector3.forward * turretRadarRotateSpeed, Space.Self);

        // rotate head at all times
        rotateTurretHead();

        // tracking target rotate accordingly
        if (startTracking)
        {
            // find the distance from turret to target
            targetDistance = Vector3.Distance(gameObject.transform.position, target);

            // rotate base of turret
            Debug.Log("Rotate Towards Target");
            target.y = transform.position.y;
            targetRotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);

            // ToDo rotate turretHead along rotation.x to align with target

            // fire primary
            weaponsController.FirePrimaryWeapon();

            // rapid fire secondary
            for (int i = 0; i < 5; i++)
            {
                weaponsController.FireSecondaryWeapon();
            }

            // if target is out of turrets range then stop turning towards target
            if (targetDistance > turretRange)
            {
                startTracking = false;
                Debug.Log("Distance:  " + targetDistance + " Range:  " + turretRange);
            }

            // update the targetObject cords
            target = targetObject.transform.position;
        }
        else // not tracking target... rotate casually
        {
            // auto rotate base of turret
            transform.Rotate(Vector3.up * turretRotateSpeed, Space.Self);
        }
    }

    void rotateTurretHead()
    {
        if(turretHeadRotateSwitch)
        turretHead.transform.Rotate(Vector3.left * turretHeadRotateSpeed, Space.Self);

        if(!turretHeadRotateSwitch)
        turretHead.transform.Rotate(Vector3.right * turretHeadRotateSpeed, Space.Self);

        count++;

        if (count % 180 == 0)
        {
            turretHeadRotateSwitch = (!turretHeadRotateSwitch);
            count = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (targetObject == null) AcquireTarget();

        // if target not specified assume you are targeting playerfighter1
        if (targetObject == null) targetObject = GameObject.Find("PlayerFighter1");

        // target the targetobject cords
        target = targetObject.transform.position;

        // start tracking target
        startTracking = true;
    }

    void AcquireTarget()
    {
        if (targetObject == null)
        {
            GameObject closestEnemy = null;
            foreach (GameObject go in UnitTracker.GetActiveEnemies(gameObject))
            {
                if (closestEnemy == null)
                {
                    closestEnemy = go;
                    continue;
                }

                if ((go.transform.position - transform.position).magnitude < (closestEnemy.transform.position - transform.position).magnitude)
                    closestEnemy = go;
            }

            targetObject = closestEnemy;

            if (targetObject != null)
                target = targetObject.transform.position;
        }
    }
}