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
    /// Takes a SaveInfoMember. Saves corresponding Game/User setting to file.
    /// </summary>
    /// <param name="members">The type of settings to save.</param>
    public static void SaveSetting(SIMember member)
    {
        SaveInfo saveInfo = LoadController.LoadSavedInfo();

        if (member == SIMember.CONTROL_TYPE)
            saveInfo.controlType = UserSettings.ControlType;
        else if (member == SIMember.CURRENT_LEVEL)
            saveInfo.currentLevel = GameSettings.CurrentLevel;
        else if (member == SIMember.CURRENT_PLAYER_SHIP)
            saveInfo.currentPlayerShip = GameSettings.CurrentPlayerShip;        

        WriteToFile(saveInfo);
    }

    /// <summary>
    /// Takes an array of SaveInfoMembers. Saves all corresponding Game and User settings to file.
    /// </summary>
    /// <param name="members">The type of settings to save.</param>
    public static void SaveSettings(SIMember[] members)
    {
        SaveInfo saveInfo = LoadController.LoadSavedInfo();

        foreach (SIMember member in members)
        {
            if (member == SIMember.CONTROL_TYPE)
                saveInfo.controlType = UserSettings.ControlType;
            else if (member == SIMember.CURRENT_LEVEL)
                saveInfo.currentLevel = GameSettings.CurrentLevel;
            else if (member == SIMember.CURRENT_PLAYER_SHIP)
                saveInfo.currentPlayerShip = GameSettings.CurrentPlayerShip;
        }

        WriteToFile(saveInfo);
    }
}
