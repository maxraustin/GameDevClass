using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton class that handles interaction with HUD UI display elements. Should be attached as a component on the HUD prefab.
/// </summary>
public class HUDController : MonoBehaviour {
    static HUDController instance;

    GameObject hsDisplay, messageText, objectiveText, timerText, throttleText, weaponDisplay;
    HealthAndShieldsDisplayController hsDisplayController;
    MessageTextController messageTextController;
    ObjectiveTextController objectiveTextController;
    TimerTextController timerTextController;
    ThrottleTextController throttleTextController;
    WeaponDisplayController weaponDisplayController;

    bool hasInitialized = false;

    void Awake()
    {
        instance = this;
    }

	void Start () {
        if (!hasInitialized)
            Initialize();
    }
	
    void Initialize()
    {
        if (!hasInitialized)
        {
            hsDisplay = transform.Find("HealthAndShieldsDisplay").gameObject;
            messageText = transform.Find("MessageText").gameObject;
            objectiveText = transform.Find("ObjectiveText").gameObject;
            timerText = transform.Find("TimerText").gameObject;
            throttleText = transform.Find("ThrottleText").gameObject;
            weaponDisplay = transform.Find("WeaponDisplay").gameObject;

            if (hsDisplay != null)
                hsDisplayController = hsDisplay.GetComponent<HealthAndShieldsDisplayController>();
            if (messageText != null)
                messageTextController = messageText.GetComponent<MessageTextController>();
            if (objectiveText != null)
                objectiveTextController = objectiveText.GetComponent<ObjectiveTextController>();
            if (timerText != null)
                timerTextController = timerText.GetComponent<TimerTextController>();
            if (throttleText != null)
                throttleTextController = throttleText.GetComponent<ThrottleTextController>();
            if (weaponDisplay != null)
                weaponDisplayController = weaponDisplay.GetComponent<WeaponDisplayController>();

            hasInitialized = true;
        }
    }

    /// <summary>
    /// Property to get the current HUDController instance.
    /// </summary>
    public static HUDController Instance { get { return instance; } }

    public void DisplayMessage(string message)
    {
        if (!hasInitialized)
            Initialize();

        if (messageTextController != null)
            messageTextController.DisplayMessage(message);
    }

    public void DisplayMessage(string message, float timeToDisplay)
    {
        if (!hasInitialized)
            Initialize();

        if (messageTextController != null)
            messageTextController.DisplayMessage(message, timeToDisplay);
    }

    public void SetHealth(int currentHP, int maxHP)
    {
        if (!hasInitialized)
            Initialize();

        if (hsDisplayController != null)
            hsDisplayController.SetHealth(currentHP, maxHP);
    }

    public void SetObjectiveCount(int count)
    {
        if (!hasInitialized)
            Initialize();

        if (objectiveTextController != null)
            objectiveTextController.UpdateObjectiveCount(count);
    }

    public void SetObjectiveText(string text)
    {
        if (!hasInitialized)
            Initialize();

        if (objectiveTextController != null)
            objectiveTextController.SetObjectiveText(text);
    }

    public void SetObjectiveTextType(ObjectiveTextType type)
    {
        if (!hasInitialized)
            Initialize();

        if (objectiveTextController != null)
            objectiveTextController.ObjectiveType = type;
    }

    public void SetShields(int currentShields, int maxShields)
    {
        if (!hasInitialized)
            Initialize();

        if (hsDisplayController != null)
            hsDisplayController.SetShields(currentShields, maxShields);
    }

    public void SetTimerTextTime(float time)
    {
        if (!hasInitialized)
            Initialize();

        if (timerTextController != null)
            timerTextController.UpdateTimeElapsed(time);
    }

    public void SetTimerTextType(TimerTextType type)
    {
        if (!hasInitialized)
            Initialize();

        if (timerTextController != null)
            timerTextController.TimerType = type;
    }

    public void SetThrottleTextPercentage(int throttlePercentage)
    {
        if (!hasInitialized)
            Initialize();

        if (throttleTextController != null)
            throttleTextController.UpdateThrottleRate(throttlePercentage);
    }

    public void SetWeaponsCount(int weaponNum, int count)
    {
        if (!hasInitialized)
            Initialize();

        if (weaponDisplayController != null)
        {
            if (weaponNum == 1)
                weaponDisplayController.SetPrimaryWeaponCount(count);
            else if (weaponNum == 2)
                weaponDisplayController.SetSecondaryWeaponCount(count);
            else if (weaponNum == 3)
                weaponDisplayController.SetTertiaryWeaponCount(count);
        }
    }

    public void SetWeaponsCount(int weaponNum, bool isInfinite)
    {
        if (!hasInitialized)
            Initialize();

        if (weaponDisplayController != null)
        {
            if (weaponNum == 1)
                weaponDisplayController.SetPrimaryWeaponCount(isInfinite);
            else if (weaponNum == 2)
                weaponDisplayController.SetSecondaryWeaponCount(isInfinite);
            else if (weaponNum == 3)
                weaponDisplayController.SetTertiaryWeaponCount(isInfinite);
        }
    }

    public void SetWeaponsEnabled(int weaponNum, bool isEnabled)
    {
        if (!hasInitialized)
            Initialize();

        if (weaponDisplayController != null)
        {
            if (weaponNum == 1)
                weaponDisplayController.SetPrimaryWeaponEnabled(isEnabled);
            else if (weaponNum == 2)
                weaponDisplayController.SetSecondaryWeaponEnabled(isEnabled);
            else if (weaponNum == 3)
                weaponDisplayController.SetTertiaryWeaponEnabled(isEnabled);
        }
    }

    public void ShowHealthAndShieldsDisplay(bool show)
    {
        if (!hasInitialized)
            Initialize();

        if (hsDisplay != null)
            hsDisplay.SetActive(show);
    }

    public void ShowObjectiveText(bool show)
    {
        if (!hasInitialized)
            Initialize();

        if (objectiveText != null)
            objectiveText.SetActive(show);
    }

    public void ShowTimerText(bool show)
    {
        if (!hasInitialized)
            Initialize();

        if (timerText != null)
            timerText.SetActive(show);
    }

    public void ShowThrottleText(bool show)
    {
        if (!hasInitialized)
            Initialize();

        if (throttleText != null)
            throttleText.SetActive(show);
    }
}
