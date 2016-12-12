using UnityEngine;
using System.Collections.Generic;

public class EscortController : BaseLevelController
{
    public GameObject cruiser;

    PriorityTarget priority;

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
        if(cruiser.GetComponent<CruiserAIController>().WayPointNumber == 3)
        {
            GameController.Instance.PlayerVictorious();
        }
        if(UnitTracker.GetActiveAllyCount() == 0)
        {
            GameController.Instance.PlayerDefeated();
        }
    }

    protected override void PlayerEnteredBoundary(GameObject boundary)
    {
        if(boundary == boundaries[1])
        {
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[0]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[1]));
        }
        if (boundary == boundaries[2])
        {
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[2]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[3]));
        }
        if (boundary == boundaries[3])
        {
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[4]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[5]));
        }
        if (boundary == boundaries[4])
        {
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyCruiserLight1, 1, enemySpawns[6]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 20, enemySpawns[6]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 20, enemySpawns[7]));
        }
        if (boundary == boundaries[5])
        {
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 25, enemySpawns[8]));
            priority.addToEnemyPool(UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 25, enemySpawns[9]));
        }
    }
}