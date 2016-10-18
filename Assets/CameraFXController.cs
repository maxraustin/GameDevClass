using UnityEngine;
using System.Collections;

public class CameraFXController : MonoBehaviour {
	ParticleSystem shieldAnim;
	// Use this for initialization
	void Start () {
		shieldAnim = GetComponent<ParticleSystem> ();
	}

	public void PlayShields(){
		shieldAnim.Play ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
