using UnityEngine;
using System.Collections;

/// <summary>
/// Provides static GameObject references to all unit prefabs.
/// </summary>
public class UnitReferences : MonoBehaviour {
    static GameObject alliedCruiserHeavy1, alliedFighter1, enemyCruiserLight1, enemyFighter1, playerFighter1, enemyBattleship, laserTurret;


    static bool hasInitialized = false;

	static void Initialize()
    {
        if (!hasInitialized)
        {
            alliedCruiserHeavy1 = Resources.Load("Units/Ships/AlliedCruiserHeavy1", typeof(GameObject)) as GameObject;
            alliedFighter1 = Resources.Load("Units/Ships/AlliedFighter1", typeof(GameObject)) as GameObject;

            enemyCruiserLight1 = Resources.Load("Units/Ships/EnemyCruiserLight1", typeof(GameObject)) as GameObject;
            enemyFighter1 = Resources.Load("Units/Ships/EnemyFighter1", typeof(GameObject)) as GameObject;
			enemyBattleship = Resources.Load ("Units/Ships/EnemyBattleship", typeof(GameObject)) as GameObject;
            
            laserTurret = Resources.Load("Units/LaserTurret", typeof(GameObject)) as GameObject;

            playerFighter1 = Resources.Load("Units/Ships/PlayerFighter1", typeof(GameObject)) as GameObject;

            hasInitialized = true;
        }
    }
	public static GameObject EnemyBattleship { get { if (!hasInitialized)Initialize ();return enemyBattleship;} }
    public static GameObject EnemyCruiserLight1 { get { if (!hasInitialized) Initialize(); return enemyCruiserLight1; } }
    public static GameObject EnemyFighter1 { get { if (!hasInitialized) Initialize(); return enemyFighter1; } }
    public static GameObject AlliedFighter1 { get { if (!hasInitialized) Initialize(); return alliedFighter1; } }
    public static GameObject AlliedCruiserHeavy1 { get { if (!hasInitialized) Initialize(); return alliedCruiserHeavy1; } }
    public static GameObject LaserTurret { get { if (!hasInitialized) Initialize(); return laserTurret; } }
    public static GameObject PlayerFighter1 { get { if (!hasInitialized) Initialize(); return playerFighter1; } }
}
