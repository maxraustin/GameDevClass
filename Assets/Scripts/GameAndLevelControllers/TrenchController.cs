using UnityEngine;
using System.Collections;

public class TrenchController : BaseLevelController {

	// Use this for initialization
	void Start () {
        UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 10, enemySpawns[7]);
    }
	
    protected override void PlayerEnteredBoundary(GameObject boundary)
    {
        if (boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("Fly Low!", 2.0f);
        }else if (boundary == boundaries[1])
        {
            GameController.Instance.PlayerVictorious();
        }else if (boundary == boundaries[2])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 5, enemySpawns[0]);
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret,25, enemySpawns[8]);
        }
        else if (boundary == boundaries[3])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 3, enemySpawns[1]);
        }
        else if (boundary == boundaries[4])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[2]);
        }
        else if (boundary == boundaries[5])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 8, enemySpawns[3]);
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 8, enemySpawns[10]);

        }
        else if (boundary == boundaries[6])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[4]);
        }
        else if (boundary == boundaries[7])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 8, enemySpawns[5]);
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 10, enemySpawns[13]);
        }
        else if (boundary == boundaries[8])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[6]);
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 35, enemySpawns[14]);
        }
        else if(boundary == boundaries[9])
        {
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 25, enemySpawns[9]);
        }
        else if (boundary == boundaries[10])
        {
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 50, enemySpawns[11]);
            UnitSpawner.SpawnUnitsInAreaAndFall(UnitReferences.LaserTurret, 50, enemySpawns[12]);
        }
    }


    protected override void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == boundaries[0])
        {
            HUDController.Instance.DisplayMessage("Safer altitude", 2.0f);
        }
    }
}
