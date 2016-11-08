using UnityEngine;
using System.Collections;

public class TrenchController : BaseLevelController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 3, enemySpawns[0]);
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
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 4, enemySpawns[3]);
        }
        else if (boundary == boundaries[6])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[4]);
        }
        else if (boundary == boundaries[7])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 8, enemySpawns[5]);
        }
        else if (boundary == boundaries[8])
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 15, enemySpawns[6]);
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
