using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ObjectiveTextType { NEUTRALIZE };
/// <summary>
/// Controller for objective UI display.
/// </summary>
public class ObjectiveTextController : MonoBehaviour
{
    public ObjectiveTextType ObjectiveType { get; set; }

    public void UpdateObjectiveCount(int count)
    {
        string objectiveString = string.Empty;

        if (ObjectiveType == ObjectiveTextType.NEUTRALIZE)
            objectiveString = "Enemies Remaining: " + count;

        GetComponent<Text>().text = objectiveString;
    }

    public void SetObjectiveText(string text)
    {
        GetComponent<Text>().text = text;
    }

    
}
