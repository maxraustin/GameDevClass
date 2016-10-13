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

    [SerializeField]
    GameObject allyIcon;

    [SerializeField]
	GameObject enemyIcon;

	// Use this for initialization
	void Start () {
        //First we need to define what the player object is
        playerObj = UnitTracker.PlayerShip;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		if (playerObj != null) {
            int playerTeamID = -1;
            if (playerObj.GetComponent<UnitInfo>() != null)
                playerTeamID = playerObj.GetComponent<UnitInfo>().TeamID;

            //We update the player's location every physics step
            playerLoc = playerObj.transform.position;
			//Get the active enemies and iterate through the list
			List<GameObject> units = UnitTracker.GetActiveUnits();
			for (int i = 0; i < units.Count; i++) {
                if (units[i] == playerObj) continue;
				if (units[i] != null && units[i].GetComponent<UnitInfo>() != null) {
					//Calculate the distance of each enemy from the player and check if it's outside the range of the radar
					Vector2 distFromPlayer = new Vector2 (playerLoc.x - units[i].transform.position.x, playerLoc.z - units[i].transform.position.z);
					float length = Mathf.Sqrt ((distFromPlayer.x * distFromPlayer.x) + (distFromPlayer.y * distFromPlayer.y));
					if (length < radarRange) {
                        //Draw the unit on the radar, maintain scale, rotation etc
                        if (units[i].GetComponent<UnitInfo>().TeamID != playerTeamID)
                        {
                            Instantiate(enemyIcon, new Vector3((transform.position.x - distFromPlayer.x / (radarRange / 150)), (transform.position.y - distFromPlayer.y / (radarRange / 150)), transform.position.z), Quaternion.identity, transform);
                            enemyIcon.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                        else
                        {
                            Instantiate(allyIcon, new Vector3((transform.position.x - distFromPlayer.x / (radarRange / 150)), (transform.position.y - distFromPlayer.y / (radarRange / 150)), transform.position.z), Quaternion.identity, transform);
                            allyIcon.transform.localScale = new Vector3(1f, 1f, 1f); //Why is this line even here?
                        }
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
