using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    /*
        Data Members
    */
    static int currentLevel = DefaultSettings.GS_CURRENT_LEVEL;

    static GameObject currentPlayerShip = DefaultSettings.GS_CURRENT_PSHIP;


    /*
        Properties
    */
    public static int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    public static GameObject CurrentPlayerShip { get { return currentPlayerShip; } set { currentPlayerShip = value; } }
}
