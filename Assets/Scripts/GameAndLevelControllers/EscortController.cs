using UnityEngine;
using System.Collections.Generic;

public class EscortController : BaseLevelController
{
    public GameObject cruiser;

    PriorityTarget priority;
    public int alliedCount;
    // Use this for initialization
    void Start()
    {
        cruiser = UnitSpawner.SpawnUnit(UnitReferences.AlliedCruiserHeavy1, Vector3.zero);
        cruiser.GetComponent<CruiserAIController>().goToPoints = this.goToPoints;

        priority = GetComponent<PriorityTarget>();
        if (priority)
            priority.setEnemyPriority(cruiser);
    }

    // Update is called once per frame
    void Update()
    {
        alliedCount = UnitTracker.GetActiveAllyCount();
        if (alliedCount < 2)
        {
            GameController.Instance.PlayerDefeated();
        }
        if (cruiser.GetComponent<CruiserAIController>().WayPointNumber == 3)
        {
            GameController.Instance.PlayerVictorious();
        }
        
        
    }

    protected override void PlayerEnteredBoundary(GameObject boundary)
    {
        if(boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("We were worried you were leaving us to die", 2.0f);
        }
        if(boundary == boundaries[1])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[0]);
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[1]));
        }
        if (boundary == boundaries[2])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[2]);
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[3]));
        }
        if (boundary == boundaries[3])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[5]);
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[4]));
        }
        if (boundary == boundaries[4])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[6]);
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[7]));
        }
        if (boundary == boundaries[5])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[8]);
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[9]));
        }
    }

    protected override void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("Don't leave us!", 2.0f);
        }
    }
}