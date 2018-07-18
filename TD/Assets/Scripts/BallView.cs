using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour {

	private BallModel ballModel;
	private CannonManager cannon;
	private Vector3 startPoint;
	private Rigidbody2D rb2d;
	

	void Awake(){
		ballModel=GetComponent<BallModel>();
		cannon = GetComponentInParent<CannonManager>();
		startPoint=GetComponentInParent<Transform>().position;
		this.transform.position=startPoint;
		rb2d=this.GetComponent<Rigidbody2D>();
	} 

	
	public void Shoot(){
		this.rb2d.velocity = (startPoint-this.transform.position).normalized*ballModel.speed;
		cannon.Create();
	}
	
}
