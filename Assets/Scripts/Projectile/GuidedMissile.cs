using UnityEngine;
using System.Collections;

public class GuidedMissile : MonoBehaviour {
    // local vars
    public int lookSpeed = 5;
    public float timeTillTrack = 0;
    public float distanceTillStopLooking = 1000;
    public int timeTillExpire = 30;

    private float speed;
    private GameObject targetObject;
    private Vector3 target;
    private float timer;
    private float calculatedDistance;
    private bool stopTurning;
    private Quaternion targetRotation;
    private bool destroy;
    bool ownerAndTeamSet = false;
    // called once when object is instantiated
    void Start()
    {
        // set speed of guided missile by accessing ProjectileInfo component -- 53 recommended speed
        speed = GetComponent<ProjectileInfo>().Speed;
    }

    void OnEnable() {
        
    }

    void OnDisable()
    {
        GetComponent<ProjectileInfo>().Owner = null;
        GetComponent<ProjectileInfo>().TeamID = -1;

        ownerAndTeamSet = false;
    }

    void CheckIfOwnerAndTeamSet() { 
        if (GetComponent<ProjectileInfo>().Owner != null && GetComponent<ProjectileInfo>().TeamID > -1)
        {
            ownerAndTeamSet = true;
            AcquireTarget();
        }
    }

    // called once every second
    void Update()
    {
        // set up the timer
        timer += Time.deltaTime;

        // give the missile speed
        transform.Translate(0, 0, speed / 100);

        if (!ownerAndTeamSet)
        {
            CheckIfOwnerAndTeamSet();
            return;
        }

        if (targetObject == null || targetObject.activeInHierarchy == false)
        {
            AcquireTarget();
            return;
        }

        // find the distance from missile to target
        calculatedDistance = Vector3.Distance(gameObject.transform.position, target);

        // delay tracking for a certain amount of time if provided...
        if (timer > timeTillTrack)
        {
            // if still alive and target within range turn to target and update target cords
            if (stopTurning == false)
            {
                // look at the target object at speed
                targetRotation = Quaternion.LookRotation(target - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);

                // target the targetObject cords
                target = targetObject.transform.position;
            }
        }

        // if target is out of missile's range then stop turning towards target
        if (calculatedDistance > distanceTillStopLooking)
        {
            stopTurning = true;    
        }

    }

    /// <summary>
    /// Find the closest enemy unit and sets it as our target.
    /// </summary>
    void AcquireTarget()
    {
        if (!ownerAndTeamSet)
            return;

        if (targetObject == null || targetObject.activeInHierarchy == false)
        {
            //If our owner is the player: Set our target as the player's target.
            if (GetComponent<ProjectileInfo>().Owner == UnitTracker.PlayerShip && TargettingController.Instance != null)
                targetObject = TargettingController.Instance.Target;

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
            }

            if (targetObject != null)
                target = targetObject.transform.position;
        }
    }

    // used to prevent missiles from merging into one object
    void OnTriggerEnter(Collider other)
    {
        // if collision with other missile then vary speed of missile
        if (other.GetComponent<GuidedMissile>())
        {
            speed -= (Random.Range(5, 10)); 
        }
    }

    void OnTriggerExit()
    {
        // return speed back to normal when no collission detected
         speed = GetComponent<ProjectileInfo>().Speed;
    }
}
