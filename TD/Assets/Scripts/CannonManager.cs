using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {
	[System.Serializable]
	public struct Ball {
		public GameObject ball;
		public float energyPerBall;
		public float stability;
	}
	public List<Ball> ballList;

	public UIManager uiManager;
	public float maxEnergy;
	public float energyRegePerSec; //regeneration rate
	public float energy;

	public int type;	//type of ball

	// Use this for initialization
	void Start () {
		type=0;
		energy=maxEnergy;
		uiManager.SetEnergyBar(energy,maxEnergy);
		InvokeRepeating("RegeEnergy",1f,0.5f);
	}
	
	// Update is called once per frame
	void UpDate(){
		
	}

	public void Create(float ballCD){
		energy-=ballList[type].energyPerBall;
		uiManager.SetEnergyBar(energy,maxEnergy);
		Invoke("NewBall",ballCD);
	}
	void NewBall(){
		GameObject temp;
		temp = Instantiate(ballList[type].ball,this.transform);
		temp.transform.localPosition =new Vector3(0f,0f,0f);	
	}

	void RegeEnergy(){
		//Debug.Log("ddd");
	if (energy<maxEnergy)
		{
			energy += energyRegePerSec * 0.5f;
			if(energy>maxEnergy)
				energy=maxEnergy;
			uiManager.SetEnergyBar(energy,maxEnergy);
		}
	}

	 public void ChangeBall(int num){
		if(this.transform.childCount>0)
		{
			Destroy(this.transform.GetChild(0).gameObject);
			Create(0f);
		}
		type=num;
	 }

}
