using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    bool isOverviewCamera;

    static Camera overviewCamera;

    void Awake()
    {
        if (isOverviewCamera && GetComponent<Camera>() != null)
            overviewCamera = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
        if (!isOverviewCamera && GetComponent<Camera>() != null && overviewCamera != null)
        {
            GetComponent<Camera>().enabled = true;
            overviewCamera.enabled = false;
        }
	}
	
    void OnDestroy()
    {
        if (!isOverviewCamera && overviewCamera != null)
            overviewCamera.enabled = true;
    }
}
