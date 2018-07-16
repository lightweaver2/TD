using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveWall : MonoBehaviour {

	private BoxCollider2D bc2d;
	public bool leftWall;
	// Use this for initialization
	void Awake() {
    	 bc2d = GetComponent<BoxCollider2D>();

   	 	float cameraHeight = Camera.main.orthographicSize * 2;
    	Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
    	Vector2 boxSize = bc2d.bounds.size;

		this.transform.localScale = new Vector3(1,cameraSize.y/boxSize.y*2,1) ;

		if(leftWall)
			this.transform.position = new Vector3(-cameraSize.x/2-0.5f,0,0);
		else
		{
			this.transform.position = new Vector3(cameraSize.x/2+0.5f,0,0);
		}
   
	}
}
