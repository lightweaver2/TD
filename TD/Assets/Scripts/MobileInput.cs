using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler{
	public GameObject cannon;
	private bool charging=false; //用charging來記住ball的狀態
	// Use this for initialization
	void Update () {
		if( charging  )
		{
			cannon.GetComponent<CannonManager>().StartCharge();
		}
	}

	public void OnDrag(PointerEventData pointerEventData){
		TurnCannon(pointerEventData);

	}
	// update is called once per frame
	public void OnPointerDown(PointerEventData pointerEventData){
		TurnCannon(pointerEventData);
		charging=true;
		
	}
	public void OnPointerUp(PointerEventData pointerEventData){
		cannon.GetComponent<CannonManager>().Shoot();
		charging=false;
	}

	void TurnCannon(PointerEventData data){
		Vector3 pos = Camera.main.ScreenToWorldPoint( (Vector3)data.position) + Vector3.forward*10;
		float deg = -Mathf.Atan2((pos-cannon.transform.position).x,(pos-cannon.transform.position).y) * Mathf.Rad2Deg;
		cannon.transform.eulerAngles=new Vector3(cannon.transform.eulerAngles.x,cannon.transform.eulerAngles.y,deg);
		
	}

	
}
