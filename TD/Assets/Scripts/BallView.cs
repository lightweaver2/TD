using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour {

	public Vector3 startPoint;
	private Rigidbody2D rb2d;
	public float speed;

	void Awake(){
		startPoint=GetComponentInParent<Transform>().position;
		this.transform.position=startPoint;
		rb2d=this.GetComponent<Rigidbody2D>();
	} 

	void Follow(Vector3 pos){
		this.transform.position=pos;
	}
	public void Shoot(){
		this.rb2d.velocity = (this.transform.position-startPoint)*speed;
	}
	
}
