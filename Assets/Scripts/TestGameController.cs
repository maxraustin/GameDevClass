using UnityEngine;
using System.Collections;

public class TestGameController : MonoBehaviour
{
    [SerializeField]
    GameObject winText;

    [SerializeField]
    GameObject loseText;

    // Use this for initialization
    void Start()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        TimerController.Instance.StartCountdown(60);
    }

    void OnEnable()
    {
        Health.OnPlayerDeath += Lose;
        Health.OnUnitDeath += CheckForWin;
        TimerController.OnTimerExpired += Lose;
    }

    void OnDisable()
    {
        Health.OnPlayerDeath -= Lose;
        Health.OnUnitDeath -= CheckForWin;
        TimerController.OnTimerExpired -= Lose;
    }

    // Update is called once per frame
    void Update()
    {
        HUDController.Instance.SetObjectiveCount(UnitTracker.GetActiveEnemyCount());
    }

    void CheckForWin()
    {
        Debug.Log("Checking for win");
        if (UnitTracker.GetActiveEnemyCount() == 0 && UnitTracker.PlayerShip != null && TimerController.Instance.GetTime() > 0)
        {
            winText.SetActive(true);
        }
    }

    void Lose()
    {
        Debug.Log("Lose");
        loseText.SetActive(true);
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
