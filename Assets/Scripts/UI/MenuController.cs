using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton class that handles display of ingame menu and behaviors of its buttons. Should be attached as a component on the Menu prefab.
/// </summary>
public class MenuController : MonoBehaviour
{
    static MenuController instance;
    GameObject optionsPanel;

    bool isMenuOpen = false;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Property to get the current MenuController instance.
    /// </summary>
    public static MenuController Instance
    {
        get
        {
            if (instance == null)
            {
                if (UIElementsTracker.Instance == null)
                    Debug.LogError("Please ensure that a UIElementsTracker is attached to the Canvas.");
                else
                {
                    instance = UIElementsTracker.Instance.Menu.GetComponent<MenuController>();

                    if (instance == null)
                        Debug.LogError("Please ensure that the Menu prefab is a first-level child of the Canvas.");
                }
            }

            return instance;
        }
    }

    void Start()
    {
        optionsPanel = transform.Find("OptionsPanel").gameObject;
    }

    /// <summary>
    /// Exits application. (Called when the exit button in the menu is clicked.)
    /// </summary>
    public void Button_Exit()
    {
        Application.Quit();
    }

    public void Button_Options()
    {
        optionsPanel.SetActive(true);
    }

    public void Button_OptionsPanel_Close()
    {
        optionsPanel.SetActive(false);
    }

    public void Button_OptionsPanel_MouseAim1()
    {
        UserSettings.ControlType = ControlType.MouseAim;
    }

    public void Button_OptionsPanel_MouseAim2()
    {
        UserSettings.ControlType = ControlType.Legacy;
    }

    public void Button_OptionsPanel_MousePos1()
    {
        UserSettings.ControlType = ControlType.MousePos;
    }

    public void Button_OptionsPanel_MousePos2()
    {
        UserSettings.ControlType = ControlType.MousePosRoll;
    }

    /// <summary>
    /// Reloads current scene. (Called when the restart button in the menu is clicked.)
    /// </summary>
    public void Button_Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads main menu scene. (Called when the Return to Menu button in the menu is clicked.)
    /// </summary>
    public void Button_ReturnToMainMenu()
    {
		Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 
    /// </summary>
    public void HideMenu()
    {
        isMenuOpen = false;
        SetMenuDisplay();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        SetMenuDisplay();
    }

    /// <summary>
    /// 
    /// </summary>
    void SetMenuDisplay()
    {
        PauseController.MenuOpen(isMenuOpen);

        if (isMenuOpen)
            CursorController.ShowCursor();
        else
            CursorController.SetCursorAccordingToControls();

        UIElementsTracker.Instance.Menu.SetActive(isMenuOpen);
    }
}
