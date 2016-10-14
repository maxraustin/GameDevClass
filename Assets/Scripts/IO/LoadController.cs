using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Handles loading settings from files on disk.
/// </summary>
public class LoadController : MonoBehaviour {
    /// <summary>
    /// Returns a SaveInfo object from the current save file.
    /// </summary>
    /// <returns>SaveInfo with previously saved information.</returns>
    public static SaveInfo LoadSavedInfo()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            return new SaveInfo();

        FileStream file;
        SaveInfo saveInfo = new SaveInfo();
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(SaveController.FilePath)) //If the file exists: Open file and get SaveInfo data from file.
        {
            try {
                file = File.Open(SaveController.FilePath, FileMode.Open);
                saveInfo = (SaveInfo)binaryFormatter.Deserialize(file);
                file.Close();
            }
            catch (System.Exception e)
            {
                Debug.Log("Exception when trying to load file. Message: " + e.Message);
                //Make a backup of the file.
            }
        }
        else //If the file doesn't exist: Validate the saveInfo.
            saveInfo.valid = true;

        return saveInfo;
    }

    /// <summary>
    /// Sets Game and User settings from info in file.
    /// </summary>
    public static void LoadAllSettings()
    {
        SaveInfo saveInfo = LoadSavedInfo();

        //GameSettings info
        GameSettings.CurrentLevel = saveInfo.currentLevel;

        //UserSettings info
        UserSettings.ControlType = saveInfo.controlType;
    }
}
