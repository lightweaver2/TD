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

	void OnTriggerExit2D(Collider2D other ){
		if(other.gameObject.CompareTag("Enemy"))
		{
			ChangeLife(-1);
			Destroy(other.gameObject);
		}
		
	}

	void ChangeLife(int num){
		life+=num;
		if(life<0)
			life=0;
		uiManager.SetLife(life);
	}
}
