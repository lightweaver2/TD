using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public Text life;
	public Text energy;
	public Image energyBar;
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetEnergyBar(float cur,float max){
		energyBar.fillAmount = cur/max;	
		energy.text= cur + "/" + max;
	}

	public void SetLife(int num){
		life.text="Life :" + num;
	}
}
