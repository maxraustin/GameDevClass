using UnityEngine;
using System.Collections;

/// <summary>
/// Handles display of ingame menu and behaviors of its buttons.
/// </summary>
public class MenuController : MonoBehaviour
{
    static MenuController current;
    bool isMenuOpen = false;
    GameObject menu;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        menu = UIElementsTracker.Current.GetMenu();
    }

    /// <summary>
    /// Property to get current static reference.
    /// </summary>
    public static MenuController Current { get { return current; } }

    /// <summary>
    /// Called when the exit button in the menu is clicked.
    /// </summary>
    public void Button_Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Called when the restart button in the menu is clicked.
    /// </summary>
    public void Button_Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("test");
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

        menu.SetActive(isMenuOpen);
    }
}
