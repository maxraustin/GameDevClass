using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour
{
    void Start()
    {
        ProjectileInfo pi = GetComponent<ProjectileInfo>();
        if (pi != null)
            GetComponent<Rigidbody>().velocity = transform.forward * pi.GetSpeed();
    }
}
