using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TargettingController : MonoBehaviour {
    static TargettingController instance;

	//Text to show the title of the target
	[SerializeField]
	Text targetTitle;
	//Placeholder. Text to show the targets shields
	[SerializeField]
	Text targetShield;
	//Placeholder. Text to show the targets hull integrity
	[SerializeField]
	Text targetHealth;
	[SerializeField]
	GameObject targetIcon;
	//This field will be populated by the current targets location in camera space. Used to draw on the canvas
	Vector2 targetLoc;
	//Players ship.
	GameObject playerObj;
	//Players camera
	Camera playerCam;
	//Current target
	GameObject target;

	[SerializeField]
	int targetIndex;

    public static TargettingController Instance { get { return instance; } }
    public GameObject Target { get { return target; } }

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
		playerObj = UnitTracker.PlayerShip;
		playerCam = playerObj.GetComponentInChildren<Camera> ();
	}
	void SwitchTarget(){
		//Get the list of enemies...
		List<GameObject> units = UnitTracker.GetActiveEnemies ();
		//cycle through the list of enemies...
		for (int i = 0; i < units.Count; i++) {
			if (units [i] != null && units [i].GetComponent<UnitInfo> () != null) {
				//First, we need to check if we currently have a target. If we don't just assign the first one for now
				if (target == null || !target.activeInHierarchy) {
					target = units [i];
					targetIndex = i;
				}
				//If the current unit being checked ISN'T the target and is closest to the reticle then pick that one
				targetLoc = playerCam.WorldToScreenPoint(units[i].transform.position );
				Vector2 currentTargetLoc = playerCam.WorldToScreenPoint (target.transform.position);
				//We also want to check if the target is within the camera's view.
				if (target != units [i] && (units[i].GetComponent<Renderer>().isVisible)) {
					//Now, we check to see if the new target is CLOSER to the reticle than the current target. If so, switch targets.
					if (     Mathf.Sqrt((targetLoc.x - (Screen.width/2f)) * (targetLoc.x - (Screen.width/2f)) + (targetLoc.y - (Screen.height/2f)) * (targetLoc.y - (Screen.height/2f))) < 				//if distance1 from center is less than
						Mathf.Sqrt((currentTargetLoc.x - (Screen.width/2f)) * (currentTargetLoc.x - (Screen.width/2f)) + (currentTargetLoc.y - (Screen.height/2f)) * (currentTargetLoc.y - (Screen.height/2f)))    ) { // Distance 2...
						//After that sinful line of code we assign the target if the current one being checked is closest to the center of the screen (reticle)
						target = units [i];
						targetIndex = i;
						targetTitle.text = target.GetComponent<UnitInfo> ().ShipTitle;
						targetIcon.GetComponent<AudioSource> ().Play ();
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//We check for the input on targetting
		if (Input.GetButtonDown("Target"))
			SwitchTarget ();
	}

	void FixedUpdate(){
		//If we have a target, we get it's screen point and draw the icon there
		if (target != null && target.activeInHierarchy) {
			targetIcon.GetComponent<Image> ().enabled = true;
			Vector2 targetScreenLoc = playerCam.WorldToScreenPoint (target.transform.position);
			targetIcon.transform.position = targetScreenLoc;
			targetTitle.transform.position = targetScreenLoc;
			//NOTE: Below we can put stuff for reading out titles/health/shields whatever
			targetTitle.enabled = true;
			//If the target leaves the view, we unlock and reset
			if (!target.GetComponent<Renderer> ().isVisible) {
				targetIcon.GetComponent<Image> ().enabled = false;
				target = null;
				targetIndex = 0;
				targetTitle.enabled = false;

			}
		} 
		//If we detect that our target has been destroyed...
		else {
			targetTitle.enabled = false;
			targetIcon.GetComponent<Image> ().enabled = false;
			target = null;
			targetIndex = 0;
		}
	}
}
