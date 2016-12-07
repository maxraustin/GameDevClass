using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton class that handles behaviours for scene starup and ending the scene. Should be attached as a component to the "GameController" GameObject.
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController instance;

    [SerializeField]
    BaseLevelController currentLevelController;

    bool playerDefeated = false;
    bool playerVictorious = false;

    public static GameController Instance { get { return instance; } }

    // Use this for initialization
    void Awake()
    {
        instance = this;

        UnitTracker.Clear();
        ProjectileTracker.Clear();

        UnitSpawner.SpawnUnit(GameSettings.CurrentPlayerShip, currentLevelController.CurrentPlayerSpawn.transform.position, RotationCalculator.RotationTowardLocation(currentLevelController.CurrentPlayerSpawn.transform.position, currentLevelController.PositionPlayerLooksAtSpawn));
    }

    void Start()
    {
        PauseController.UnpauseAll();

        CursorController.SetCursorAccordingToControls();
    }


    /// <summary>
    /// The player has been defeated. Reload the current scene.
    /// </summary>
    public void PlayerDefeated()
    {
        if (playerVictorious)
            return;

        playerDefeated = true;

        HUDController.Instance.DisplayMessage("You have been defeated. Restarting level...");

        StartCoroutine(RestartLevel());
    }

    /// <summary>
    /// The player has completed the level. Increment the current level number and return to main menu.
    /// </summary>
    public void PlayerVictorious()
    {
        if (playerDefeated)
            return;

        playerVictorious = true;

        HUDController.Instance.DisplayMessage("Congratulations, you have completed the level!");

        if (currentLevelController.CurrentLevel == GameSettings.CurrentLevel)
        {
            GameSettings.CurrentLevel++;
            SaveController.SaveSetting(SIMember.CURRENT_LEVEL);
        }

        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
