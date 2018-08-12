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
	void Start(){

	}
	public void Shoot(){
		this.gameObject.layer=8;
		rb2d.velocity = new Vector2( Mathf.Sin(-cannon.transform.eulerAngles.z*Mathf.Deg2Rad),Mathf.Cos(-cannon.transform.eulerAngles.z*Mathf.Deg2Rad)).normalized*ballModel.speed;
		//Debug.Log(this.transform.localEulerAngles);
		this.transform.parent=null;
		cannon.Create(ballModel.BallCD);//create new ball for next shoot
	}
	
}
