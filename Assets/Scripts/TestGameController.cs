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

    // Update is called once per frame
    void Update()
    {
        HUDController.Instance.SetObjectiveCount(UnitTracker.GetActiveEnemyCount());
        if (UnitTracker.GetActiveEnemyCount() == 0 && UnitTracker.playerShip != null && TimerController.Instance.GetTime() > 0)
        {
            winText.SetActive(true);
        }

        if (UnitTracker.playerShip == null)
        {
            loseText.SetActive(true);
            StartCoroutine(Restart());
        }

        if (TimerController.Instance.GetTime() <= 0)
        {
            loseText.SetActive(true);
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
