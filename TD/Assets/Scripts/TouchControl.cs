using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IDragHandler , IEndDragHandler  {
	private CannonManager cannonManager;
	private Vector3 startPoint;
	public float maxRadius;
	[SerializeField]
	private UnityEvent onUp;
	
	void Awake(){
		startPoint=GetComponentInParent<Transform>().position;
		cannonManager=GetComponentInParent<CannonManager>();
	}
	
	public void OnDrag(PointerEventData eventData) {
		if(cannonManager.energy < cannonManager.ballList[cannonManager.type].energyPerBall)
			return;

		Vector3 pos = Camera.main.ScreenToWorldPoint( (Vector3)eventData.position) + Vector3.forward*10;
		float radius = (pos - startPoint).magnitude;

		if(radius > maxRadius)
		{
			this.transform.position = startPoint + (pos - startPoint)*maxRadius/radius;
		}	
		else	
			this.transform.position = pos;
		//Debug.Log(camera.ScreenToWorldPoint( (Vector3)eventData.position));
	}
	public void OnEndDrag(PointerEventData eventData) {
		if(cannonManager.energy < cannonManager.ballList[cannonManager.type].energyPerBall)
			return;
		this.onUp.Invoke();
		this.enabled=false;
		//Debug.Log("3");
 
	}
	
}
