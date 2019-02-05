using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public float minimumBashSpeed,
				 firstEnemySpawnDelay,
				 popUp1DelayTime,
				 popUp2DelayTime;
	
	float timer;

	public GameObject playerPrefab,
					  enemyBasic,
					  popUp1,
					  popUp2;

	GameObject player;
	List<GameObject> activeEnemies = new List<GameObject>();

	bool spawnedFirstEnemy,
		 finishedPopUp1,
		 finishedPopUp2,
		 playerSwiped,
		 movingFastEnough;

	public Vector2 playerStartPos,
				   enemy1StartPos,
				   enemy2StartPos;

	Vector2 playerPos;

	Movement playerMovement;

	//runAnimSpeed


	void Awake () {
			//player = Manager.me.player;
			player = (GameObject) Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
			playerMovement = player.GetComponent<Movement>();
			minimumBashSpeed = playerMovement.runAnimSpeed;
			popUp1.SetActive(false);
			popUp2.SetActive(false);
		
	}


	void Update(){
		//playerSwiped = // check from Movement script
		
		if (!finishedPopUp1 || !finishedPopUp2){
			timer += Time.deltaTime;
		
			if (!finishedPopUp1){
				if (timer > firstEnemySpawnDelay && !spawnedFirstEnemy){
					GameObject newEnemy = (GameObject) Instantiate(enemyBasic, enemy1StartPos, Quaternion.identity);
					activeEnemies.Add(newEnemy);
					spawnedFirstEnemy = true;
					timer = 0f;
				}
				else if (timer > popUp1DelayTime && !popUp1.activeSelf && spawnedFirstEnemy){
					popUp1.SetActive(true);
					timer = 0f;
				}
				else if(popUp1.activeSelf && playerSwiped){
					finishedPopUp1 = true;
				}
			}

			else if(!finishedPopUp2){
				if (timer > popUp2DelayTime && !popUp2.activeSelf){
					popUp2.SetActive(true);
				}
				else if (popUp2.activeSelf && playerSwiped){
					popUp2.SetActive(false);
					finishedPopUp2 = true;
				}
			}
		}

		// check if enemy1 is dead, then spawn enemy 2

	}
	

	void FixedUpdate () {
		if (player != null){
			playerPos = player.transform.position;
		}
		
	}
}
