using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public Text life;
	void Awake () {
		life.color=Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setLife(int num){
		life.text="Life :" + num;
	}
}
