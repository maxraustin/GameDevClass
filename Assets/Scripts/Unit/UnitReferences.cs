using UnityEngine;
using System.Collections;

/// <summary>
/// Provides static GameObject references to all unit prefabs.
/// </summary>
public class UnitReferences : MonoBehaviour {
    static GameObject enemyFighter1;
    static GameObject playerFighter1;

    static bool hasInitialized = false;

	static void Initialize()
    {
        if (!hasInitialized)
        {
            enemyFighter1 = Resources.Load("Units/Ships/EnemyFighter1", typeof(GameObject)) as GameObject; 
            playerFighter1 = Resources.Load("Units/Ships/PlayerFighter1", typeof(GameObject)) as GameObject;

            hasInitialized = true;
        }
    }

    public static GameObject EnemyFighter1 { get { if (!hasInitialized) Initialize(); return enemyFighter1; } }
    public static GameObject PlayerFighter1 { get { if (!hasInitialized) Initialize(); return playerFighter1; } }
}
