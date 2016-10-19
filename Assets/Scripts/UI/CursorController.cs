using UnityEngine;
using System.Collections;

/// <summary>
/// Handles locking and visibility of player's cursor.
/// </summary>
public class CursorController : MonoBehaviour
{
    static Texture2D reticule;
    static float cursorScale = 0.5f;
    static bool showReticule = false;

    static bool hasInitialized = false;

    static void Initialize()
    {
        if (!hasInitialized)
        {
            reticule = Resources.Load("Sprites/crosshair", typeof(Texture2D)) as Texture2D;
            hasInitialized = true;
        }
    }

    public static void HideAll()
    {
        Debug.Log("HideAll");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        showReticule = false;
    }

    public static void SetCursorAccordingToControls()
    {
        if (PauseController.IsPaused())
            return;

        if (UserSettings.ControlType == ControlType.Legacy || UserSettings.ControlType == ControlType.MouseAim)
            HideAll();
        else
            ShowReticule();
    }

    public static void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        showReticule = false;
    }

    public static void ShowReticule()
    {
        Debug.Log("ShowReticule");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        showReticule = true;
    }


    public static void DrawReticule()
    {
        if (!hasInitialized)
            Initialize();

        if (!showReticule)
            return;

        // If not paused, removes crosshair on pause
        if (Time.timeScale != 0)
        {
            float screenWidth = (Screen.width / 2) - ((reticule.width * cursorScale) / 2);
            float screenHeight = (Screen.height / 2) - ((reticule.height * cursorScale) / 2);
            float realX = Event.current.mousePosition.x;
            float realY = Event.current.mousePosition.y;


            //float hypotenuse = (float)Math.Sqrt(Math.Pow(Math.Abs((Screen.width / 2) - realX), 2) + Math.Pow((Screen.height / 2) - realY, 2));
            //if (hypotenuse > mousePosSensitivity) {
            //    float angle = Mathf.Atan2(Math.Abs((Screen.height / 2) - realY), Math.Abs((Screen.width / 2) - realX));
            //    realX = (Screen.width / 2) + ((Mathf.Cos(angle) * mousePosSensitivity) * (realX > Screen.width / 2 ? 1 : -1));
            //    realY = (Screen.height / 2) + ((Mathf.Sin(angle) * mousePosSensitivity) * (realY > Screen.height / 2 ? 1 : -1));
            //}

            float xPos = realX - ((reticule.width * cursorScale) / 2);
            float yPos = realY - ((reticule.height * cursorScale) / 2);

            if (reticule != null)
                GUI.DrawTexture(new Rect(xPos, yPos, reticule.width * cursorScale, reticule.height * cursorScale), reticule);
            else
                Debug.Log("No crosshair texture found");
        }
    }
}
