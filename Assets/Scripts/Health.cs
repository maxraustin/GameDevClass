using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    UnitInfo myInfo;
    int currentHealth;

    // Use this for initialization
    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        currentHealth = myInfo.GetMaxHealth();
    }

    public void AddLife(int life)
    {
        currentHealth += life;

        if (currentHealth > myInfo.GetMaxHealth())
            currentHealth = myInfo.GetMaxHealth();

        //AdjustHPDisplay();
    }

    void Die()
    {
        if (myInfo.GetExplosion() != null)
            Instantiate(myInfo.GetExplosion(), transform.position, transform.rotation);

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
