using UnityEngine;
using System.Collections;
using System;

enum ControlType { Legacy, Mouse };
public class PlayerController : MonoBehaviour {
    static PlayerController instance;
    public const int MAX_THROTTLE_PERCENT = 100;
    public const int MIN_THROTTLE_PERCENT = 15;

    [SerializeField]
    ControlType controlType = ControlType.Mouse;

    UnitInfo myInfo;
    WeaponsController weaponsController;
    public Rigidbody myRigidbody;

    public int throttlePercentage = MIN_THROTTLE_PERCENT;
    public int rotationSpeed = 2;
    public float mouseSensitivity = 200.0f;
    public Texture2D cursor;
    public float cursorScale = 0.5f;

    bool mouseControlsEnabled = true;


    void Awake() {
        instance = this;
    }

    void Start() {
        myInfo = GetComponent<UnitInfo>();
        weaponsController = GetComponent<WeaponsController>();
        myRigidbody = GetComponent<Rigidbody>();

        UnitTracker.PlayerShip = gameObject;
        CursorController.ShowCursor(true); //This should be handled in game controller once set up.
        if (HUDController.Instance != null) HUDController.Instance.SetThrottleTextPercentage(throttlePercentage);

        Cursor.visible = false;
    }

    void Update() {
        if (Input.GetButton("Fire1"))
            weaponsController.FirePrimaryWeapon();
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
    }

    void Rotate() {
        if (controlType == ControlType.Legacy) {
            float keyboardInputHorizontal = Input.GetAxisRaw("Horizontal") * rotationSpeed;
            float mouseInputVertical = 0;
            if (mouseControlsEnabled)
                mouseInputVertical = Input.GetAxisRaw("Mouse Y");
            //float mouseInputHorizontal= 0;
            //if (mouseControlsEnabled)
            //mouseInputHorizontal = Input.GetAxisRaw("Mouse X");



            Vector3 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            float vertAccel = Math.Abs(mousePos.y) > mouseSensitivity ? (mousePos.y < 0 ? -1 : 1) * 1.0f : mousePos.y / mouseSensitivity;
            float horizAccel = Math.Abs(mousePos.x) > mouseSensitivity ? (mousePos.x < 0 ? -1 : 1) * 1.0f : mousePos.x / mouseSensitivity;
            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(-vertAccel, horizAccel, -keyboardInputHorizontal));
        }
    }

    public void SetMouseControlsEnabled(bool enabled) {
        mouseControlsEnabled = enabled;
    }

    void SetVelocity() {
        myRigidbody.velocity = transform.forward * myInfo.MaxSpeed * ((float)throttlePercentage / 100);
    }

    void OnGUI()
    {
        // If not paused, removes crosshair on pause
        if (Time.timeScale != 0) {
            float screenWidth = (Screen.width / 2) - ((cursor.width * cursorScale) / 2);
            float screenHeight = (Screen.height / 2) - ((cursor.height * cursorScale) / 2);
            float realX = Event.current.mousePosition.x;
            float realY = Event.current.mousePosition.y;


            //float hypotenuse = (float)Math.Sqrt(Math.Pow(Math.Abs((Screen.width / 2) - realX), 2) + Math.Pow((Screen.height / 2) - realY, 2));
            //if (hypotenuse > mouseSensitivity) {
            //    float angle = Mathf.Atan2(Math.Abs((Screen.height / 2) - realY), Math.Abs((Screen.width / 2) - realX));
            //    realX = (Screen.width / 2) + ((Mathf.Cos(angle) * mouseSensitivity) * (realX > Screen.width / 2 ? 1 : -1));
            //    realY = (Screen.height / 2) + ((Mathf.Sin(angle) * mouseSensitivity) * (realY > Screen.height / 2 ? 1 : -1));
            //}

            float xPos = realX - ((cursor.width * cursorScale) / 2);
            float yPos = realY - ((cursor.height * cursorScale) / 2);

            if (cursor != null)
                GUI.DrawTexture(new Rect(xPos, yPos, cursor.width * cursorScale, cursor.height * cursorScale), cursor);
            else
                Debug.Log("No crosshair texture found");
        }
    }
}