using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	public Text lifeText;
	public Text energyText;
	public Text energyWarning;

	public Image energyBar;
	public Text deadText;
	void Awake () {
		//life.color=Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SetEnergyBar(float cur,float max){
		energyBar.fillAmount = cur/max;	
		energyText.text= cur + "/" + max;
	}

	public void SetLife(int num){
		//Debug.Log (life.color);
		lifeText.text="Life :" + num;
		if(num==0)
			deadText.enabled=true;
	}

	public void NotEnoughEnergy(){
		InvokeRepeating("Blink",0f,0.5f);
		Invoke("CancelBlink",3f);
	}
	void Blink(){
		energyWarning.enabled=!energyWarning.enabled;
	}
	void CancelBlink(){
		CancelInvoke();
	}

}
