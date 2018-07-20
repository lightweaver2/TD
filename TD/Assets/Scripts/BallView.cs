using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour {

	private BallModel ballModel;
	private CannonManager cannon;
	private Vector3 startPoint;
	private Rigidbody2D rb2d;
	private CircleCollider2D cc2d;
	

	void Awake(){
		ballModel=GetComponent<BallModel>();
		cannon = GetComponentInParent<CannonManager>();
		startPoint=GetComponentInParent<Transform>().position;
		this.transform.position=startPoint;
		rb2d=this.GetComponent<Rigidbody2D>();
		cc2d=this.GetComponent<CircleCollider2D>();
	} 
	void Start(){

	}

	
	public void Shoot(){
		this.gameObject.layer=8;
		rb2d.velocity = (startPoint-this.transform.position).normalized*ballModel.speed;
		this.transform.parent=null;
		cannon.Create(ballModel.BallCD);//create new ball for next shoot
	}
	
}
