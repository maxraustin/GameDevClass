using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadarController : MonoBehaviour {
	//Vector for holding the player's world location
	Vector3 playerLoc;
	Vector2 radarCenter;

	/// The range in units that the radar sees
	public float radarRange;
	//the player's ship
	public GameObject playerObj;

	public GameObject enemyIcon;
	// Use this for initialization
	void Start () {
		//First we need to define what the player object is
		//playerObj = UnitTracker.PlayerShip; ***NOTE: Is this not working properly?
		//Temporary test soln below
		playerObj = GameObject.Find("PlayerFighter1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (playerObj != null) {
			//We update the player's location every physics step
			playerLoc = playerObj.transform.position;
			//Get the active enemies and iterate through the list
			List<GameObject> enemies = UnitTracker.GetActiveEnemies ();
			for (int i = 0; i < enemies.Count; i++) {
				if (enemies [i] != null) {
					//Calculate the distance of each enemy from the player and check if it's outside the range of the radar
					Vector2 distFromPlayer = new Vector2 (playerLoc.x - enemies [i].transform.position.x, playerLoc.z - enemies [i].transform.position.z);
					float length = Mathf.Sqrt ((distFromPlayer.x * distFromPlayer.x) + (distFromPlayer.y * distFromPlayer.y));
					if (length < radarRange) {
						//Draw the enemy on the radar, maintain scale, rotation etc
						Instantiate (enemyIcon, new Vector3 ((transform.position.x - distFromPlayer.x / (radarRange/150)), (transform.position.y - distFromPlayer.y / (radarRange/150)), transform.position.z), Quaternion.identity, transform);
						enemyIcon.transform.localScale = new Vector3 (1f, 1f, 1f);
					}
				}
			}
			//Get the player ships rotation
			Vector3 playerRotation = playerObj.transform.eulerAngles;
			//Rotate our radar with the player ship, but only on one axis
			transform.eulerAngles = new Vector3 (transform.rotation.x, transform.rotation.y, -playerRotation.y);
		}
	}
}
