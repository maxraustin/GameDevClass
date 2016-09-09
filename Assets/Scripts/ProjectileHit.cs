using UnityEngine;
using System.Collections;

public class ProjectileHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag.Equals("Projectile"))
            return;
        else if (other.transform.parent.tag.Equals("Ship"))
            HitUnit(other.transform.parent.gameObject);
    }

    void HitUnit(GameObject otherUnit)
    {
        otherUnit.GetComponent<Health>().TakeDamage(GetComponent<ProjectileInfo>().GetDamage());

        Destroy(gameObject);
    }
}
