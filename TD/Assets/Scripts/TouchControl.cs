using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IBeginDragHandler , IDragHandler , IDropHandler  {

	public Camera camera;

	[System.Serializable]
	public class PassVector : UnityEvent<Vector3>{};

	[SerializeField]
	// Use this for initialization
	private PassVector onDown;
	[SerializeField]
	private PassVector onDrag;
	[SerializeField]
	private UnityEvent onUp;
	

	
	public void OnBeginDrag(PointerEventData eventData) {
		this.onDown.Invoke(camera.ScreenToWorldPoint( (Vector3)eventData.position)); 
	}
	public void OnDrag(PointerEventData eventData) {
		this.onDrag.Invoke(camera.ScreenToWorldPoint( (Vector3)eventData.position)); 
	}
	public void OnDrop(PointerEventData eventData) {
		this.onUp.Invoke(); 
	}

	
}
