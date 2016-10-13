using UnityEngine;
using System.Collections;

/// <summary>
/// Manages health and shields of a unit.
/// </summary>
public class Health : MonoBehaviour
{
    public delegate void DeathAction();
    public static event DeathAction OnPlayerDeath;
    public static event DeathAction OnUnitDeath;

    UnitInfo myInfo;
    int currentHealth;
    int currentShields;
    float shieldRegenRate;
    float nextShieldRegenTime;

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        currentHealth = myInfo.MaxHealth;
        currentShields = myInfo.MaxShields;
        shieldRegenRate = myInfo.ShieldRegenRate;

        AdjustHPDisplay();
    }

    void Update()
    {
        CheckShieldRegen();
    }

    public void AddLife(int life)
    {
        currentHealth += life;

        if (currentHealth > myInfo.MaxHealth)
            currentHealth = myInfo.MaxHealth;
        else if (currentHealth < 0)
            currentHealth = 0;

        AdjustHPDisplay();
    }

    public void AddShields(int shields)
    {
        currentShields += shields;

        if (currentShields > myInfo.MaxShields)
            currentShields = myInfo.MaxShields;
        else if (currentShields < 0)
            currentShields = 0;

        AdjustHPDisplay();
    }

    void AdjustHPDisplay()
    {
        if (myInfo.IsPlayerShip && HUDController.Instance != null)
        {
            HUDController.Instance.SetHealth(currentHealth, myInfo.MaxHealth);
            HUDController.Instance.SetShields(currentShields, myInfo.MaxShields);
        }
    }

    void CheckShieldRegen()
    {
        if (shieldRegenRate != myInfo.ShieldRegenRate)
        {
            if (shieldRegenRate == 0)
                nextShieldRegenTime = Time.time + (1 / myInfo.ShieldRegenRate);
            else
                nextShieldRegenTime += (1 / myInfo.ShieldRegenRate) - (1 / shieldRegenRate);

            shieldRegenRate = myInfo.ShieldRegenRate;
        }

        if (shieldRegenRate != 0 && Time.time >= nextShieldRegenTime)
        {
            nextShieldRegenTime = Time.time + (1 / Mathf.Abs(shieldRegenRate));
            AddShields((shieldRegenRate > 0) ? 1 : -1);
        }
    }

    void Die()
    {
        if (myInfo.Explosion != null)
            Instantiate(myInfo.Explosion, transform.position, transform.rotation);

        //If this unit is not the player: Remove it from the unitTracker and raise the OnUnitDeath event.
        if (!myInfo.IsPlayerShip) {
            UnitTracker.RemoveUnit(gameObject);
            try
            {
                if (OnUnitDeath != null)
                    OnUnitDeath();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
        else //If this unit is the player: Remove its reference in the unitTracker and raise the OnPlayerDeath event.
        {
            UnitTracker.PlayerShip = null;
            try {
                if (OnPlayerDeath != null)
                    OnPlayerDeath();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message + ex.StackTrace);
            }
        }

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
