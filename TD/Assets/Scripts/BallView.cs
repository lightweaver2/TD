using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour {

	private float countDown;
	
	private Vector3 startPoint;
	private Rigidbody2D rb2d;
	public float speed;

	void Awake(){
		startPoint=GetComponentInParent<Transform>().position;
		this.transform.position=startPoint;
		rb2d=this.GetComponent<Rigidbody2D>();
	} 

	void UpDate(){
		if(countDown>0)
		{
			countDown-=Time.deltaTime;
		}
	}

	public void Shoot(){
		this.rb2d.velocity = (startPoint-this.transform.position)*speed;
		countDown
	}
	
}
