using UnityEngine;
using System.Collections;

public class UserSettings : MonoBehaviour
{
    static ControlType controlType = DefaultSettings.US_CONTROL_TYPE;

    public static ControlType ControlType { get { return controlType; } set { controlType = value; } }
}
