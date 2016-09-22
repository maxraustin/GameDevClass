using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controller for objective text element.
/// </summary>
public class ObjectiveTextController : MonoBehaviour
{
    public void UpdateEnemyCount(int enemyCount)
    {
        GetComponent<Text>().text = "Enemies Remaining: " + enemyCount;
    }
}
