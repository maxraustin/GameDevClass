using UnityEngine;
using System.Collections;

/// <summary>
/// Provides static GameObject references to all unit prefabs.
/// </summary>
public class UnitReferences : MonoBehaviour {
    static GameObject alliedFighter1, enemyCruiser1, enemyFighter1, playerFighter1;


    static bool hasInitialized = false;

	static void Initialize()
    {
        if (!hasInitialized)
        {
            enemyCruiser1 = Resources.Load("Units/Ships/EnemyCruiser1", typeof(GameObject)) as GameObject;
            enemyFighter1 = Resources.Load("Units/Ships/EnemyFighter1", typeof(GameObject)) as GameObject;
            alliedFighter1 = Resources.Load("Units/Ships/AlliedFighter1", typeof(GameObject)) as GameObject;
            playerFighter1 = Resources.Load("Units/Ships/PlayerFighter1", typeof(GameObject)) as GameObject;

            hasInitialized = true;
        }
    }
    public static GameObject EnemyCruiser1 { get { if (!hasInitialized) Initialize(); return enemyCruiser1; } }
    public static GameObject EnemyFighter1 { get { if (!hasInitialized) Initialize(); return enemyFighter1; } }
    public static GameObject AlliedFighter1 { get { if (!hasInitialized) Initialize(); return alliedFighter1; } }
    public static GameObject PlayerFighter1 { get { if (!hasInitialized) Initialize(); return playerFighter1; } }
}
