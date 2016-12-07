using UnityEngine;
using System.Collections;

public class Level1Controller : BaseLevelController {
    void Start()
    {
        //Set good initial pool sizes and initialize object pools.
        PoolController.Instance.SetInitialPoolSize(ProjectileReferences.SmallLaser, 400);
        PoolController.Instance.SetInitialPoolSize(ProjectileReferences.LargeLaser, 5);
        PoolController.Instance.SetInitialPoolSize(ProjectileReferences.GuidedMissile, 5);
        PoolController.Instance.SetInitialPoolSize(UnitReferences.EnemyFighter1, 30);
        PoolController.Instance.Initialize();

        HUDController.Instance.SetObjectiveTextType(ObjectiveTextType.NEUTRALIZE_WAVES);
    }

    protected override void AdvancePhase()
    {
        base.AdvancePhase();

        HUDController.Instance.SetObjectiveCount(6 - currentPhase);
        switch (currentPhase)
        {
            case 1:
                SpawnAlliedShips();
                SpawnEnemyShips();
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                SpawnEnemyShips();
                break;
            case 6:
                GameController.Instance.PlayerVictorious();
                break;
            default:
                Debug.Log("Advanced to a progression point beyond my scope.");
                break;
        }
    }

    protected override void UnitDeath()
    {
        if (UnitTracker.GetActiveEnemyCount() == 0)
        {
            AdvancePhase();
        }
    }

    protected override void PlayerDeath()
    {
        GameController.Instance.PlayerDefeated();
    }

    protected override void PlayerEnteredBoundary(GameObject boundary)
    {
        if (currentPhase == 0 && boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("Objective: Destroy all enemy waves.");
            AdvancePhase();
        }
        else
        {
            HUDController.Instance.DisplayMessage("You have re-entered the mission area.", 2.0f);
        }
    }

    protected override void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("Return to the mission area!", 2.0f);
        }
    }

    void SpawnAlliedShips()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.AlliedFighter1, 3, allySpawns[0]);
    }

    void SpawnEnemyShips()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[0]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[1]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[2]);
    }
}
