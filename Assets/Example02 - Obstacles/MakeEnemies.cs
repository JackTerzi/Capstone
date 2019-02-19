using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemies : MonoBehaviour {

	public GameObject enemy;
	public Transform spawnPos;
	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 1){
			Instantiate(enemy, spawnPos.position, Quaternion.identity);
			timer = 0f;
		}
		
	}
}
