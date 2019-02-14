using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class W1L2 : MonoBehaviour {

	Vector2[] spawnPositions;

	public float spawnRate;

	float timer;

	int wave1Counter, wave2Counter, wave3Counter, currentWave = 0;
	int maxEnemiesWave1 = 5, maxEnemiesWave2 = 3, maxEnemiesWave3 = 6;


	void FindSpawnPositions(){
		GameObject[] spawnObjs = GameObject.FindGameObjectsWithTag("SpawnPos");
		int numObjs = spawnObjs.Length;
		spawnPositions = new Vector2[numObjs];
		for (int i = 0; i < numObjs; i ++){
			spawnPositions[i] = spawnObjs[i].transform.position;
		}
		//Debug.Log(numObjs);
	}


	void Wave1(){
		if (timer > spawnRate && Manager.me.numEnemiesOnScreen < maxEnemiesWave1){
			int spawn1 = (int) Random.Range(.5f, spawnPositions.Length - .5f);
			Instantiate(Manager.me.enemyBasicPrefab, spawnPositions[spawn1], Quaternion.identity);
			wave1Counter++;
			timer = 0;
		}

		if (Manager.me.numEnemiesOnScreen == 0 && wave1Counter == maxEnemiesWave1){
			currentWave ++;
		}

	}

	void Wave2(){
		if (timer > spawnRate && Manager.me.numEnemiesOnScreen < maxEnemiesWave2){
			int spawn1 = (int) Random.Range(.5f, spawnPositions.Length - .5f);
			Instantiate(Manager.me.enemySuicidePrefab, spawnPositions[spawn1], Quaternion.identity);
			wave2Counter++;
			timer = 0;
		}

		if (Manager.me.numEnemiesOnScreen == 0 && wave1Counter == maxEnemiesWave2){
			currentWave ++;
		}
	}


	void Wave3(){
		if (timer > spawnRate && Manager.me.numEnemiesOnScreen < maxEnemiesWave3){
			int spawn1 = (int) Random.Range(.5f, spawnPositions.Length - .5f);
			GameObject enemyToSpawn;
			if (wave3Counter < maxEnemiesWave3 / 2){
				enemyToSpawn = Manager.me.enemyBasicPrefab;
			}
			else{
				enemyToSpawn = Manager.me.enemySuicidePrefab;
			}

			Instantiate(enemyToSpawn, spawnPositions[spawn1], Quaternion.identity);
			wave3Counter++;
			timer = 0;
		}

		if (Manager.me.numEnemiesOnScreen == 0 && wave1Counter == maxEnemiesWave3){
			currentWave ++;
		}
	}


	void Awake () {
		FindSpawnPositions();
		currentWave = 1;
		
	}
	

	void Update () {
		timer += Time.deltaTime;

		switch(currentWave){
			case (1): {
				Wave1();
				return;
			}
			case (2):{
				Wave2();
				return;

			}
			case (3):{
				Wave3();
				return;
			}
			default:{
				Debug.Log("wave over");
				return;
			}
		}
	}

}