using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys the script's gameObject after a delay.
/// </summary>
public class DestroyAfterDelay : MonoBehaviour {

    [SerializeField]
    float delay = 5.0f;

    [SerializeField]
    bool actuallyDestroy;

	void OnDisable()
    {
        CancelInvoke();
    }

	void OnEnable () {
        Invoke("Destroy", delay);
	}
	
	void Destroy()
    {
        if (actuallyDestroy)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
