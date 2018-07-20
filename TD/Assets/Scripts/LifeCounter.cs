using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour {

	// Use this for initialization
	public UIManager uiManager;
	public int life;


	void Awake(){

	}
	void Start(){
		uiManager.SetLife(life);
	}

	void OnTriggerEnter2D(Collider2D other ){
		if(other.gameObject.CompareTag("Enemy"))
		{
			ChangeLife(-1);
		}
	}

	void ChangeLife(int num){
		life+=num;
		uiManager.SetLife(life);
	}
}
