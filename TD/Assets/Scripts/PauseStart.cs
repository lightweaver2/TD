using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStart : MonoBehaviour {

	delegate void MyDele();
	MyDele myDele;
	// Use this for initialization
	void Start(){
		myDele=Pause;
	}
	public void CallDele(){
		myDele();
	}
	void Pause () {
		Time.timeScale=0;
		myDele=UnPause;
	}
	
	// Update is called once per frame
	void UnPause () {
		Time.timeScale=1;
		myDele=Pause;
	}
}
