using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Singleton that provides timer functionality.
/// </summary>
public class TimerController : MonoBehaviour
{
    public static TimerController current;
    float timeElapsed;
    float startTime;
    bool timerStopped = true;
    bool countingUp= false;
    //Text timerText;
    TimerTextController timerTextController;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (!timerStopped && Time.timeScale != 0)
        {
            timeElapsed += Time.deltaTime;

            if (timerTextController != null)
            {
                if (countingUp)
                    timerTextController.UpdateTimeElapsed(timeElapsed);
                else
                    timerTextController.UpdateTimeElapsed(startTime - timeElapsed);
            }
        }
    }

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
    }

    /// <summary>
    /// Sets timerText Text reference.
    /// </summary>
    /// <param name="_text"></param>
    public void SetTimerText(TimerTextController _ttc)
    {
        if (_ttc == null)
            throw new System.Exception("TimerTextController is null!");

        timerTextController = _ttc;
    }

    /// <summary>
    /// Starts countdown timer at countdownTime in seconds.
    /// </summary>
    /// <param name="countdownTime"></param>
    public void StartCountdown(float countdownTime)
    {
        startTime = countdownTime;
        countingUp = false;
        RestartTimer();
    }

    /// <summary>
    /// Starts countdown timer at countdownTime in seconds and sets timerTextController reference.
    /// </summary>
    /// <param name="countdownTime"></param>
    public void StartCountdown(float countdownTime, TimerTextController _ttc)
    {
        startTime = countdownTime;
        countingUp = false;
        SetTimerText(_ttc);
        RestartTimer();
    }

    /// <summary>
    /// Starts timer at 0s elapsed.
    /// </summary>
    public void StartStopwatch()
    {
        countingUp = true;
        RestartTimer();
    }

    /// <summary>
    /// Starts timer at 0s elapsed and sets timerTextController reference.
    /// </summary>
    public void StartStopwatch(TimerTextController _ttc)
    {
        countingUp = true;
        SetTimerText(_ttc);
        RestartTimer();
    }
}   
