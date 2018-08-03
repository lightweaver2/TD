using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public Text life;
	public Text energy;
	public Text energyWarning;
	public Image energyBar;
	void Awake () {
		//life.color=Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetEnergyBar(float cur,float max){
		energyBar.fillAmount = cur/max;	
		energy.text= cur + "/" + max;
	}

	public void SetLife(int num){
		//Debug.Log (life.color);
		life.text="Life :" + num;
	}

	public void NotEnoughEnergy(){
		for(int i=0;i<6;i++)
			Invoke("Blink",0.5f);
	}
	void Blink(){
		energyWarning.enabled=!energyWarning.enabled;
	}

}
