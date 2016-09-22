using UnityEngine;
using System.Collections;
using System;

enum ControlType { Legacy, Mouse };
public class PlayerController : MonoBehaviour {
    static PlayerController current;

    [SerializeField]
    ControlType controlType = ControlType.Mouse;

    [SerializeField]
    GameObject reticule;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;

    int throttlePercentage = 15;
    bool mouseControlsEnabled = true;

    void Awake()
    {
        current = this;
        UnitTracker.playerShip = gameObject;
    }

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        CursorController.ShowCursor(false); //This should be handled in game controller once set up.
    }

    void Update()
    {
        Vector3 reticuleLocation = transform.position + (transform.forward * 50);
        reticule.transform.position = reticuleLocation;

        if (Input.GetButton("Fire1"))
            weaponsController.FirePrimaryWeapon();
    }

    /// <summary>
    /// Property to get current static reference.
    /// </summary>
    public static PlayerController Current { get { return current; } }

    void FixedUpdate ()
    {
        AdjustThrottle();
        Rotate();
        SetVelocity();
    }

    /// <summary>
    /// 
    /// </summary>
    void AdjustThrottle()
    {
        float keyboardInputVertical = Input.GetAxisRaw("Vertical");

        if (keyboardInputVertical > 0 && throttlePercentage < 100)
        {
            throttlePercentage++;
            UIElementsTracker.Current.GetThrottleTextController().UpdateThrottleRate(throttlePercentage);
        }
        else if (keyboardInputVertical < 0 && throttlePercentage > 15)
        {
            throttlePercentage--;
            UIElementsTracker.Current.GetThrottleTextController().UpdateThrottleRate(throttlePercentage);
        }
    }

    void Rotate()
    {
        if (controlType == ControlType.Legacy)
        {
            //float keyboardInputHorizontal = Input.GetAxisRaw("Horizontal");
            float mouseInputVertical = 0;
            if (mouseControlsEnabled)
                mouseInputVertical = Input.GetAxisRaw("Mouse Y");
            float mouseInputHorizontal= 0;
            if (mouseControlsEnabled)
                mouseInputHorizontal = Input.GetAxisRaw("Mouse X");

            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(mouseInputVertical, 0, -mouseInputHorizontal));
        }
    }

    public void SetMouseControlsEnabled(bool enabled)
    {
        mouseControlsEnabled = enabled;
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }
}
