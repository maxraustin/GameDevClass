using UnityEngine;
using System.Collections;

/// <summary>
/// Class that, when instantiated, can be serialized and written to files.
/// </summary>
[System.Serializable]
public class SaveInfo {
    //This object must be validated to serialize it to a file.
    public bool valid = false;
    
    //Game Settings
    public int currentLevel = DefaultSettings.GS_CURRENT_LEVEL;
    public GameObject currentPlayerShip = DefaultSettings.GS_CURRENT_PSHIP;

    //User Settings
    public ControlType controlType = DefaultSettings.US_CONTROL_TYPE;
}
