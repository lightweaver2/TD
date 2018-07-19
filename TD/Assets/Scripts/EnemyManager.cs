using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	// Use this for initialization

	private float cameraHeight;
	private Vector2 cameraSize;
	public GameObject Enemy;
	public float spawnRate;
	void Awake () {
		cameraHeight = Camera.main.orthographicSize * 2;
		cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
	}
	
	void Start(){
		StartCoroutine(SpawnEnemy());
	}
	// Update is called once per frame
	void Update () {
			}

	IEnumerator SpawnEnemy(){
		while(true){
			Vector3 pos = new Vector3(Random.Range(-cameraSize.x/2,cameraSize.x/2) ,Random.Range(cameraSize.y/2,cameraSize.y*3/4),0f);
			Instantiate(Enemy,pos,Quaternion.identity,this.transform);
			yield return new WaitForSeconds(spawnRate);
		}
	}
}
