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

    // raycast used to get more info about collisions
    RaycastHit hit;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        // set target on spawn
        target = GetNextTarget();
        Debug.Log(target);
    }

    void Update()
    {
        if (target != null && target.name != "ai_waypoint" &&
            Vector3.Angle(target.transform.forward, transform.position - target.transform.position) < 40.0f)
            weaponsController.FirePrimaryWeapon();
    }

    void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position, 5f, transform.TransformDirection(Vector3.forward), out hit, 50 , (1 << LayerMask.NameToLayer("Collidable"))))
        {
            // if already headed to a waypoint, destroy that waypoint
            if (target != null && target.name == "ai_waypoint")
                Destroy(target);

            // generate a random waypoint around the ship and set it as the target
            Vector3 pos = transform.position + (Random.insideUnitSphere * 100);
            GameObject waypoint = new GameObject("ai_waypoint");
            waypoint.transform.position = pos;
            waypoint.AddComponent<WayPointController>();
            target = waypoint;
        }

        if (target == null)
            target = GetNextTarget();

        AdjustThrottle();
        Rotate();
        SetVelocity();
    }

    void OnDisable()
    {
        UnitTracker.RemoveUnit(gameObject);
    }

    void AdjustThrottle()
    {
        if (throttlePercentage < 100)
            throttlePercentage++;
    }

    void Rotate()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnRate);
        }
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }

    GameObject GetNextTarget()
    {
        GameObject closestTarget = null;

        // find the enemy that is closest
        foreach (GameObject enemy in UnitTracker.GetTeam(myInfo.TeamID == 1 ? 0 : 1))
        {
            if ((closestTarget == null) ||
                (Vector3.Distance(enemy.transform.position, this.transform.position)
                > Vector3.Distance(closestTarget.transform.position, this.transform.position)))
            {
                closestTarget = enemy;
            }
        }
        return closestTarget;
    }
}