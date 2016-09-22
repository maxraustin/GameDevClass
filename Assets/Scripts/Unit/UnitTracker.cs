using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitTracker : MonoBehaviour
{
    public static GameObject playerShip { get; set; }
    private static List<GameObject> activeEnemies;

    static bool hasInitialized = false;

    static void Initialize()
    {
        if (!hasInitialized)
        {
            activeEnemies = new List<GameObject>();
            hasInitialized = true;
        }
    }

    public static void AddEnemy(GameObject enemy)
    {
        if (!hasInitialized)
            Initialize();

        activeEnemies.Add(enemy);
    }

    public static int GetActiveEnemyCount()
    {
        if (!hasInitialized)
            Initialize();

        //activeEnemies.RemoveAll(enemy => enemy == null);

        return activeEnemies.Count;
    }

    public static List<GameObject> GetActiveEnemies()
    {
        if (!hasInitialized)
            Initialize();

        //activeEnemies.RemoveAll(enemy => enemy == null);

        return activeEnemies;
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        if (!hasInitialized)
            return;

        activeEnemies.Remove(enemy);
    }
}
