using UnityEngine;
using System.Collections;

public class PlayerCameraController : MonoBehaviour {
    static PlayerCameraController current;

    bool freeLookActive;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (!freeLookActive)
            return;

        float mouseInputHorizontal = Input.GetAxisRaw("Mouse X");
        float mouseInputVertical = Input.GetAxisRaw("Mouse Y");

        Quaternion newRotation = transform.rotation * Quaternion.Euler(new Vector3(mouseInputVertical, mouseInputHorizontal, 0));
        newRotation = Quaternion.Euler(newRotation.eulerAngles.x, newRotation.eulerAngles.y, 0);
        transform.rotation = newRotation;
    }

    /// <summary>
    /// Property to get current static reference.
    /// </summary>
    public static PlayerCameraController Current { get { return current; } }

    public void SetFreeLookActive(bool isActive)
    {
        freeLookActive = isActive;

        if (isActive)
            transform.localPosition = new Vector3(0, 5, -10);
        else
            transform.localPosition = new Vector3(0, 5, -10);

        transform.localRotation = Quaternion.identity;
    }
}
