using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IDragHandler,IDropHandler{
	public GameObject cannon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnDrag(PointerEventData pointerEventData){
		Vector3 pos = Camera.main.ScreenToWorldPoint( (Vector3)pointerEventData.position) + Vector3.forward*10;
		float deg = -Mathf.Atan2((pos-cannon.transform.position).x,(pos-cannon.transform.position).y) * Mathf.Rad2Deg;
		cannon.transform.eulerAngles=new Vector3(cannon.transform.eulerAngles.x,cannon.transform.eulerAngles.y,deg);
		Debug.Log("pos");
	}
	public void OnDrop(PointerEventData pointerEventData){
		if(cannon.GetComponentInChildren<BallView>()!=null)
			cannon.GetComponentInChildren<BallView>().Shoot();
	}

	
}
