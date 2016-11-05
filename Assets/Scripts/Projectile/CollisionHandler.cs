using UnityEngine;
using System.Collections;

/// <summary>
/// Handles collisions with other objects. Every unit and projectile should have this attached as a component.
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Projectile")) return;
        else if (other.tag.Equals("Immovable"))
            HitImmovableObject();
        else if (other.tag.Equals("Unit"))
            HitUnit(other.gameObject);
    }

    /// <summary>
    /// We hit an immovable object.
    /// </summary>
    void HitImmovableObject()
    {
        if (tag.Equals("Unit") && GetComponent<Health>() != null)
            GetComponent<Health>().TakeDamage(1000000);
        else if (tag.Equals("Projectile"))
            Destroy(gameObject);
    }

    /// <summary>
    /// We hit a unit.
    /// </summary>
    /// <param name="otherUnit">The unit we hit.</param>
    void HitUnit(GameObject otherUnit)
    {
        //If we are a projectile and hit the unit that created us: return.
        if (GetComponent<ProjectileInfo>() != null && GetComponent<ProjectileInfo>().Owner == otherUnit)
            return;

        //If we are a projectile and hit a unit on our team: return. (No friendly fire, may change later.)
        if (GetComponent<ProjectileInfo>() != null && otherUnit.GetComponent<UnitInfo>() != null && GetComponent<ProjectileInfo>().TeamID == otherUnit.GetComponent<UnitInfo>().TeamID)
            return;

        //Determine damage to deal to other unit.
        int damageToDeal = 0;
        if (tag.Equals("Projectile") && GetComponent<ProjectileInfo>() != null) //If we are a projectile: deal our projetile damage.
            damageToDeal = GetComponent<ProjectileInfo>().Damage;
        else if (tag.Equals("Unit") && GetComponent<UnitInfo>() != null) //If we are a unit: deal 2x our max health as damage.
            damageToDeal = GetComponent<UnitInfo>().MaxHealth + GetComponent<UnitInfo>().MaxShields;

        //Deal damage to other unit.
        if (otherUnit.GetComponent<Health>() != null)
            otherUnit.GetComponent<Health>().TakeDamage(damageToDeal);

        //If we are a projecile: destroy ourself.
        if (tag.Equals("Projectile"))
            Destroy(gameObject);
    }
}
