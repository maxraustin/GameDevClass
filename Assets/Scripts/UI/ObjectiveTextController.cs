using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ObjectiveTextType { NEUTRALIZE_SHIPS = 0, NEUTRALIZE_WAVES = 1 };
/// <summary>
/// Controller for objective UI display.
/// </summary>
public class ObjectiveTextController : MonoBehaviour
{
    public ObjectiveTextType ObjectiveType { get; set; }

    public void UpdateObjectiveCount(int count)
    {
        string objectiveString = string.Empty;

        if (ObjectiveType == ObjectiveTextType.NEUTRALIZE_SHIPS)
            objectiveString = "Enemies Remaining: " + count;
        else if (ObjectiveType == ObjectiveTextType.NEUTRALIZE_WAVES)
            objectiveString = "Waves Remaining: " + count;

        GetComponent<Text>().text = objectiveString;
    }

    public void SetObjectiveText(string text)
    {
        GetComponent<Text>().text = text;
    }

    
}
