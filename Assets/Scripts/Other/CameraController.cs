using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    const float MAX_Z = -12;
	const float MIN_Z = -8;

    [SerializeField]
    bool isOverviewCamera;

    [SerializeField]
    bool isPlayerCamera;

    static Camera overviewCamera;
    static CameraController playerCameraControllerInstance;

    PlayerController parentPlayerController;

    void Awake()
    {
        if (isOverviewCamera && GetComponent<Camera>() != null)
            overviewCamera = GetComponent<Camera>();
        if (isPlayerCamera)
        {
            if (playerCameraControllerInstance != null && playerCameraControllerInstance != this)
                Debug.LogError("There are multiple player cameras in the scene.");

            playerCameraControllerInstance = this;
        }

        if (isOverviewCamera && isPlayerCamera)
            throw new System.Exception("Camera can not be set as both overview camera and player camera.");

        parentPlayerController = GetComponentInParent<PlayerController>();
    }

	// Use this for initialization
	void Start () {
        if (isPlayerCamera && GetComponent<Camera>() != null && overviewCamera != null)
        {
            GetComponent<Camera>().enabled = true;
            overviewCamera.enabled = false;
        }
	}
	
    void OnDestroy()
    {
        if (isPlayerCamera && overviewCamera != null)
            overviewCamera.enabled = true;
    }

    void Update() {
        if (parentPlayerController != null) {
            float z_offset = ((((float)(parentPlayerController.throttlePercentage - PlayerController.MIN_THROTTLE_PERCENT) /
                (float)(PlayerController.MAX_THROTTLE_PERCENT - PlayerController.MIN_THROTTLE_PERCENT))) *
                (MAX_Z - MIN_Z)) + MIN_Z;
            transform.localPosition = new Vector3(0, 5, z_offset);
        }
    }
   
    /// <summary>
    /// Property to get the current CameraController instance for the player camera.
    /// </summary>
    public static CameraController PlayerCameraControllerInstance { get { return playerCameraControllerInstance; } }
}
