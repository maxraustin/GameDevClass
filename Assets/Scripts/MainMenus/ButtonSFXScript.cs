using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ButtonSFXScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler {

	// Use this for initialization
	void Start () {
	
	}
	public void OnPointerEnter(PointerEventData eventData){
		GetComponent<AudioSource> ().Play ();
	}
	public void OnSelect(BaseEventData eventData){
		GetComponent<AudioSource> ().Play ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
