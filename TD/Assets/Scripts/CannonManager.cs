using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

	public float BallCD;
	public GameObject Ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void UpDate(){
	}

	public void Create(){
		Invoke("NewBall",BallCD);
	}
	void NewBall(){
		GameObject temp;
		temp = Instantiate(Ball,this.transform);
		temp.transform.localPosition =new Vector3(0f,0f,0f);
		
	}

}
