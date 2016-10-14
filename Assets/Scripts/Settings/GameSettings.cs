using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{
    static int currentLevel = DefaultSettings.GS_CURRENT_LEVEL;

    public static int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
}
