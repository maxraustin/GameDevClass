using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    UnitInfo myInfo;
    WeaponsController weaponsController;
    Rigidbody myRigidbody;

    int throttlePercentage = 0;
    

    void Start()
    {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        CursorController.ShowCursor(false); //This should be handled in game controller once set up.
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        AdjustThrottle();
        if (Input.GetButton("Fire1"))
            weaponsController.FirePrimaryWeapon();
    }

    void FixedUpdate ()
    {
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
            throttlePercentage++;
        else if (keyboardInputVertical < 0 && throttlePercentage > 0)
            throttlePercentage--;
    }

    void Rotate()
    {
        float keyboardInputHorizontal = Input.GetAxisRaw("Horizontal");
        float mouseInputVertical = Input.GetAxisRaw("Mouse Y");

        transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(mouseInputVertical, 0, -keyboardInputHorizontal));
    }

    void SetVelocity()
    {
        myRigidbody.velocity = transform.forward * myInfo.GetMaxSpeed() * ((float)throttlePercentage / 100);
    }
}
