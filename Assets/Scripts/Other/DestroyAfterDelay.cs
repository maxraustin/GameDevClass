using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys the script's gameObject after a delay.
/// </summary>
public class DestroyAfterDelay : MonoBehaviour {

    [SerializeField]
    float delay = 5.0f;

	void OnDisable()
    {
        CancelInvoke();
    }

	void OnEnable () {
        Invoke("Destroy", delay);
	}
	
	void Destroy()
    {
        Destroy(gameObject);
    }
}
