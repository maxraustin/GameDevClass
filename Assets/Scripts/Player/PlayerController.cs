using UnityEngine;
using System.Collections;
using System;

enum ControlType { Legacy, Mouse };
public class PlayerController : MonoBehaviour {
    static PlayerController instance;

    [SerializeField]
    ControlType controlType = ControlType.Mouse;

    [SerializeField]
    GameObject reticule; //This is a terrible way to implement a targetting reticule but it was fast, should reimplement.

    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;

    int throttlePercentage = 15;
    bool mouseControlsEnabled = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        UnitTracker.PlayerShip = gameObject;
        CursorController.ShowCursor(false); //This should be handled in game controller once set up.
        if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);
    }

    void Update()
    {
        Vector3 reticuleLocation = transform.position + (transform.forward * 50);
        reticule.transform.position = reticuleLocation;

        if (Input.GetButton("Fire1"))
            weaponsController.FirePrimaryWeapon();
    }

    void FixedUpdate ()
    {
        AdjustThrottle();
        Rotate();
        SetVelocity();
    }

    /// <summary>
    /// Property to get the current PlayerController instance.
    /// </summary>
    public static PlayerController Instance { get { return instance; } }

    /// <summary>
    /// 
    /// </summary>
    void AdjustThrottle()
    {
        float keyboardInputVertical = Input.GetAxisRaw("Vertical");

        if (keyboardInputVertical > 0 && throttlePercentage < 100)
        {
            throttlePercentage++;
            if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);
        }
        else if (keyboardInputVertical < 0 && throttlePercentage > 15)
        {
            throttlePercentage--;
            if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);
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
