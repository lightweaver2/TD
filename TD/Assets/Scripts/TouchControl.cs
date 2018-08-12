using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour{
	/*public float maxRadius;

	private CannonManager cannonManager;
	private Vector3 startPoint;
	[SerializeField]
	private UnityEvent onUp;
	
	void Awake(){
		startPoint=this.transform.parent.position;
		cannonManager=GetComponentInParent<CannonManager>();
	}
	
	public void OnDrag(PointerEventData eventData) {
		if(cannonManager.energy < cannonManager.ballList[cannonManager.type].energyPerBall)
		{
			//cannonManager.NotEnoughEnergy();
			return;
		}
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
 
	}*/

	
	
}
