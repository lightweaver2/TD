using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour {
	private LineRenderer lineRend;
	// Use this for initialization
	void Awake () {
		lineRend=GetComponent<LineRenderer>();
		lineRend.useWorldSpace=true;
	}
	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.up,15,1<<9);
		lineRend.SetPosition(0,this.transform.position);
		if(hit.collider!=null)
			lineRend.SetPosition(1,hit.point);
		else
			lineRend.SetPosition(1,this.transform.position+this.transform.up*15);
	}

}
