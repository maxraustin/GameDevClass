using UnityEngine;
using System.Collections.Generic;

public class PoolController : MonoBehaviour {
    private static PoolController instance; //Static reference to the current PoolController.

    /// <summary>
    /// Public property to access the current PoolController.
    /// </summary>
    public static PoolController Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("No PoolController exists. Please ensure that the ProjectileController prefab exists in the scene.");
            return instance;
        }
    }

    const int poolCount = 7; //Amount of pools to create.

    [SerializeField]
    bool canGrow = true; //Can pools grow if all objects are in use?

    GameObject[] parents; //Array of GameObject references to the parents of our pooled objects.
    GameObject[] objects; //Array of GameObject references of the types of our pooled objects.
    int[] poolSizes; //Array of initial sizes for our pools.
    List<GameObject>[] pools; //Array of GameObject lists that will serve as our object pools.

    bool hasInitialized; //Has this instance been initialized?
    bool hasPreInitialized;

    void Awake()
    {
        instance = this; //Set the static reference to this instance of the script.
    }
	
    public void SetInitialPoolSize(GameObject objectType, int initialPoolSize)
    {
        if (!hasPreInitialized) PreInitialize();
        if (hasInitialized)
            Debug.Log("Pools have already been initialized, setting initial pool size is useless.");

        int index = GetIndexForObject(objectType);
        poolSizes[index] = initialPoolSize;
    }

    void PreInitialize()
    {
        if (hasPreInitialized) return;
        hasPreInitialized = true;

        poolSizes = new int[poolCount];

        //Initialize array of GameObject references.
        objects = new GameObject[poolCount]; //Array of GameObject references of the types of our pooled objects.
        objects[0] = Resources.Load("Projectiles/Small Laser", typeof(GameObject)) as GameObject;
        objects[1] = Resources.Load("Projectiles/Large Laser", typeof(GameObject)) as GameObject;
        objects[2] = Resources.Load("Projectiles/GuidedMissile", typeof(GameObject)) as GameObject;
        objects[3] = Resources.Load("Units/Ships/EnemyFighter1", typeof(GameObject)) as GameObject;
        objects[4] = Resources.Load("uiAssets/Radar/AllyIcon", typeof(GameObject)) as GameObject;
        objects[5] = Resources.Load("uiAssets/Radar/EnemyIcon", typeof(GameObject)) as GameObject;
        objects[6] = Resources.Load("Units/LaserTurret", typeof(GameObject)) as GameObject;
    }

	public void Initialize()
    {
        if (!hasPreInitialized) PreInitialize();
        if (hasInitialized) return; //If we have already initialized: return.
        hasInitialized = true;
        
        //Initialze parents array with references to parent GameObjects
        GameObject projectileParent = transform.Find("ProjectileParent").gameObject;
        GameObject unitParent = transform.Find("UnitParent").gameObject;
        parents = new GameObject[poolCount];
        parents[0] = projectileParent;
        parents[1] = projectileParent;
        parents[2] = projectileParent;
        parents[3] = unitParent;
        parents[4] = null;
        parents[5] = null;
        parents[6] = unitParent;

        //Initialize pool array, pools, and populate pools with GameObjects.
        pools = new List<GameObject>[poolCount];
        for (int i = 0; i < poolCount; i++)
        {
            pools[i] = new List<GameObject>();
            for (int j = 0; j < poolSizes[i]; j++)
            {
                GameObject obj = Instantiate(objects[i]);
                obj.SetActive(false);
                if (parents[i] != null)
                    obj.transform.SetParent(parents[i].transform);
                pools[i].Add(obj);
            }
        }
    }

    /// <summary>
    /// Returns the index that corresponds to the given GameObject.
    /// </summary>
    /// <param name="objectType">The type of GameObject you need an index for.</param>
    /// <returns>Index of given GameObject type.</returns>
    int GetIndexForObject(GameObject objectType)
    {
        //Seach every GameObject refence to find one that matches the given GameObject and return the corresponding pool.
        for (int i = 0; i < poolCount; i++) {
            if (objectType == objects[i])
                return i;
        }

        //Method still running, no pool corresponds to the given GameObject. Throw an error.
        throw new System.Exception("Given GameObject does not have a corresponding pool.");
    }

    /// <summary>
    /// Returns an available pooled GameObject after setting its position & rotation and setting its parent to the given parent.
    /// </summary>
    /// <param name="objectType">The type of GameObject you want to get.</param>
    /// <param name="pos">Position to set GameObject.</param>
    /// <param name="rot">Rotation to set GameObject.</param>
    /// <param name="parent">GameObject which will be the parent of the spawned GameObject.</param>
    /// <returns>GameObject of given type.</returns>
    public GameObject GetObject(GameObject objectType, Vector3 pos, Quaternion rot, Transform parent)
    {
        GameObject go = GetObject(objectType, pos, rot);
        if (go != null)
            go.transform.SetParent(parent);

        return go;
    }

    /// <summary>
    /// Returns an available pooled GameObject after setting its position & rotation.
    /// </summary>
    /// <param name="objectType">The type of GameObject you want to get.</param>
    /// <param name="pos">Position to set GameObject.</param>
    /// <param name="rot">Rotation to set GameObject.</param>
    /// <returns>GameObject of given type.</returns>
    public GameObject GetObject(GameObject objectType, Vector3 pos, Quaternion rot)
    {
        GameObject returnedGo = GetObject(objectType); //Get an unused pooled object.
        if (returnedGo == null) return null;

        //Set the object's position and rotation.
        returnedGo.transform.position = pos;
        returnedGo.transform.rotation = rot;

        return returnedGo; //Return the GameObject.
    }

    /// <summary>
    /// Returns an available pooled GameObject.
    /// </summary>
    /// <param name="objectType">The type of GameObject you want to get.</param>
    /// <returns>GameObject of given type.</returns>
    public GameObject GetObject(GameObject objectType)
    {
        if (!hasInitialized) Initialize(); //If we haven't initialized: initialize.

        int index = GetIndexForObject(objectType); //Get the pool which corresponds to the given GameObject.

        //Return the first inactive object in the pool that we can find.
        for (int i = 0; i < pools[index].Count; i++)
        {
            if (!pools[index][i].activeInHierarchy)
            {
                pools[index][i].SetActive(true);
                return pools[index][i];
            }
        }

        //Function still running, therefore all objects in pool must be in use. Try to grow the pool.
        if (canGrow)
        {
            GameObject obj = Instantiate(objectType);
            obj.SetActive(true);
            if (parents[index] != null)
                obj.transform.SetParent(parents[index].transform);
            pools[index].Add(obj);
            return obj;
        }

        //Function still running, therefore there are no objects to return.
        return null;
    }

}
