using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Non-instantiated singleton class that tracks all projectiles in the scene.
/// </summary>
public class ProjectileTracker : MonoBehaviour
{
    private static List<GameObject> activeProjectiles;

    static bool hasInitialized = false;

    static void Initialize()
    {
        if (!hasInitialized)
        {
            activeProjectiles = new List<GameObject>();
            hasInitialized = true;
        }
    }

    /// <summary>
    /// Add a projectile gameObject to the list of active projectiles.
    /// </summary>
    /// <param name="projectile">Reference to the projectile gameobject to add to the list.</param>
    public static void AddProjectile(GameObject projectile)
    {
        if (!hasInitialized)
            Initialize();

        if (!projectile.tag.Equals("Projectile")) 
            throw new System.Exception("You are trying to add a gameobject to the active projectiles list but it doesn't seem to be a projectile.");
        if (projectile.GetComponent<ProjectileInfo>() == null)
            throw new MissingComponentException("You are trying to add a gameobject to the active projectiles list but it doesn't have a ProjectileInfo component.");

        activeProjectiles.Add(projectile);
    }

    /// <summary>
    /// Removes all references in the projectile tracker.
    /// </summary>
    public static void Clear()
    {
        if(activeProjectiles != null)
            activeProjectiles.Clear();
    }


    /// <summary>
    /// Returns a list of all active projectile gameobjects in the scene.
    /// </summary>
    /// <returns>List of projectiles.</returns>
    public static List<GameObject> GetActiveProjectiles()
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeProjectiles.RemoveAll(unit => unit == null);

        return activeProjectiles;
    }

    /// <summary>
    /// Returns a list of all active projectile gameobjects belonging to a given team in the scene.
    /// </summary>
    /// <param name="teamID">A team ID.</param>
    /// <param name="onTeam">True if you want all the projectiles on the given teamID, false if you want all the projectiles not on the given teamID.</param>
    /// <returns>List of projectiles.</returns>
    public static List<GameObject> GetActiveProjectiles(int teamID, bool onTeam)
    {
        if (!hasInitialized)
            Initialize();

        //Remove any null objects from the list.
        activeProjectiles.RemoveAll(unit => unit == null);

        //Create a list of projectiles from activeProjectiles.
        List<GameObject> projectiles = new List<GameObject>();
        foreach (GameObject go in activeProjectiles)
        {
            if (go.GetComponent<ProjectileInfo>() != null) {
                if (onTeam && go.GetComponent<ProjectileInfo>().TeamID == teamID)
                    projectiles.Add(go);
                else if (!onTeam && go.GetComponent<ProjectileInfo>().TeamID != teamID)
                    projectiles.Add(go);
            }
        }

        return projectiles;
    }

    /// <summary>
    /// Remove a projectile gameObject from the list of active projectiles.
    /// </summary>
    /// <param name="projectile">Reference to the projectile gameobject to remove from the list.</param>
    public static void RemoveProjectile(GameObject projectile)
    {
        if (!hasInitialized)
            return;

        activeProjectiles.Remove(projectile);
    }
}
