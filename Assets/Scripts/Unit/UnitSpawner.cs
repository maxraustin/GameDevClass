using UnityEngine;
using System.Collections;

/// <summary>
/// Provides methods to spawn units in the scene.
/// </summary>
public class UnitSpawner : MonoBehaviour {
    /// <summary>
    /// Spawns a given unit type at given location.
    /// </summary>
    /// <param name="unitType">Type of unit to spawn.</param>
    /// <param name="spawnLocation">Location to spawn the unit.</param>
    /// <returns>Reference to the spawned unit.</returns>
    public static GameObject SpawnUnit(GameObject unitType, Vector3 spawnLocation)
    {
        if (unitType == null)
            throw new MissingReferenceException("Given unitType was null, can't spawn.");
        if (unitType.GetComponent<UnitInfo>() == null)
            throw new MissingComponentException("Given unitType doesn't have a UnitInfo component. Is it a unit?");

        Quaternion lookRotation = Quaternion.identity;
        if (unitType.GetComponent<UnitInfo>().IsPlayerShip)
        {
            lookRotation = RotationCalculator.RotationTowardZero(spawnLocation);
            if(UnitTracker.PlayerShip != null)
                throw new System.Exception("A player ship already exists in this scene and you are trying to instantiate another player ship.");
        }
        else
            lookRotation = RotationCalculator.RotationTowardPlayerShip(spawnLocation);
            
        GameObject unit = Instantiate(unitType, spawnLocation, lookRotation) as GameObject;
        UnitTracker.AddUnit(unit);
        return unit;
    }

    public static GameObject SpawnUnit(GameObject unitType, Vector3 spawnLocation, Quaternion spawnRotation)
    {
        if (unitType == null)
            throw new MissingReferenceException("Given unitType was null, can't spawn.");
        if (unitType.GetComponent<UnitInfo>() == null)
            throw new MissingComponentException("Given unitType doesn't have a UnitInfo component. Is it a unit?");
        if (unitType.GetComponent<UnitInfo>().IsPlayerShip && UnitTracker.PlayerShip != null)
            throw new System.Exception("A player ship already exists in this scene and you are trying to instantiate another player ship.");

        GameObject unit = Instantiate(unitType, spawnLocation, spawnRotation) as GameObject;
        UnitTracker.AddUnit(unit);
        return unit;
    }

    /*
    /// <summary>
    /// Spawns an EnemyFighter1 at given location looking at the player ship.
    /// </summary>
    /// <param name="spawnLocation">Location to spawn the unit.</param>
    /// <returns>Reference to the spawned unit.</returns>
    public static GameObject SpawnEnemyFighter1(Vector3 spawnLocation)
    {
        return SpawnEnemyFighter1(spawnLocation, RotationCalculator.RotationTowardPlayerShip(spawnLocation));
    }

    /// <summary>
    /// Spawns an EnemyFighter1 at given location with given rotation.
    /// </summary>
    /// <param name="spawnLocation">Location to spawn the unit.</param>
    /// <returns>Reference to the spawned unit.</returns>
    public static GameObject SpawnEnemyFighter1(Vector3 spawnLocation, Quaternion spawnRotation)
    {
        return Instantiate(UnitReferences.EnemyFighter1, spawnLocation, spawnRotation) as GameObject;
    }

    /// <summary>
    /// Spawns a PlayerFighter1 at given location looking at the position (0,0,0).
    /// </summary>
    /// <param name="spawnLocation">Location to spawn the unit.</param>
    /// <returns>Reference to the spawned unit.</returns>
    public static GameObject SpawnPlayerFighter1(Vector3 spawnLocation)
    {
        return SpawnPlayerFighter1(spawnLocation, RotationCalculator.RotationTowardZero(spawnLocation));
    }

    /// <summary>
    /// Spawns a PlayerFighter1 at given location with given rotation.
    /// </summary>
    /// <param name="spawnLocation">Location to spawn the unit.</param>
    /// <returns>Reference to the spawned unit.</returns>
    public static GameObject SpawnPlayerFighter1(Vector3 spawnLocation, Quaternion spawnRotation)
    {
        if (UnitTracker.PlayerShip != null)
            throw new System.Exception("A player ship already exists in this scene and you are trying to instantiate another player ship.");

        return Instantiate(UnitReferences.PlayerFighter1, spawnLocation, spawnRotation) as GameObject;
    }   
    */
}
