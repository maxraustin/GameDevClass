using UnityEngine;
using System.Collections.Generic;

public class ProjectilePoolController : MonoBehaviour {
    private static ProjectilePoolController instance;

    public static ProjectilePoolController Instance
    {
        get
        {
            if (instance == null)
                throw new System.Exception("No ProjectilePoolController exists. Please ensure that the ProjectileController prefab exists in the scene.");
            return instance;
        }
    }

    [SerializeField]
    bool canGrow = true;

    GameObject parentGO;
    GameObject smallLaser, largeLaser, guidedMissile;

    int smallLaserInitialPoolSize = 400;
    int largeLaserInitialPoolSize = 5;
    int guidedMissileInitialPoolSize = 5;

    public List<GameObject> smallLaserPool, largeLaserPool, guidedMissilePool;

    bool hasInitialized;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        if (!hasInitialized)
            Initialize();
	}
	
	void Initialize()
    {
        if (hasInitialized)
            return;

        hasInitialized = true;

        smallLaser = Resources.Load("Projectiles/Small Laser", typeof(GameObject)) as GameObject;
        largeLaser = Resources.Load("Projectiles/Large Laser", typeof(GameObject)) as GameObject;
        guidedMissile = Resources.Load("Projectiles/GuidedMissile", typeof(GameObject)) as GameObject;

        parentGO = transform.Find("ProjectileParent").gameObject;

        smallLaserPool = new List<GameObject>();
        for (int i = 0; i < smallLaserInitialPoolSize; i++)
        {
            GameObject obj = Instantiate(smallLaser);
            obj.SetActive(false);
            obj.transform.parent = parentGO.transform;
            smallLaserPool.Add(obj);
        }

        largeLaserPool = new List<GameObject>();
        for (int i = 0; i < largeLaserInitialPoolSize; i++)
        {
            GameObject obj = Instantiate(largeLaser);
            obj.SetActive(false);
            obj.transform.parent = parentGO.transform;
            largeLaserPool.Add(obj);
        }

        guidedMissilePool = new List<GameObject>();
        for (int i = 0; i < guidedMissileInitialPoolSize; i++)
        {
            GameObject obj = Instantiate(guidedMissile);
            obj.SetActive(false);
            obj.transform.parent = parentGO.transform;
            guidedMissilePool.Add(obj);
        }
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
        GameObject returnedGo = GetProjectile(projectile);
        if (returnedGo == null)
            return null;

        returnedGo.transform.position = pos;
        returnedGo.transform.rotation = rot;
        returnedGo.SetActive(true);

        return returnedGo;
    }

    /// <summary>
    /// Returns an available pooled projectile. You will have to activate it and set its position and rotation.
    /// </summary>
    /// <param name="projectile">The type of projectile you want to get.</param>
    /// <returns>Projectile GameObject of given type.</returns>
    public GameObject GetProjectile(GameObject projectile)
    {
        if (!hasInitialized)
            Initialize();

        if (projectile == smallLaser)
        {
            //Return the first inactive object in the pool that we can find.
            for (int i = 0; i < smallLaserPool.Count; i++)
            {
                if (!smallLaserPool[i].activeInHierarchy)
                {
                    return smallLaserPool[i];
                }
            }

            //Function still running, therefore all objects in pool must be in use. Try to grow the pool.
            if (canGrow)
            {
                GameObject obj = Instantiate(projectile);
                obj.SetActive(false);
                obj.transform.parent = parentGO.transform;
                smallLaserPool.Add(obj);
                return obj;
            }

            //Function still running, therefore there are no objects to return.
            return null;
        }
        else if (projectile == largeLaser)
        {
            //Return the first inactive object in the pool that we can find.
            for (int i = 0; i < largeLaserPool.Count; i++)
            {
                if (!largeLaserPool[i].activeInHierarchy)
                {
                    return largeLaserPool[i];
                }
            }

            //Function still running, therefore all objects in pool must be in use. Try to grow the pool.
            if (canGrow)
            {
                GameObject obj = Instantiate(projectile);
                obj.SetActive(false);
                obj.transform.parent = parentGO.transform;
                largeLaserPool.Add(obj);
                return obj;
            }

            //Function still running, therefore there are no objects to return.
            return null;
        }
        else if (projectile == guidedMissile)
        {
            //Return the first inactive object in the pool that we can find.
            for (int i = 0; i < guidedMissilePool.Count; i++)
            {
                if (!guidedMissilePool[i].activeInHierarchy)
                {
                    return guidedMissilePool[i];
                }
            }

            //Function still running, therefore all objects in pool must be in use. Try to grow the pool.
            if (canGrow)
            {
                GameObject obj = Instantiate(projectile);
                obj.SetActive(false);
                obj.transform.parent = parentGO.transform;
                guidedMissilePool.Add(obj);
                return obj;
            }

            //Function still running, therefore there are no objects to return.
            return null;
        }
        else
            return null;
    }
}
