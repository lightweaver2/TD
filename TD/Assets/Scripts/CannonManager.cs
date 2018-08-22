using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {
	[System.Serializable]
	public struct Ball {
		public GameObject ball;
		public float energyPerBall;
		public float stability;
		public float ballCD;
	}
	public List<Ball> ballList;
	public UIManager uiManager;
	public float maxEnergy;
	public float energyRegePerSec; //regeneration rate
	private float energy;
	private int type;	//type of ball
	//private BallView childBall;

	// Use this for initialization
	void Start () {
		type=0;
		energy=maxEnergy;
		uiManager.SetEnergyBar(energy,maxEnergy);
		InvokeRepeating("RegeEnergy",0f,0.1f);
	}
	void Awake(){
		/*for(int i=0; i<transform.childCount; i++)
		{
			if(transform.GetChild(i).gameObject.CompareTag("Ball"))
			{
				childBall=transform.GetChild(i).gameObject.GetComponent<BallView>();
			}
	 	}*/
	}
	
	// Update is called once per frame
	public void Create(float ballCD){
		uiManager.SetEnergyBar(energy,maxEnergy);
		Invoke("NewBall",ballCD);
	}
	void NewBall(){
		/*childBall = */Instantiate(ballList[type].ball,this.transform).GetComponent<BallView>();
	}

	
	void RegeEnergy(){
		//Debug.Log("ddd");
		if (energy<maxEnergy)
		{
			energy += energyRegePerSec * 0.1f;
			if(energy>maxEnergy)
				energy=maxEnergy;
			uiManager.SetEnergyBar((int)energy,maxEnergy);
		}
	}
	 public void ChangeBall(int num){
		BallView ballview=GetComponentInChildren<BallView>();
		type=num;
			
		if(ballview!=null)
		{
			Destroy(ballview.gameObject);
			NewBall();
		}
	 }

	public void NotEnoughEnergy(){
		uiManager.NotEnoughEnergy();
	}

	public void StartCharge(){
		BallView ballview=GetComponentInChildren<BallView>();
		if(ballview==null )
		 	return;
		if(energy<ballList[type].energyPerBall)
		{
			uiManager.NotEnoughEnergy();
			return;
		}
		
		ballview.charging=true;
	}

	public void Shoot(){
		BallView ballview=GetComponentInChildren<BallView>();
		if(ballview==null)
		 	return;

		if(energy<ballList[type].energyPerBall)
		{
			uiManager.NotEnoughEnergy();
			return;
		}

		ballview.Shoot();
		energy-=ballList[type].energyPerBall;
		if(energy<0)
			energy=0;
		uiManager.SetEnergyBar(energy,maxEnergy);

		Create(ballList[type].ballCD);//create new ball for next shoot

	}
}
