using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Keeps references of ingame UI elements.
/// </summary>
public class UIElementsTracker : MonoBehaviour {
    static UIElementsTracker current;

    GameObject menu, timerText, throttleText, objectiveText;

    bool hasInitialized = false;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        if (!hasInitialized)
            Initialize();
    }

    void Initialize()
    {
        if (!hasInitialized)
        {
            menu = transform.Find("Menu").gameObject;
            timerText = transform.Find("TimerText").gameObject;
            throttleText = transform.Find("ThrottleText").gameObject;
            objectiveText = transform.Find("ObjectiveText").gameObject;
            hasInitialized = true;
        }
    }

    /// <summary>
    /// Property to get current static reference.
    /// </summary>
    public static UIElementsTracker Current { get { return current; } }

    public GameObject GetMenu()
    {
        if (!hasInitialized)
            Initialize();

        return menu;
    }

    public TimerTextController GetTimerTextController()
    {
        if (!hasInitialized)
            Initialize();

        return timerText.GetComponent<TimerTextController>();
    }

    public ThrottleTextController GetThrottleTextController()
    {
        if (!hasInitialized)
            Initialize();

        return throttleText.GetComponent<ThrottleTextController>();
    }

    public ObjectiveTextController GetObjectiveTextController()
    {
        if (!hasInitialized)
            Initialize();

        return objectiveText.GetComponent<ObjectiveTextController>();
    }
}
