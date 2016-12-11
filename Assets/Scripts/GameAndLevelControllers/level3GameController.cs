using UnityEngine;
using System.Collections;

public class level3GameController : BaseLevelController
{
    public int enemiesKilled = 0;
    public int alliesKilled = 0;
    public bool bossSpawned = false;
    // Use this for initialization
    void Start()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 5, enemySpawns[0]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawns[1]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 5, enemySpawns[2]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyCruiserLight1, 1, enemySpawns[0]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyCruiserLight1, 1, enemySpawns[1]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.AlliedFighter1, 5, allySpawns[0]);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.AlliedFighter1, 5, allySpawns[1]);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (UnitTracker.GetActiveEnemyCount() == 0)
        {
            if (bossSpawned == false)
            {
                UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyBattleship, 1, enemySpawns[3]);
                bossSpawned = true;
            }
            else
            {
                GameController.Instance.PlayerVictorious();
            }
        }
        if(UnitTracker.GetActiveEnemyCount() < 20 && enemiesKilled < 20)
        {
            int randomSpawn = Random.Range(0, 2);
            int tempEnemiesKilled = 20 - UnitTracker.GetActiveEnemyCount();
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1,tempEnemiesKilled, enemySpawns[randomSpawn]);
            enemiesKilled += tempEnemiesKilled;
        }
        if(UnitTracker.GetActiveAllyCount()< 10 && alliesKilled < 15)
        {
            int randomSpawn = Random.Range(0, 2);
            int temAllyKilled = 10 - UnitTracker.GetActiveAllyCount();
            UnitSpawner.SpawnUnitsInArea(UnitReferences.AlliedFighter1, temAllyKilled, allySpawns[randomSpawn]);
            alliesKilled += temAllyKilled;
        }
        
    }

    protected override void PlayerEnteredBoundary(GameObject boundary)
    {
        HUDController.Instance.DisplayMessage("Welcome Back to the Fight!", 2.0f);
    }


    protected override void PlayerLeftBoundary(GameObject boundary)
    {
        HUDController.Instance.DisplayMessage("Don't Leave! your team members need you!", 2.0f);
    }
}

