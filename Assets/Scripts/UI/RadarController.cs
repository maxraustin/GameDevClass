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

    List<GameObject> allyIcons;
    List<GameObject> enemyIcons;

    // Use this for initialization
    void Start () {
        //First we need to define what the player object is
        playerObj = UnitTracker.PlayerShip;

        allyIcons = new List<GameObject>();
        enemyIcons = new List<GameObject>();
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
			List<GameObject> enemies = UnitTracker.GetActiveEnemies();
            int requiredEnemyIcons = 0;
			for (int i = 0; i < enemies.Count; i++) {
				if (enemies[i] != null && enemies[i].GetComponent<UnitInfo>() != null) {
					//Calculate the distance of each enemy from the player and check if it's outside the range of the radar
					Vector2 distFromPlayer = new Vector2 (playerLoc.x - enemies[i].transform.position.x, playerLoc.z - enemies[i].transform.position.z);
					float length = Mathf.Sqrt ((distFromPlayer.x * distFromPlayer.x) + (distFromPlayer.y * distFromPlayer.y));
					if (length < radarRange) {
                        //Draw the unit on the radar, maintain scale, rotation etc

                        Vector3 pos = new Vector3((transform.position.x - distFromPlayer.x / (radarRange / 150)), (transform.position.y - distFromPlayer.y / (radarRange / 150)), transform.position.z);

                        if (requiredEnemyIcons < enemyIcons.Count)
                            enemyIcons[requiredEnemyIcons].transform.position = pos;
                        else
                            enemyIcons.Add(PoolController.Instance.GetObject(enemyIcon, pos, Quaternion.identity, transform));

                        requiredEnemyIcons++;
					}
				}
			}
            //Remove unneccessary enemy icons.
            for (int i = enemyIcons.Count - 1; i >= requiredEnemyIcons; i--)
            {
                enemyIcons[i].SetActive(false);
                enemyIcons.Remove(enemyIcons[i]);
            }

            List<GameObject> allies = UnitTracker.GetActiveAllies();
            int requiredAllyIcons = 0;
            for (int i = 0; i < allies.Count; i++)
            {
                if (allies[i] == playerObj) continue;
                if (allies[i] != null && allies[i].GetComponent<UnitInfo>() != null)
                {
                    //Calculate the distance of each enemy from the player and check if it's outside the range of the radar
                    Vector2 distFromPlayer = new Vector2(playerLoc.x - allies[i].transform.position.x, playerLoc.z - allies[i].transform.position.z);
                    float length = Mathf.Sqrt((distFromPlayer.x * distFromPlayer.x) + (distFromPlayer.y * distFromPlayer.y));
                    if (length < radarRange)
                    {
                        //Draw the unit on the radar, maintain scale, rotation etc
                        Vector3 pos = new Vector3((transform.position.x - distFromPlayer.x / (radarRange / 150)), (transform.position.y - distFromPlayer.y / (radarRange / 150)), transform.position.z);

                        if (requiredAllyIcons < allyIcons.Count)
                            allyIcons[requiredAllyIcons].transform.position = pos;
                        else
                            allyIcons.Add(PoolController.Instance.GetObject(allyIcon, pos, Quaternion.identity, transform));

                        requiredAllyIcons++;
                    }
                }
            }
            //Remove unneccessary ally icons.
            for (int i = allyIcons.Count - 1; i >= requiredAllyIcons; i--)
            {
                allyIcons[i].SetActive(false);
                allyIcons.Remove(allyIcons[i]);
            }

            //Get the player ships rotation
            Vector3 playerRotation = playerObj.transform.eulerAngles;
			//Rotate our radar with the player ship, but only on one axis
			transform.eulerAngles = new Vector3 (transform.rotation.x, transform.rotation.y, -playerRotation.y);
		}
    }
}
