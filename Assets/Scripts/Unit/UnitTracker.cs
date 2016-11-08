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
    private static List<GameObject> objectiveUnits;

    static int playerTeamID = -1;
    static bool hasInitialized = false;

    static void Initialize()
    {
        if (!hasInitialized)
        {
            activeUnits = new List<GameObject>();
            objectiveUnits = new List<GameObject>();
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
            {
                activeUnits.Add(playerShip);
                playerTeamID = playerShip.GetComponent<UnitInfo>().TeamID;
            }
        }
    }

    /// <summary>
    /// Adds a unit gameObject to the list of active units and the list of objective units.
    /// </summary>
    /// <param name="unit">Unit to add to the lists.</param>
    public static void AddObjectiveUnit(GameObject unit)
    {
        if (!hasInitialized)
            Initialize();

        if (!unit.tag.Equals("Unit"))
            throw new System.Exception("You are trying to add a gameobject to the active units list but it doesn't seem to be a unit.");
        if (unit.GetComponent<UnitInfo>() == null)
            throw new MissingComponentException("You are trying to add a gameobject to the active units list but it doesn't have a UnitInfo component.");

        activeUnits.Add(unit);
        objectiveUnits.Add(unit);
    }

    /// <summary>
    /// Add a unit gameObject to the list of active units.
    /// </summary>
    /// <param name="unit">Unit to add to the list..</param>
    public static void AddUnit(GameObject unit)
    {
        if (!hasInitialized)
            Initialize();

        if (!unit.tag.Equals("Unit"))
            throw new System.Exception("You are trying to add a gameobject to the active units list but it doesn't seem to be a unit.");
        if (unit.GetComponent<UnitInfo>() == null)
            throw new MissingComponentException("You are trying to add a gameobject to the active units list but it doesn't have a UnitInfo component.");

        activeUnits.Add(unit);
    }

    /// <summary>
    /// Removes all unit references in the unit tracker.
    /// </summary>
    public static void Clear()
    {
        playerShip = null;
        playerTeamID = -1;
        if (activeUnits != null)
            activeUnits.Clear();
        if (objectiveUnits != null)
            objectiveUnits.Clear();
    }

    /// <summary>
    /// Returns the amount of active units that are enemies of the player.
    /// </summary>
    /// <returns>Count of units that are enemies of the player.</returns>
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

    /// <summary>
    /// Returns all the active units that are allies of the player. Does not return the player's unit.
    /// </summary>
    /// <returns>List of units that are allies of the player. Player's unit is not included in the list.</returns>
    public static List<GameObject> GetActiveAllies()
    {
        if (playerTeamID == -1)
            return new List<GameObject>();
        else {
            List<GameObject> alliedUnits = GetActiveUnits(playerTeamID, true);
            alliedUnits.Remove(playerShip);
            return alliedUnits;
        }
    }

    /// <summary>
    /// Returns all the active units that are on the given team.
    /// </summary>
    /// <param name="teamID">Team ID of the units you want to get.</param>
    /// <returns>List of units that are on the given team.</returns>
    public static List<GameObject> GetActiveAllies(int teamID)
    {
        return GetActiveUnits(teamID, true);
    }

    /// <summary>
    /// Returns all the active units that are allies of the given GameObject. Does not return the given GameObject.
    /// </summary>
    /// <param name="go">GameObject (unit or projectile) who's allies you want to get.</param>
    /// <returns>List of units that are allies of the given GameObject. Given GameObject is not included in the list.</returns>
    public static List<GameObject> GetActiveAllies(GameObject go)
    {
        int teamID = -1;
        if (go.GetComponent<UnitInfo>() != null)
            teamID = go.GetComponent<UnitInfo>().TeamID;
        else if (go.GetComponent<ProjectileInfo>() != null)
            teamID = go.GetComponent<ProjectileInfo>().TeamID;

        if (teamID == -1)
            return new List<GameObject>();
        else {
            List<GameObject> alliedUnits = GetActiveUnits(teamID, true);
            alliedUnits.Remove(go);
            return alliedUnits;
        }
    }

    /// <summary>
    /// Returns all the active units that are enemies of the player.
    /// </summary>
    /// <returns>List of units that are allies of the player.</returns>
    public static List<GameObject> GetActiveEnemies()
    {
        if (playerTeamID == -1)
            return new List<GameObject>();
        else
            return GetActiveUnits(playerTeamID, false);
    }

    /// <summary>
    /// Returns all the active units that are not on the given team.
    /// </summary>
    /// <param name="teamID">Team who's enemies you want to get.</param>
    /// <returns>List of units that are not on the given team.</returns>
    public static List<GameObject> GetActiveEnemies(int teamID)
    {
        return GetActiveUnits(teamID, false);
    }

    /// <summary>
    /// Returns all the active units that are enemies of the given GameObject.
    /// </summary>
    /// <param name="go">GameObject (unit or projectile) who's enemies you want to get.</param>
    /// <returns>List of units that are enemies of the given GameObject.</returns>
    public static List<GameObject> GetActiveEnemies(GameObject go)
    {
        int teamID = -1;
        if (go.GetComponent<UnitInfo>() != null)
            teamID = go.GetComponent<UnitInfo>().TeamID;
        else if (go.GetComponent<ProjectileInfo>() != null)
            teamID = go.GetComponent<ProjectileInfo>().TeamID;

        if (teamID == -1)
            return new List<GameObject>();
        else
            return GetActiveUnits(teamID, false);
    }

    /// <summary>
    /// Returns all the active units in the scene.
    /// </summary>
    /// <returns>List of all units in the scene.</returns>
    public static List<GameObject> GetActiveUnits()
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeUnits.RemoveAll(unit => unit == null);

        return activeUnits;
    }

    /// <summary>
    /// Returns all the active units in the scene that belong to the given team.
    /// </summary>
    /// <param name="teamID">Team ID of the units you want to get.</param>
    /// <returns>List of units that are on the given team.</returns>
    public static List<GameObject> GetActiveUnits(int teamID)
    {
        return GetActiveUnits(teamID, true);
    }

    /// <summary>
    /// Returns a list of all active units belonging to a given team in the scene.
    /// </summary>
    /// <param name="teamID">A team ID.</param>
    /// <param name="onTeam">True if you want all the units on the given teamID, false if you want all the units not on the given teamID.</param>
    /// <returns>List of units.</returns>
    public static List<GameObject> GetActiveUnits(int teamID, bool onTeam)
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeUnits.RemoveAll(unit => unit == null);

        //Create a list of units from activeUnits.
        List<GameObject> units = new List<GameObject>();
        foreach (GameObject go in activeUnits)
        {
            if (go.GetComponent<UnitInfo>() != null)
            {
                if (onTeam && go.GetComponent<UnitInfo>().TeamID == teamID)
                    units.Add(go);
                else if (!onTeam && go.GetComponent<UnitInfo>().TeamID != teamID)
                    units.Add(go);
            }
        }

        return units;
    }

    /// <summary>
    /// Returns all the active units that are enemies of the player.
    /// </summary>
    /// <returns>List of units that are allies of the player.</returns>
    public static List<GameObject> GetObjectiveEnemies()
    {
        if (playerTeamID == -1)
            return new List<GameObject>();
        else
            return GetObjectiveUnits(playerTeamID, false);
    }

    /// <summary>
    /// Returns all the active units that are not on the given team.
    /// </summary>
    /// <param name="teamID">Team who's enemies you want to get.</param>
    /// <returns>List of units that are not on the given team.</returns>
    public static List<GameObject> GetObjectiveEnemies(int teamID)
    {
        return GetObjectiveUnits(teamID, false);
    }

    /// <summary>
    /// Returns all the active units that are enemies of the given GameObject.
    /// </summary>
    /// <param name="go">GameObject (unit or projectile) who's enemies you want to get.</param>
    /// <returns>List of units that are enemies of the given GameObject.</returns>
    public static List<GameObject> GetObjectiveEnemies(GameObject go)
    {
        int teamID = -1;
        if (go.GetComponent<UnitInfo>() != null)
            teamID = go.GetComponent<UnitInfo>().TeamID;
        else if (go.GetComponent<ProjectileInfo>() != null)
            teamID = go.GetComponent<ProjectileInfo>().TeamID;

        if (teamID == -1)
            return new List<GameObject>();
        else
            return GetObjectiveUnits(teamID, false);
    }

    public static List<GameObject> GetObjectiveUnits(int teamID, bool onTeam)
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        objectiveUnits.RemoveAll(unit => unit == null);

        //Create a list of units from activeUnits.
        List<GameObject> units = new List<GameObject>();
        foreach (GameObject go in objectiveUnits)
        {
            if (go.GetComponent<UnitInfo>() != null)
            {
                if (onTeam && go.GetComponent<UnitInfo>().TeamID == teamID)
                    units.Add(go);
                else if (!onTeam && go.GetComponent<UnitInfo>().TeamID != teamID)
                    units.Add(go);
            }
        }

        return units;
    }


    /// <summary>
    /// Remove a unit gameObject from the list of active and objective units.
    /// </summary>
    /// <param name="unit">Reference to the unit gameobject to remove from the lists.</param>
    public static void RemoveUnit(GameObject unit)
    {
        if (!hasInitialized)
            return;

        activeUnits.Remove(unit);
        objectiveUnits.Remove(unit);
    }
}
