using UnityEngine;
using System.Collections;

/// <summary>
/// Base class that is parent to all individual level controllers.
/// </summary>
public abstract class BaseLevelController : MonoBehaviour
{
    [SerializeField]
    protected int currentLevel = 0;

    [SerializeField]
    protected GameObject[] boundaries;

    [SerializeField]
    protected GameObject[] playerSpawns;

    [SerializeField]
    protected GameObject[] enemySpawns;

    [SerializeField]
    protected GameObject[] allySpawns;

    [SerializeField]
    protected Vector3 positionPlayerLooksAtSpawn;

    protected int currentPhase = 0;

    /// <summary>
    /// The index in playerSpawns of the GameObject to use to spawn the player.
    /// </summary>
    protected int currentPlayerSpawnIndex = 0;

    protected virtual void OnEnable()
    {
        Health.OnPlayerDeath += PlayerDeath;
        Health.OnUnitDeath += UnitDeath;
        BoundaryController.OnPlayerEnterBoundary += PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary += PlayerLeftBoundary;
    }

    protected virtual void OnDisable()
    {
        Health.OnPlayerDeath -= PlayerDeath;
        Health.OnUnitDeath -= UnitDeath;
        BoundaryController.OnPlayerEnterBoundary -= PlayerEnteredBoundary;
        BoundaryController.OnPlayerExitBoundary -= PlayerLeftBoundary;
    }

    protected virtual void AdvancePhase()
    {
        currentPhase++;
    }

    public int CurrentLevel { get { return currentLevel; } }

    public GameObject CurrentPlayerSpawn { get { return playerSpawns[currentPlayerSpawnIndex]; } }

    protected virtual void PlayerDeath() { }

    protected virtual void PlayerEnteredBoundary(GameObject boundary) { }

    protected virtual void PlayerLeftBoundary(GameObject boundary) { }

    public Vector3 PositionPlayerLooksAtSpawn { get { return positionPlayerLooksAtSpawn; } }

    protected virtual void UnitDeath() { }
}
