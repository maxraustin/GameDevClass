using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Handles saving settings to files on disk.
/// </summary>
public class SaveController : MonoBehaviour {

    static string filePath = Application.persistentDataPath + "/user.dat";
    public static string FilePath { get { return filePath; } }

    /// <summary>
    /// Serializes a SaveInfo object and writes it to a file.
    /// </summary>
    /// <param name="saveInfo">SaveInfo object to serialize.</param>
    private static void WriteToFile(SaveInfo saveInfo)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            return;

        if (!saveInfo.valid)
            throw new IOException("Given SaveInfo was not valid. Make sure you are using a SaveInfo returned from LoadController.LoadSavedInfo().");

        FileStream file;
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        //Create a new file, discarding any previously saved file in this path, and write the serialized saveInfo to it.
        file = File.Create(filePath);
        binaryFormatter.Serialize(file, saveInfo);
        file.Close();
    }

    /// <summary>
    /// Writes UserSettings.ControlType to file.
    /// </summary>
    public static void SaveControlType()
    {
        SaveInfo saveInfo = LoadController.LoadSavedInfo();

        saveInfo.controlType = UserSettings.ControlType;

        WriteToFile(saveInfo);
    }


    /// <summary>
    /// Writes GameSettings.CurrentLevel to file.
    /// </summary>
    public static void SaveCurrentLevel()
    {
        SaveInfo saveInfo = LoadController.LoadSavedInfo();

        saveInfo.currentLevel = GameSettings.CurrentLevel;

        WriteToFile(saveInfo);
    }
}
