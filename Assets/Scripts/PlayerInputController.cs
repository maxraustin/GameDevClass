using UnityEngine;
using System.Collections;

//Gets all user input, excluding input captured by PlayerController, and calls necessary methods.
public class PlayerInputController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            MenuController.ToggleMenu();
    }
}
