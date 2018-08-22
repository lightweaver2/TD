using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallView : MonoBehaviour {

	private BallModel ballModel;
	private CannonManager cannon;
	private Vector3 startPoint;
	private Rigidbody2D rb2d;
	private CircleCollider2D cir2D;
	private float chargeAmount;//0~100
	private SpriteRenderer sprRend;
	public bool charging=false;

 	
		
	void Awake(){
		ballModel=GetComponent<BallModel>();
		cannon = GetComponentInParent<CannonManager>();
		startPoint=GetComponentInParent<Transform>().position;
		this.transform.position=startPoint;
		rb2d=GetComponent<Rigidbody2D>();
		sprRend=GetComponent<SpriteRenderer>();
		cir2D=GetComponent<CircleCollider2D>();
	} 
	void Update(){
		if(charging)
		 	Charge(Time.deltaTime);
		if(rb2d.velocity.magnitude <= 0.5f && this.gameObject.layer==8)
			StartCoroutine(Explode());
	}
	void Charge(float time){
		if(chargeAmount>=100)
			return;
		chargeAmount += time*ballModel.chargePerSec;
		this.transform.localPosition = Vector3.down*(chargeAmount/200);
		//Debug.Log(chargeAmount);
		//sprRend.color = Color.white + Color.blue*(chargeAmount/100);
	}

	public void Shoot(){
			this.gameObject.layer=8;
			Vector2 vec = new Vector2( Mathf.Sin(-cannon.transform.eulerAngles.z*Mathf.Deg2Rad),Mathf.Cos(-cannon.transform.eulerAngles.z*Mathf.Deg2Rad)).normalized;
			rb2d.velocity = vec*ballModel.speed*(chargeAmount+20)/120;
			//Debug.Log(this.transform.localEulerAngles);
			this.transform.parent=null;
			charging=false;
			//this.enabled=false;
	}

	IEnumerator Explode(){
		//rb2d.velocity= Vector2.zero;
		cir2D.enabled=false;
		sprRend.DOColor(Color.green,1f);
		yield return new WaitForSeconds(1f);
		Destroy(this.gameObject);
	}
	
}
