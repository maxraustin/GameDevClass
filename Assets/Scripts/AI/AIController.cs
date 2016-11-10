using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    public int throttlePercentage = 15;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;
    GameObject target;
    RaycastHit hit; // raycast used to get more info about collisions
    float targetTimer; // Amount of time target has been set 
    float maxTargetLength = 10.0f; // Maximum amount of time to have a target before searching for a new one

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        // set target on spawn
        target = GetEasiestTarget();

        if (!UnitTracker.GetActiveUnits().Contains(this.gameObject))
            UnitTracker.AddUnit(this.gameObject);
    }

    void Update()
    {
        if (target != null && target.name != "ai_waypoint" &&
            Vector3.Angle(transform.forward, target.transform.position - transform.position) < 30.0f)
            weaponsController.FirePrimaryWeapon();

        targetTimer += Time.deltaTime;
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
            targetTimer = 0;
        }

        if (target == null || targetTimer > maxTargetLength)
            target = GetEasiestTarget();

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
            Quaternion lookRotation;
            float rate = Time.deltaTime;
            if (Vector3.Angle(transform.forward, target.transform.position - transform.position) > 30.0f) {
                lookRotation = Quaternion.LookRotation(direction, direction);
                rate *= .75f;
            } else {
                lookRotation = Quaternion.LookRotation(direction);
                rate *= .5f;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rate);
        }
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }

    // Gets the closest enemy
    GameObject GetClosestTarget() {
        GameObject closestTarget = null;
        
        foreach (GameObject enemy in UnitTracker.GetActiveEnemies(myInfo.TeamID)) {
            if ((closestTarget == null) ||
                (Vector3.Distance(enemy.transform.position, this.transform.position)
                > Vector3.Distance(closestTarget.transform.position, this.transform.position))) {
                closestTarget = enemy;
            }
        }

        targetTimer = 0;
        return closestTarget;
    }

    // Gets the enemy closest to crosshairs
    GameObject GetEasiestTarget()
    {
        GameObject easiestTarget = null;

        foreach (GameObject enemy in UnitTracker.GetActiveEnemies(myInfo.TeamID)) {
            if ((easiestTarget == null) ||
                (Vector3.Angle(transform.forward, easiestTarget.transform.position - transform.position)
                > Vector3.Angle(transform.forward, enemy.transform.position - transform.position))) {
                easiestTarget = enemy;
            }
        }

        targetTimer = 0;
        return easiestTarget;
    }
}