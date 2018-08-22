using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour {

	private Vector2 initVelocity;
	private EnemyModel enemyModel;	
	private Rigidbody2D rb2d;
	private PolygonCollider2D poly2D;
	private SpriteRenderer sprite;
	void Awake(){
		enemyModel=GetComponent<EnemyModel>();
		rb2d=GetComponent<Rigidbody2D>();
		sprite=GetComponent<SpriteRenderer>();
		poly2D=GetComponent<PolygonCollider2D>();
	}

	void Start(){
		
		initVelocity = new Vector2 (0f,-1f).normalized*enemyModel.speed;
		rb2d.velocity = initVelocity;
	}

	void Update(){
		if(this.transform.rotation.z> 3f*Mathf.Deg2Rad)
		{
			//this.transform.eulerAngles -= new Vector3(0f,0f,Time.deltaTime*enemyModel.rotRecoverConst);
			rb2d.AddTorque(-enemyModel.rotRecoverConst);

		}
		else if (this.transform.rotation.z<-3f*Mathf.Deg2Rad)
		{			
			//this.transform.eulerAngles += new Vector3(0f,0f,Time.deltaTime*enemyModel.rotRecoverConst);
			rb2d.AddTorque(enemyModel.rotRecoverConst);

		}
		
		if(rb2d.velocity.y >= initVelocity.y)
			rb2d.AddForce(new Vector2(0f,-1f).normalized * enemyModel.speedRecoverConst );
		


	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("Ball")){
			BallModel state = collision.gameObject.GetComponent<BallModel>();
			enemyModel.hp-=state.atk;
			sprite.DOColor(Color.red,0.5f);
			sprite.DOColor(Color.white,0.5f);
		}
		else if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall")){
			EnemyModel state = this.gameObject.GetComponent<EnemyModel>();
			enemyModel.hp -= state.atk;
			sprite.DOColor(Color.red,0.5f);
			sprite.DOColor(Color.white,0.5f);
		}

	
		if(enemyModel.hp<=0)
			StartCoroutine (Explode());
	}

	IEnumerator Explode(){
		rb2d.velocity= Vector2.zero;
		poly2D.enabled=false;
		sprite.DOColor(Color.black,1f);
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}

}
