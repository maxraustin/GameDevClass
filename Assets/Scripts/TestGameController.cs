using UnityEngine;
using System.Collections;

public class TestGameController : MonoBehaviour
{
    [SerializeField]
    GameObject startBoundary;

    int currentProgressionPoint = 0;

    // Use this for initialization
    void Start()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }

    void OnEnable()
    {
        Health.OnPlayerDeath += Lose;
        Health.OnUnitDeath += CheckForWin;
        BoundaryController.OnPlayerEnterBoundary += PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary += PlayerLeftBoundary;
    }

    void OnDisable()
    {
        Health.OnPlayerDeath -= Lose;
        Health.OnUnitDeath -= CheckForWin;
        BoundaryController.OnPlayerEnterBoundary -= PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary -= PlayerLeftBoundary;
    }

    // Update is called once per frame
    void Update()
    {
        HUDController.Instance.SetObjectiveCount(UnitTracker.GetActiveEnemyCount());
    }

    void AdvanceLevelProgression()
    {
        currentProgressionPoint++;

        switch(currentProgressionPoint)
        {
            case 1:
                StartCoroutine(SpawnShips1());
                break;
            case 2:
                CheckForWin();
                break;
            default:
                Debug.Log("Advanced to a progression point beyond my scope.");
                break;
        }

        
    }

    void CheckForWin()
    {
        if (UnitTracker.GetActiveEnemyCount() == 0 && UnitTracker.PlayerShip != null && TimerController.Instance.GetTime() > 0 && currentProgressionPoint == 2)
        {
            HUDController.Instance.DisplayMessage("You win.");
        }
    }

    void Lose()
    {
        HUDController.Instance.DisplayMessage("You lose.");
        //StartCoroutine(Restart());
    }

    void PlayerEnteredBoundary(GameObject boundary)
    {
        if (currentProgressionPoint == 0 && boundary == startBoundary)
        {
            Debug.Log("Player entered start boundary.");
            AdvanceLevelProgression();
        }
    }

    void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == startBoundary)
        {
            Debug.Log("Player left start boundary.");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnShips1()
    {
        for (int i = 0; i < 6; i++) { 
            for (int j = 0; j < 4; j++)
            {
                Vector3 spawnLocation = new Vector3(Random.Range(-75, 75), Random.Range(50, 150), 200);
                UnitSpawner.SpawnUnit(UnitReferences.EnemyFighter1, spawnLocation);
            }
            yield return new WaitForSeconds(10);
        }
        AdvanceLevelProgression();
    }
}
