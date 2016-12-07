using UnityEngine;
using System.Collections;

/// <summary>
/// Provides static GameObject references to all unit prefabs.
/// </summary>
public class ProjectileReferences : MonoBehaviour {
    static GameObject smallLaser, largeLaser, guidedMissile;

    static bool hasInitialized = false;

	static void Initialize()
    {
        if (!hasInitialized)
        {
            smallLaser = Resources.Load("Projectiles/Small Laser", typeof(GameObject)) as GameObject;
            largeLaser = Resources.Load("Projectiles/Large Laser", typeof(GameObject)) as GameObject;
            guidedMissile = Resources.Load("Projectiles/GuidedMissile", typeof(GameObject)) as GameObject;
            hasInitialized = true;
        }
    }
    public static GameObject SmallLaser { get { if (!hasInitialized) Initialize(); return smallLaser; } }
    public static GameObject LargeLaser { get { if (!hasInitialized) Initialize(); return largeLaser; } }
    public static GameObject GuidedMissile { get { if (!hasInitialized) Initialize(); return guidedMissile; } }
}
