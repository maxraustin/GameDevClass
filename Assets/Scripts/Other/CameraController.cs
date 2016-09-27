using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    bool isOverviewCamera;

    [SerializeField]
    bool isPlayerCamera;

    static Camera overviewCamera;
    static CameraController playerCameraControllerInstance;

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

    /// <summary>
    /// Property to get the current CameraController instance for the player camera.
    /// </summary>
    public static CameraController PlayerCameraControllerInstance { get { return playerCameraControllerInstance; } }
}
