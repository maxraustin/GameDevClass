using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    static PlayerController instance;
    public const int MAX_THROTTLE_PERCENT = 100;
    public const int MIN_THROTTLE_PERCENT = 15;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    public Rigidbody myRigidbody;

    public int throttlePercentage = MIN_THROTTLE_PERCENT;

    float posOuterTresh = 200.0f;
    float mouseAimSensitivity = 3.0f;
    public Texture2D cursor;
    public float cursorScale = 0.5f;
	[SerializeField]
	ParticleSystem[] thrusterFX;
	[SerializeField]
	AudioSource engineSFX;

    void Awake() {
        instance = this;
    }

    void Start() {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);

    }

    void Update() {
        if (Time.timeScale == 0)
            return;

        if (Input.GetButton("Fire1"))
            weaponsController.FirePrimaryWeapon();
        if (Input.GetButton("Fire2"))
            weaponsController.FireSecondaryWeapon();
        if (Input.GetButton("Fire3"))
            weaponsController.FireTertiaryWeapon();
    }

    void FixedUpdate() {
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
    void AdjustThrottle() {
        float keyboardInputVertical = Input.GetAxisRaw("Vertical");

        if (keyboardInputVertical > 0 && throttlePercentage < MAX_THROTTLE_PERCENT) {
            throttlePercentage++;
            if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);
        } else if (keyboardInputVertical < 0 && throttlePercentage > MIN_THROTTLE_PERCENT) {
            throttlePercentage--;
            if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);
        }
		//Adjust the thrust fx based on throttle
		for (int i = 0; i < thrusterFX.Length; i++) {
			thrusterFX [i].startSpeed = throttlePercentage/5;
			thrusterFX [i].startLifetime = Mathf.Clamp(throttlePercentage,0f,.1f);
			thrusterFX [i].emissionRate = myRigidbody.velocity.magnitude;
		}
		//A little hacky of a way to do this but i wanted a precise range for the sound
		engineSFX.pitch = throttlePercentage / 18f;
    }

    void Rotate() {
        if (UserSettings.ControlType == ControlType.MouseAim)
        {
            float keyboardRollInput = Input.GetAxisRaw("Horizontal");

            float mousePitchInput = Input.GetAxisRaw("Mouse Y");
            if (mousePitchInput > 1) mousePitchInput = 1;
            else if (mousePitchInput < -1) mousePitchInput = -1;

            float mouseYawInput = Input.GetAxisRaw("Mouse X");  
            if (mouseYawInput > 1) mouseYawInput = 1;
            else if (mouseYawInput < -1) mouseYawInput = -1;

            transform.rotation *= Quaternion.Euler(new Vector3(-mousePitchInput * myInfo.MaxPitchSpeed, mouseYawInput * myInfo.MaxYawSpeed, -keyboardRollInput * myInfo.MaxRollSpeed));
        }
        else if (UserSettings.ControlType == ControlType.MousePos)
        {
            float keyboardRollInput = Input.GetAxisRaw("Horizontal") * myInfo.MaxRollSpeed;

            Vector3 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            float vertAccel = Math.Abs(mousePos.y) > posOuterTresh ? (mousePos.y < 0 ? -1 : 1) * 1.0f : mousePos.y / posOuterTresh;
            float horizAccel = Math.Abs(mousePos.x) > posOuterTresh ? (mousePos.x < 0 ? -1 : 1) * 1.0f : mousePos.x / posOuterTresh;

            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-vertAccel * myInfo.MaxPitchSpeed, horizAccel * myInfo.MaxYawSpeed, -keyboardRollInput * myInfo.MaxRollSpeed));
        }
        else if (UserSettings.ControlType == ControlType.MousePosRoll)
        {
            float keyboardYawInput = Input.GetAxisRaw("Horizontal");

            Vector3 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            float vertAccel = Math.Abs(mousePos.y) > posOuterTresh ? (mousePos.y < 0 ? -1 : 1) * 1.0f : mousePos.y / posOuterTresh;
            float horizAccel = Math.Abs(mousePos.x) > posOuterTresh ? (mousePos.x < 0 ? -1 : 1) * 1.0f : mousePos.x / posOuterTresh;

            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-vertAccel * myInfo.MaxPitchSpeed, keyboardYawInput * myInfo.MaxYawSpeed, -horizAccel * myInfo.MaxRollSpeed));
        }
        else if (UserSettings.ControlType == ControlType.Legacy)
        {
            float keyboardYawInput = Input.GetAxisRaw("Horizontal");

            float mouseInputVertical = Input.GetAxisRaw("Mouse Y");
            if (mouseInputVertical > 1) mouseInputVertical = 1;
            else if (mouseInputVertical < -1) mouseInputVertical = -1;

            float mouseInputHorizontal = Input.GetAxisRaw("Mouse X") * myInfo.MaxRollSpeed;
            if (mouseInputHorizontal > 1) mouseInputHorizontal = 1;
            else if (mouseInputHorizontal < -1) mouseInputHorizontal = -1;

            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-mouseInputVertical * myInfo.MaxPitchSpeed, keyboardYawInput * myInfo.MaxYawSpeed, -mouseInputHorizontal * myInfo.MaxRollSpeed));
        }
    }

    void SetVelocity() {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }

    
    void OnGUI()
    {
        CursorController.DrawReticule();
        /*
        // If not paused, removes crosshair on pause
        if (Time.timeScale != 0) {
            float screenWidth = (Screen.width / 2) - ((cursor.width * cursorScale) / 2);
            float screenHeight = (Screen.height / 2) - ((cursor.height * cursorScale) / 2);
            float realX = Event.current.mousePosition.x;
            float realY = Event.current.mousePosition.y;


            //float hypotenuse = (float)Math.Sqrt(Math.Pow(Math.Abs((Screen.width / 2) - realX), 2) + Math.Pow((Screen.height / 2) - realY, 2));
            //if (hypotenuse > mousePosSensitivity) {
            //    float angle = Mathf.Atan2(Math.Abs((Screen.height / 2) - realY), Math.Abs((Screen.width / 2) - realX));
            //    realX = (Screen.width / 2) + ((Mathf.Cos(angle) * mousePosSensitivity) * (realX > Screen.width / 2 ? 1 : -1));
            //    realY = (Screen.height / 2) + ((Mathf.Sin(angle) * mousePosSensitivity) * (realY > Screen.height / 2 ? 1 : -1));
            //}

            float xPos = realX - ((cursor.width * cursorScale) / 2);
            float yPos = realY - ((cursor.height * cursorScale) / 2);

            if (cursor != null)
                GUI.DrawTexture(new Rect(xPos, yPos, cursor.width * cursorScale, cursor.height * cursorScale), cursor);
            else
                Debug.Log("No crosshair texture found");
        }
        */
    }
    
}