using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MissionButtonScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler {
	Button self;
	[SerializeField]
	Text missionText;
	[SerializeField]
	string description;
	// Use this for initialization
	void Start () {
		self = GetComponentInParent<Button> ();
	}

	public void OnPointerEnter(PointerEventData eventData){
		missionText.text = description;
	}

	public void OnSelect(BaseEventData eventData){
		missionText.text = description;
	}

	// Update is called once per frame
	void Update () {
	}
}
