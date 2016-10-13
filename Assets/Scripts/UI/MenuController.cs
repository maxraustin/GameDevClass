using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton class that handles display of ingame menu and behaviors of its buttons. Should be attached as a component on the Menu prefab.
/// </summary>
public class MenuController : MonoBehaviour
{
    static MenuController instance;

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

    /// <summary>
    /// Exits application. (Called when the exit button in the menu is clicked.)
    /// </summary>
    public void Button_Exit()
    {
        Application.Quit();
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

        CursorController.ShowCursor(isMenuOpen);

        UIElementsTracker.Instance.Menu.SetActive(isMenuOpen);
    }
}
