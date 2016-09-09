using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour
{
    public static void ShowCursor(bool show)
    {
        Cursor.visible = show;

        if (show)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
