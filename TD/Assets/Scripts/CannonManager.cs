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
	private GameObject childBall;

	// Use this for initialization
	void Start () {
		type=0;
		energy=maxEnergy;
		uiManager.SetEnergyBar(energy,maxEnergy);
		InvokeRepeating("RegeEnergy",1f,0.5f);
	}
	void Awake(){
		for(int i=0; i<transform.childCount; i++)
		{
			if(transform.GetChild(i).gameObject.CompareTag("Ball"))
			{
				childBall=transform.GetChild(i).gameObject;
			}
	 	}
	}
	
	// Update is called once per frame
	void Shoot(){
		BallView ballView=childBall.GetComponent<BallView>();
		ballView.Shoot();
	}
	public void Create(float ballCD){
		energy-=ballList[type].energyPerBall;
		uiManager.SetEnergyBar(energy,maxEnergy);
		Invoke("NewBall",ballCD);
	}
	void NewBall(){
		childBall = Instantiate(ballList[type].ball,this.transform);
		//childBall.transform.localPosition =new Vector3(0f,0f,0f);	
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
		bool noBall=false;
		type=num;
		for(int i=0; i<transform.childCount; i++)
		{
			if(transform.GetChild(i).gameObject.CompareTag("Ball"))
			{
				Destroy(transform.GetChild(i).gameObject);
				noBall=true;
			}
	 	}
		if(noBall)
			NewBall();
	 }

	public void NotEnoughEnergy(){
		uiManager.NotEnoughEnergy();
	}
}
