using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageTextController : MonoBehaviour {
    const float defaultDisplayTime = 5;
    float overridenDisplayTime = 0;

    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    public void DisplayMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessage(message));
    }

    public void DisplayMessage(string message, float timeToDisplay)
    {
        overridenDisplayTime = timeToDisplay;
        DisplayMessage(message);
    }

    IEnumerator ShowMessage(string message)
    {
        myText.text = message;
        myText.enabled = true;

        yield return new WaitForSeconds((overridenDisplayTime > 0) ? overridenDisplayTime : defaultDisplayTime);

        overridenDisplayTime = 0;
        myText.text = string.Empty;
        myText.enabled = false;
    }
}
