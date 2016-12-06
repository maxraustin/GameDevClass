using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleShipAIController : MonoBehaviour {

	public int throttlePercentage = 15;
	//The default unboosted rotation speed
	[SerializeField]
	float rotationSpeed;
	//The starting cooldown of the boost. Goes to 15 seconds in the final stage
	[SerializeField]
	float boostMovementCooldown;
	[SerializeField]
	bool boosted;
	//The sound effect to play to warn the player that a boost is coming. Lasts 3 seconds
	[SerializeField]
	AudioSource warning;
	//Animator for the jump drive to activate it
	[SerializeField]
	Animator jumpDriveAnim;
	//Debug tool. Just a way to look at the bosses current health. We can remove this if unwanted
	[SerializeField]
	Text bossHealth;

	//Fields for checking components
	[SerializeField]
	Health shieldGeneratorHealth;
	[SerializeField]
	Health jumpDriveHealth;

	//Temporary: This is what we'll use to convey what the boss is doing. IE messages from them that tell you what they're trying to do(hints etc)
	[SerializeField]
	MessageTextController messageTxt;
	//The location that fighters spawn from in the final stage of the fight
	[SerializeField]
	GameObject hangarSpawn;
	//The current phase of the boss fight and a debug tool used to make sure shields were being shut off.
	public int phase;
	public bool shieldsOn;

	float originalRotationSpeed;
	float originalMaxSpeed;

	Health myHealth;
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
		target = GetEasiestTarget();
		myHealth = GetComponent<Health> ();

		phase = 1;
		shieldsOn = true;
		boosted = false;

		if (!UnitTracker.GetActiveUnits().Contains(this.gameObject))
			UnitTracker.AddUnit(this.gameObject);
		// set target on spawn
		messageTxt = GameObject.Find("MessageText").GetComponent<MessageTextController>();
		messageTxt.DisplayMessage ("Their shields are too strong for our weapons. Try to find a weak point.");
	}

	void Update()
	{
		bossHealth.text = myHealth.CurrentHealth.ToString();
		if (shieldGeneratorHealth.CurrentHealth <= 0 && phase == 1) {
			phase = 2;
			StartSecondPhase ();
		}
		if (jumpDriveHealth.CurrentHealth <= 0 && phase == 2) {
			StopCoroutine (SecondPhaseTimer ());
			phase = 3; StartThirdPhase ();
		}
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
				lookRotation = Quaternion.LookRotation(direction, new Vector3(Mathf.Clamp(direction.x,0f,15f),Mathf.Clamp(direction.y,0f,15f),Mathf.Clamp(direction.z,0f,15f)));
				rate *= rotationSpeed;
				//If the player is behind the battleship...
				if (!boosted) {
					EvasiveAction ();
				}
			} else {
				//lookRotation = Quaternion.LookRotation(direction);
				lookRotation = Quaternion.LookRotation(direction, new Vector3(Mathf.Clamp(direction.x,0f,15f),Mathf.Clamp(direction.y,0f,15f),Mathf.Clamp(direction.z,0f,15f)));
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}
	}

	void SetVelocity()
	{
		myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
	}

	//Special Move: Increases mobility briefly
	void EvasiveAction(){
		warning.enabled = true;
		boosted = true;
		originalRotationSpeed = rotationSpeed;
		originalMaxSpeed = myInfo.MaxSpeed;
		StartCoroutine (BoostDelay ());
	}
	//Timer Functions for EvasiveAction
	IEnumerator BoostDelay(){
		yield return new WaitForSeconds (3f);
		warning.enabled = false;
		rotationSpeed *= 5f;
		myInfo.MaxSpeed *= 10f;
		StartCoroutine (BoostTimer ());
	}
	//How long the boost should last. Resets the mobility specs after 3 seconds and starts cooldown
	IEnumerator BoostTimer(){
		yield return new WaitForSeconds (3f);
		rotationSpeed = originalRotationSpeed;
		myInfo.MaxSpeed = originalMaxSpeed;
		StartCoroutine (SpecialCooldown ());
	}
	IEnumerator SpecialCooldown(){
		yield return new WaitForSeconds (boostMovementCooldown);
		boosted = false;
	}
	//-----------------------------------------
	//This is called when the shield generator is destroyed. Starts the second phase of the boss fight
	public void StartSecondPhase(){
		//We activate the hitbox on the jumpdrive and start the timer while also disabling the ships shields.
		messageTxt.DisplayMessage("Their shields are down, and they're trying to escape! Find and destroy the jump drive!");
		jumpDriveAnim.enabled = true;
		myInfo.MaxShields = 0;
		myInfo.ShieldRegenRate = 0f;
		GetComponent<Health> ().AddShields (-500);
		StartCoroutine (SecondPhaseTimer ());
	}
	//60 Second timer for the second phase. We will draw this timer on the canvas for the player too.
	IEnumerator SecondPhaseTimer(){
		yield return new WaitForSeconds (60f);
		//Not sure of an easier way to end the game. THis is just temporary.
		if (jumpDriveHealth != null) {
			UnitTracker.PlayerShip.GetComponent<Health> ().TakeDamage(500);
		}
	}

	//Called when the jump drive is destroyed. Starts spawning ships and the boost timer becomes much shorter, making the boss more difficult
	public void StartThirdPhase(){
		messageTxt.DisplayMessage ("They have no shields and no escape route, finish them off! Watch out for enemy fighters being deployed.");
		boostMovementCooldown = 15f;
		StartCoroutine (SpawnShip ());
	}
	//Timer to spawn ship. Calls itself every 20 seconds
	IEnumerator SpawnShip(){
		yield return new WaitForSeconds (20f);
		UnitSpawner.SpawnUnitsInArea (UnitReferences.EnemyFighter1, 1, hangarSpawn);
		StartCoroutine (SpawnShip ());
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
