using UnityEngine;
using System.Collections;

/// <summary>
/// Gets all user input, excluding input captured by PlayerController, and calls necessary methods.
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            MenuController.Current.ToggleMenu();

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerCameraController.Current.SetFreeLookActive(true);
            PlayerController.Current.SetMouseControlsEnabled(false);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerCameraController.Current.SetFreeLookActive(false);
            PlayerController.Current.SetMouseControlsEnabled(true);
        }
    }
}
