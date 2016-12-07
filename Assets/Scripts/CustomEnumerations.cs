using UnityEngine;
using System.Collections;

public enum ControlType {
    Legacy,
    MouseAim,
    MousePos,
    MousePosRoll
}

public enum ObjectiveTextType { NEUTRALIZE_SHIPS = 0, NEUTRALIZE_WAVES = 1 }

public enum RandomOffset { NONE = 0, TINY = 1, SMALL = 2, MEDIUM = 3, LARGE = 4, GIANT = 5 }

public enum UnitType { DRONE = 0, FIGHTER = 1, BOMBER = 2, CRUISER = 3, BATTLESHIP = 4, TURRET = 5, STRUCTURE = 6 }

/// <summary>
/// "SaveInfoMember". Represents settings that can be saved and loaded.
/// </summary>
public enum SIMember {
    CONTROL_TYPE,
    CURRENT_LEVEL,
    CURRENT_PLAYER_SHIP
}

public enum TimerTextType { ELAPSED = 1, REMAINING = 0 }

public enum TurretColor { BLUE = 0, RED = 1 }
