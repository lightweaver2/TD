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
	private AudioSource hitSound;
	private Sequence hitAnimation;
	void Awake(){
		enemyModel=GetComponent<EnemyModel>();
		rb2d=GetComponent<Rigidbody2D>();
		sprite=GetComponent<SpriteRenderer>();
		poly2D=GetComponent<PolygonCollider2D>();
		hitSound=GetComponent<AudioSource>();
	}

	void Start(){
		
		initVelocity = new Vector2 (0f,-1f).normalized*enemyModel.speed;
		rb2d.velocity = initVelocity;
		hitAnimation = DOTween.Sequence();
		hitAnimation.Append(sprite.DOColor(Color.red,0.5f));
		hitAnimation.Append(sprite.DOColor(Color.white,0.5f));
	}

	void Update(){
		if(this.transform.rotation.z> 0.1f*Mathf.Deg2Rad)
		{
			//this.transform.eulerAngles -= new Vector3(0f,0f,Time.deltaTime*enemyModel.rotRecoverConst);
			rb2d.AddTorque(-enemyModel.rotRecoverConst);

		}
		else if (this.transform.rotation.z<-0.1f*Mathf.Deg2Rad)
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
			HitSFX();
		}
		else if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall")){
			EnemyModel state = this.gameObject.GetComponent<EnemyModel>();
			enemyModel.hp -= state.atk;
			HitSFX();
		}

	
		if(enemyModel.hp<=0)
			StartCoroutine (Explode());
	}
	void HitSFX(){
		DOTween.Rewind("hitAnimation");
		DOTween.Play("hitAnimation");
		hitSound.Play();
	}

	IEnumerator Explode(){
		this.gameObject.layer=10;
		rb2d.velocity= Vector2.zero;
		poly2D.enabled=false;
		sprite.DOColor(Color.black,1f);
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}

}
