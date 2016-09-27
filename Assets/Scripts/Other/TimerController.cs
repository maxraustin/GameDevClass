using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Singleton class that provides timer functionality.
/// </summary>
public class TimerController : MonoBehaviour
{
    public delegate void TimerAction();
    public static event TimerAction OnTimerExpired;

    static TimerController instance;

    float timeElapsed;
    float startTime;
    bool timerStopped = true;
    bool countingUp = false;
    bool timerExpired = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!timerStopped && Time.timeScale != 0)
        {
            timeElapsed += Time.deltaTime;

            if (HUDController.Instance != null) HUDController.Instance.SetTimerTextTime(countingUp ? timeElapsed : startTime - timeElapsed);

            //If we are counting down and time has expired: Raise the OnTimerExpired event.
            if (!countingUp && !timerExpired && GetTime() <= 0)
            {
                timerExpired = true;
                if (OnTimerExpired != null)
                    OnTimerExpired();
            }
        }
    }

    /// <summary>
    /// Property to get the current TimerController instance.
    /// </summary>
    public static TimerController Instance { get { return instance; } }

    /// <summary>
    /// Gets timeElapsed if counting up, gets time remaining if counting down.
    /// </summary>
    /// <returns>Time in milliseconds.</returns>
    public float GetTime()
    {
        if (countingUp)
            return timeElapsed;
        else
            return startTime - timeElapsed;
    }

    /// <summary>
    /// Pauses timer.
    /// </summary>
    public void PauseTimer()
    {
        timerStopped = true;
    }

    /// <summary>
    /// Allows timer to run.
    /// </summary>
    public void ResumeTimer()
    {
        timerStopped = false;
    }

    /// <summary>
    /// Sets timeElapsed to 0 and allows timer to run.
    /// </summary>
    public void RestartTimer()
    {
        timeElapsed = 0;
        timerStopped = false;
        timerExpired = false;
    }

    /// <summary>
    /// Starts countdown timer at countdownTime in seconds.
    /// </summary>
    /// <param name="countdownTime"></param>
    public void StartCountdown(float countdownTime)
    {
        startTime = countdownTime;
        countingUp = false;
        if (HUDController.Instance != null) HUDController.Instance.SetTimerTextType(TimerTextType.REMAINING);
        RestartTimer();
    }

    /// <summary>
    /// Starts timer at 0s elapsed.
    /// </summary>
    public void StartStopwatch()
    {
        countingUp = true;
        if (HUDController.Instance != null) HUDController.Instance.SetTimerTextType(TimerTextType.ELAPSED);
        RestartTimer();
    }
}   
