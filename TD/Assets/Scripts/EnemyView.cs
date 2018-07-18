using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour {

	private EnemyModel enemyModel;	
	private Rigidbody2D rb2d;
	private SpriteRenderer sprite;
	void Awake(){
		enemyModel=GetComponent<EnemyModel>();
		rb2d=GetComponent<Rigidbody2D>();
		sprite=GetComponent<SpriteRenderer>();
	}

	void Start(){
		rb2d.velocity = new Vector2 (0f,-1f).normalized*enemyModel.speed;
	}

	void UpDate(){
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("Ball")){
			BallModel state = collision.gameObject.GetComponent<BallModel>();
			enemyModel.hp-=state.atk;
		}

		if(enemyModel.hp<=0)
			StartCoroutine (Explode());
	}

	IEnumerator Explode(){
		rb2d.velocity= Vector2.zero;
		sprite.DOColor(Color.red,1f);
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
	}

}
