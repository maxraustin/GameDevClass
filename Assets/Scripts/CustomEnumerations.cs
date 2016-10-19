using UnityEngine;
using System.Collections;

public enum ControlType {
    Legacy,
    MouseAim,
    MousePos,
    MousePosRoll
}

public enum ObjectiveTextType { NEUTRALIZE_SHIPS = 0, NEUTRALIZE_WAVES = 1 }

/// <summary>
/// "SaveInfoMember". Represents settings that can be saved and loaded.
/// </summary>
public enum SIMember
{
    CONTROL_TYPE,
    CURRENT_LEVEL,
    CURRENT_PLAYER_SHIP
}

public enum TimerTextType { ELAPSED = 1, REMAINING = 0 }
