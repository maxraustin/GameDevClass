using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetAcquirer : MonoBehaviour {
    /// <summary>
    /// Returns the closest enemy unit.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>
    /// <returns>Closest enemy unit.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit)
    {
        return GetClosestEnemy(myUnit, int.MaxValue, false);
    }

    /// <summary>
    /// Returns the closest enemy.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>m>
    /// <returns>Closest enemy unit.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit, bool objectiveUnitsOnly)
    {
        return GetClosestEnemy(myUnit, int.MaxValue, objectiveUnitsOnly);
    }

    /// <summary>
    /// Returns the closest enemy.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>m>
    /// <returns>Closest enemy unit.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit, UnitType[] unitTypes, bool objectiveUnitsOnly)
    {
        return GetClosestEnemy(myUnit, unitTypes, int.MaxValue, objectiveUnitsOnly);
    }

    /// <summary>
    /// Returns the closest enemy that is within the given maximum distance.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>
    /// <param name="maxDistance">Maximum distance to find enemies.</param>
    /// <returns>Closest enemy unit within max distance.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit, int maxDistance)
    {
        return GetClosestEnemy(myUnit, maxDistance, false);
    }

    /// <summary>
    /// Returns the closest enemy that is within the given maximum distance.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>
    /// <param name="maxDistance">Maximum distance to find enemies.</param>
    /// <returns>Closest enemy unit within max distance.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit, int maxDistance, bool objectiveUnitsOnly)
    {
        return GetClosestEnemy(myUnit, new[] { UnitType.BATTLESHIP, UnitType.BOMBER, UnitType.CRUISER, UnitType.DRONE, UnitType.FIGHTER, UnitType.STRUCTURE, UnitType.TURRET }, maxDistance, objectiveUnitsOnly);
    }

    /// <summary>
    /// Returns the closest enemy of given UnitTypes that is within the given maximum distance.
    /// </summary>
    /// <param name="myUnit">Unit to find the enemies of.</param>
    /// <param name="unitTypes">Array of unit types that are valid targets.</param>
    /// <param name="maxDistance">Maximum distance to find enemies.</param>
    /// <returns>Closest enemy unit within max distance.</returns>
    public static GameObject GetClosestEnemy(GameObject myUnit, UnitType[] unitTypes, int maxDistance, bool objectiveUnitsOnly)
    {
        List<GameObject> enemies;
        if (objectiveUnitsOnly)
            enemies = UnitTracker.GetObjectiveEnemies(myUnit);
        else
            enemies = UnitTracker.GetActiveEnemies(myUnit);

        GameObject closestEnemy = null;
        float closestEnemyDistance = float.MaxValue;
        foreach(GameObject go in enemies)
        {
            bool isCorrectShipType = false;
            foreach (UnitType ut in unitTypes)
            {
                if (go.GetComponent<UnitInfo>().UnitType == ut)
                {
                    isCorrectShipType = true;
                    break;
                }
            }

            if (!isCorrectShipType)
                continue;

            float dist = (go.transform.position - myUnit.transform.position).magnitude;
            if (dist < closestEnemyDistance && dist <= maxDistance)
            {
                closestEnemyDistance = (go.transform.position - myUnit.transform.position).magnitude;
                closestEnemy = go;
            }
        }

        return closestEnemy;
    }
}
