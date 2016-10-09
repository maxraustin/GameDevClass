using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Non-instantiated singleton class that tracks all units in the scene.
/// </summary>
public class UnitTracker : MonoBehaviour
{
    private static GameObject playerShip;
    private static List<GameObject> activeUnits;

    static int playerTeamID = -1;
    static bool hasInitialized = false;

    static void Initialize()
    {
        if (!hasInitialized)
        {
            activeUnits = new List<GameObject>();
            hasInitialized = true;
        }
    }

    public static GameObject PlayerShip
    {
        get
        {
            return playerShip;
        }
        set
        {
            playerShip = value;
            if (playerShip != null && playerShip.GetComponent<UnitInfo>() != null)
                playerTeamID = playerShip.GetComponent<UnitInfo>().TeamID;
        }
    }

    public static void AddUnit(GameObject enemy)
    {
        if (!hasInitialized)
            Initialize();

        activeUnits.Add(enemy);
    }

    public static void Clear()
    {
        playerShip = null;
        playerTeamID = -1;
        activeUnits.Clear();
    }

    public static int GetActiveEnemyCount()
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeUnits.RemoveAll(unit => unit == null);

        //Count enemy units in activeUnits.
        int enemyCount = 0;
        foreach(GameObject go in activeUnits)
        {
            if (playerTeamID != -1 && go != null && go.GetComponent<UnitInfo>() != null && go.GetComponent<UnitInfo>().TeamID != playerTeamID)
                enemyCount++;
        }

        return enemyCount;
    }

    public static List<GameObject> GetActiveEnemies()
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeUnits.RemoveAll(unit => unit == null);

        //Create a list of enemy units from activeUnits.
        List<GameObject> enemies = new List<GameObject>();
        foreach (GameObject go in activeUnits)
        {
            if (playerTeamID != -1 && go != null && go.GetComponent<UnitInfo>() != null && go.GetComponent<UnitInfo>().TeamID != playerTeamID)
                enemies.Add(go);
        }

        return enemies;
    }

    public static List<GameObject> GetAllActiveUnits()
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeUnits.RemoveAll(unit => unit == null);

        if (playerShip != null)
            activeUnits.Add(playerShip);

        return activeUnits;
    }

    public static void RemoveUnit(GameObject unit)
    {
        if (!hasInitialized)
            return;

        activeUnits.Remove(unit);
    }
}
