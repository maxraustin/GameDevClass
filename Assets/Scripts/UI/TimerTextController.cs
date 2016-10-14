using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controller for timer UI display.
/// </summary>
public class TimerTextController : MonoBehaviour
{
    public TimerTextType TimerType { get; set; }

    public void UpdateTimeElapsed(float time)
    {
        System.Text.StringBuilder timeString = new System.Text.StringBuilder();
        timeString.Append((TimerType == TimerTextType.ELAPSED) ? "Elapsed: " : "Remaining: ");

        if (time < 0)
            timeString.Append("-");

        time = Mathf.Abs(time);

        int minutes = (int)time / 60;
        float seconds = time % 60;

        timeString.Append(string.Format("{0:00}:{1:00.00}", minutes, seconds));

        GetComponent<Text>().text = timeString.ToString();
    }
}
