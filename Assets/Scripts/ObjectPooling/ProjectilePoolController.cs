using UnityEngine;
using System.Collections.Generic;

public class ProjectilePoolController : MonoBehaviour {
    private static ProjectilePoolController instance; //Static reference to the current ProjectilePoolController.

    /// <summary>
    /// Public property to access the current ProjectilePoolController.
    /// </summary>
    public static ProjectilePoolController Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("No ProjectilePoolController exists. Please ensure that the ProjectileController prefab exists in the scene.");
            return instance;
        }
    }

    const int poolCount = 3; //Amount of pools to create.

    [SerializeField]
    bool canGrow = true; //Can pools grow if all objects are in use?

    GameObject parentGO; //GameObject that will be the parent to all our pooled objects.

    GameObject[] projectiles; //Array of GameObject references.
    int[] poolSizes; //Array of initial sizes for our pools.
    List<GameObject>[] pools; //Array of GameObject lists that will serve as our object pools.

    bool hasInitialized; //Has this instance been initialized?

    void Awake()
    {
        instance = this; //Set the static reference to this instance of the script.
    }

	// Use this for initialization
	void Start () {
        if (!hasInitialized)
            Initialize();
	}
	
	void Initialize()
    {
        if (hasInitialized) return; //If we have already initialized: return.

        hasInitialized = true;

        parentGO = transform.Find("ProjectileParent").gameObject; //Set parent GameObject.

        poolSizes = new int[poolCount] { 400, 5, 5 }; //Set array of pool sizes.

        //Initialize array of GameObject references.
        projectiles = new GameObject[poolCount];
        projectiles[0] = Resources.Load("Projectiles/Small Laser", typeof(GameObject)) as GameObject;
        projectiles[1] = Resources.Load("Projectiles/Large Laser", typeof(GameObject)) as GameObject;
        projectiles[2] = Resources.Load("Projectiles/GuidedMissile", typeof(GameObject)) as GameObject;

        //Initialize pool array, pools, and populate pools with GameObjects.
        pools = new List<GameObject>[poolCount];
        for (int i = 0; i < poolCount; i++)
        {
            pools[i] = new List<GameObject>();
            for (int j = 0; j < poolSizes[i]; j++)
            {
                GameObject obj = Instantiate(projectiles[i]);
                obj.SetActive(false);
                obj.transform.parent = parentGO.transform;
                pools[i].Add(obj);
            }
        }
    }

    /// <summary>
    /// Returns the pool that corresponds to the given projectile.
    /// </summary>
    /// <param name="projectile">The type of projectile used in the pool you want to get.</param>
    /// <returns>Object Pool</returns>
    List<GameObject> GetPoolForProjectile(GameObject projectile)
    {
        //Seach every GameObject refence to find one that matches the given GameObject and return the corresponding pool.
        for (int i = 0; i < poolCount; i++) {
            if (projectile == projectiles[i])
                return pools[i];
        }

        //Method still running, no pool corresponds to the given GameObject. Throw an error.
        throw new System.Exception("Given GameObject does not have a corresponding projectile pool.");
    }

    /// <summary>
    /// Returns an available pooled projectile after setting its position & rotation and activating it.
    /// </summary>
    /// <param name="projectile">The type of projectile you want to get.</param>
    /// <param name="pos">Position to set projectile.</param>
    /// <param name="rot">Rotation to set projectile.</param>
    /// <returns>Projectile GameObject of given type.</returns>
    public GameObject GetProjectile(GameObject projectile, Vector3 pos, Quaternion rot)
    {
        GameObject returnedGo = GetProjectile(projectile); //Get an unused pooled object.
        if (returnedGo == null) return null;

        //Set the objects position, rotation, and set it active.
        returnedGo.transform.position = pos;
        returnedGo.transform.rotation = rot;
        returnedGo.SetActive(true);

        return returnedGo; //Return the GameObject.
    }

    /// <summary>
    /// Returns an available pooled projectile. It must be manually activated and have its position and rotation set.
    /// </summary>
    /// <param name="projectile">The type of projectile you want to get.</param>
    /// <returns>Projectile GameObject of given type.</returns>
    public GameObject GetProjectile(GameObject projectile)
    {
        if (!hasInitialized) Initialize(); //If we haven't initialized: initialize.

        List<GameObject> pool = GetPoolForProjectile(projectile); //Get the pool which corresponds to the given GameObject.

        //Return the first inactive object in the pool that we can find.
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
                return pool[i];
        }

        //Function still running, therefore all objects in pool must be in use. Try to grow the pool.
        if (canGrow)
        {
            GameObject obj = Instantiate(projectile);
            obj.SetActive(false);
            obj.transform.parent = parentGO.transform;
            pool.Add(obj);
            return obj;
        }

        //Function still running, therefore there are no objects to return.
        return null;
    }

}
