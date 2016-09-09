using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    static bool isMenuOpen = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        PauseController.MenuOpen(isMenuOpen);

        CursorController.ShowCursor(isMenuOpen);
    }
}
