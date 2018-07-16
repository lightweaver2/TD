using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IDragHandler , IEndDragHandler  {
	private Vector3 startPoint;
	public float maxRadius;

	[System.Serializable]
	public class PassVector : UnityEvent<Vector3>{};

	[SerializeField]
	// Use this for initialization
	private PassVector onDown;
	[SerializeField]
	private PassVector onDrag;
	[SerializeField]
	private UnityEvent onUp;
	
	void Awake(){
		startPoint=GetComponentInParent<Transform>().position;
	}
	
	public void OnDrag(PointerEventData eventData) {
		Vector3 pos = Camera.main.ScreenToWorldPoint( (Vector3)eventData.position) + Vector3.forward*10;
		float radius = (pos - startPoint).magnitude;

		if(radius > maxRadius)
		{
			this.transform.position = startPoint + (pos - startPoint)*maxRadius/radius;
		}	
		else	
			this.onDrag.Invoke(pos); 
		//Debug.Log(camera.ScreenToWorldPoint( (Vector3)eventData.position));
	}
	public void OnEndDrag(PointerEventData eventData) {
		this.onUp.Invoke();
		//Debug.Log("3");
 
	}
	
}
