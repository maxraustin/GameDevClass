using UnityEngine;
using System.Collections;

public class EscortController : BaseLevelController
{
    public GameObject cruiser;
    // Use this for initialization
    void Start()
    {
        cruiser = UnitSpawner.SpawnUnit(UnitReferences.AlliedCruiserHeavy1, Vector3.zero);
        cruiser.GetComponent<CruiserAIController>().goToPoints = this.goToPoints;
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
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[0]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[1]);
        }
        if (boundary == boundaries[2])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[2]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[3]);
        }
        if (boundary == boundaries[3])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[4]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[5]);
        }
        if (boundary == boundaries[4])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyCruiserLight1, 1, enemySpawns[6]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 20, enemySpawns[6]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 20, enemySpawns[7]);
        }
        if (boundary == boundaries[5])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 25, enemySpawns[8]);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 25, enemySpawns[9]);
        }
    }
}