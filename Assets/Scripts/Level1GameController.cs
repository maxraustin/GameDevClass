using UnityEngine;
using System.Collections;

public class Level1GameController : MonoBehaviour {

    [SerializeField]
    GameObject startBoundary;

    [SerializeField]
    GameObject spawn1;

    [SerializeField]
    GameObject spawn2;

    [SerializeField]
    GameObject spawn3;

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

        switch (currentProgressionPoint)
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
            HUDController.Instance.DisplayMessage("Kill all units before they kill you");
            AdvanceLevelProgression();
        }
        else
        {
            HUDController.Instance.DisplayMessage("Thanks for returning");
        }
    }

    void PlayerLeftBoundary(GameObject boundary)
    {
        if (boundary == startBoundary)
        {
            HUDController.Instance.DisplayMessage("Return to mission area or your ship go boom boom");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnShips1()
    {
        for (int i = 0; i < 5; i++)
        {
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 1, spawn1);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 1, spawn2);
            UnitSpawner.SpawnUnitsInArea(UnitReferences.EnemyFighter1, 1, spawn3);
            yield return new WaitForSeconds(15);
        }
        AdvanceLevelProgression();
    }
}
