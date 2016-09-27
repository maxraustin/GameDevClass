using UnityEngine;
using System.Collections;

/// <summary>
/// Manages health and shields of a unit.
/// </summary>
public class Health : MonoBehaviour
{
    UnitInfo myInfo;
    int currentHealth;
    int currentShields;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        currentHealth = myInfo.MaxHealth;
        currentShields = myInfo.MaxShields;

        AdjustHPDisplay();
    }

    public void AddLife(int life)
    {
        currentHealth += life;

        if (currentHealth > myInfo.MaxHealth)
            currentHealth = myInfo.MaxHealth;

        AdjustHPDisplay();
    }

    public void AddShields(int shields)
    {
        currentShields += shields;

        if (currentShields > myInfo.MaxShields)
            currentShields = myInfo.MaxShields;

        AdjustHPDisplay();
    }

    void AdjustHPDisplay()
    {
        if (myInfo.IsPlayerShip)
        {
            if (HUDController.Instance != null) HUDController.Instance.SetHealth(currentHealth, myInfo.MaxHealth);
            if (HUDController.Instance != null) HUDController.Instance.SetShields(currentShields, myInfo.MaxShields);
        }
    }

    void Die()
    {
        if (myInfo.Explosion != null)
            Instantiate(myInfo.Explosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        if (currentShields >= dmg)
        {
            currentShields -= dmg;
            AdjustHPDisplay();
        }
        else
        {
            dmg -= currentShields;
            currentShields = 0;
            currentHealth -= dmg;

            if (currentHealth < 0)
                currentHealth = 0;

            AdjustHPDisplay();

            if (currentHealth == 0)
                Die();
        }
    }
}
