using UnityEngine;
using System.Collections;

public class Level1GameController : MonoBehaviour {

    [SerializeField]
    GameObject startBoundary;

    [SerializeField]
    GameObject playerSpawn;

    [SerializeField]
    GameObject enemySpawn1;

    [SerializeField]
    GameObject enemySpawn2;

    [SerializeField]
    GameObject enemySpawn3;

    [SerializeField]
    GameObject alliedSpawn1;

    int currentProgressionPoint = 0;

    void Awake()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.PlayerFighter1, 1, playerSpawn);
    }

    // Use this for initialization
    void Start()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;

        HUDController.Instance.SetObjectiveTextType(ObjectiveTextType.NEUTRALIZE_WAVES);
    }

    void OnEnable()
    {
        Health.OnPlayerDeath += Lose;
        Health.OnUnitDeath += EnemyDeath;
        BoundaryController.OnPlayerEnterBoundary += PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary += PlayerLeftBoundary;
    }

    void OnDisable()
    {
        Health.OnPlayerDeath -= Lose;
        Health.OnUnitDeath -= EnemyDeath;
        BoundaryController.OnPlayerEnterBoundary -= PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary -= PlayerLeftBoundary;
    }

    void AdvanceLevelProgression()
    {
        currentProgressionPoint++;
        HUDController.Instance.SetObjectiveCount(6 - currentProgressionPoint);
        switch (currentProgressionPoint)
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
                Victory();
                break;
            default:
                Debug.Log("Advanced to a progression point beyond my scope.");
                break;
        }


    }

    void EnemyDeath()
    {
        if (UnitTracker.GetActiveEnemyCount() == 0)
        {
            AdvanceLevelProgression();
        }
    }

    void Lose()
    {
        HUDController.Instance.DisplayMessage("You failed.");
        StartCoroutine(Restart());
    }

    void PlayerEnteredBoundary(GameObject boundary)
    {
        if (currentProgressionPoint == 0 && boundary == startBoundary)
        {
            HUDController.Instance.DisplayMessage("Objective: Destroy all enemy waves.");
            AdvanceLevelProgression();
        }
        else
        {
            HUDController.Instance.DisplayMessage("You have re-entered the mission area.", 2.0f);
        }
    }

    void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == startBoundary)
        {
            HUDController.Instance.DisplayMessage("Return to the mission area!", 2.0f);
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void SpawnAlliedShips()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.AlliedFighter1, 3, alliedSpawn1);
    }

    void SpawnEnemyShips()
    {
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawn1);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawn2);
        UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 10, enemySpawn3);
    }

    void Victory()
    {
        HUDController.Instance.DisplayMessage("Congratulations, you have completed the level!");
    }
}
