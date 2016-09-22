using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controller for throttle text element.
/// </summary>
public class ThrottleTextController : MonoBehaviour
{
    public void UpdateThrottleRate(int throttleRate)
    {
        GetComponent<Text>().text = "Throttle: " + throttleRate + "%";
    }
}
