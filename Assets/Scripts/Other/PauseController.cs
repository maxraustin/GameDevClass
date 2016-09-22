using UnityEngine;
using System.Collections;

/// <summary>
/// Handles all changing of Time.timeScale.
/// </summary>
public class PauseController : MonoBehaviour
{
    static bool pausedMenu;

    public static void MenuOpen(bool open)
    {
        pausedMenu = open;

        TogglePause();
    }

    public static bool IsPaused()
    {
        return (pausedMenu);
    }

    static void TogglePause()
    {
        if (IsPaused())
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
