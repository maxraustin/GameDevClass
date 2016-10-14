using UnityEngine;
using System.Collections;

public class UserSettings : MonoBehaviour
{
    /*
        Data Members
    */
    static ControlType controlType = DefaultSettings.US_CONTROL_TYPE;

    /*
        Properties
    */
    public static ControlType ControlType { get { return controlType; } set { controlType = value; } }
}
