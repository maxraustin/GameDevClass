using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class Health : MonoBehaviour
{
    UnitInfo myInfo;
    int currentHealth;
    int currentShields;

    // Use this for initialization
    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        currentHealth = myInfo.MaxHealth;
    }

    public void AddLife(int life)
    {
        currentHealth += life;

        if (currentHealth > myInfo.MaxHealth)
            currentHealth = myInfo.MaxHealth;

        //AdjustHPDisplay();
    }

    void Die()
    {
        if (myInfo.Explosion != null)
            Instantiate(myInfo.Explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth < 0)
            currentHealth = 0;

        //AdjustHPDisplay();

        if (currentHealth == 0)
            Die();
    }
}
