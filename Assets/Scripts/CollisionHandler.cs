using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Immovable"))
            HitImmovableObject();
        else if (other.transform.parent == null)
            return;
        else if (other.transform.parent.tag.Equals("Projectile"))
            return;
        else if (other.transform.parent.tag.Equals("Ship"))
            HitUnit(other.transform.parent.gameObject);
        
    }

    void HitImmovableObject()
    {
        if (GetComponent<Health>() != null)
            GetComponent<Health>().TakeDamage(1000000);
    }

    void HitUnit(GameObject otherUnit)
    {
        //If we are a projectile and hit the unit that created us: return.
        if (GetComponent<ProjectileInfo>() != null && GetComponent<ProjectileInfo>().owner == otherUnit)
            return;
        
        //Determine damage to deal to other unit.
        int damageToDeal = 0;
        if (GetComponent<ProjectileInfo>() != null) //If we are a projectile: deal our projetile damage.
            damageToDeal = GetComponent<ProjectileInfo>().Damage;
        else if (GetComponent<UnitInfo>() != null) //If we are a unit: deal 2x our max health as damage.
            damageToDeal = GetComponent<UnitInfo>().MaxHealth * 2;

        //Deal damage to other unit.
        if (otherUnit.GetComponent<Health>() != null)
            otherUnit.GetComponent<Health>().TakeDamage(damageToDeal);

        //If we are a projecile: destroy ourself.
        if (tag.Equals("Projectile"))
            Destroy(gameObject);
    }
}
