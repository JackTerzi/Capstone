using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour {

	public float minimumBashSpeed,
				 firstEnemySpawnDelay,
				 popUp1DelayTime,
				 popUp2DelayTime;
	
	float timer,
		  frameCount;

	public GameObject playerPrefab,
					  enemyBasic,
					  popUp1,
					  popUp2;

	GameObject player;

	//public List<GameObject> activeEnemies = new List<GameObject>();

	bool spawnedFirstEnemy,
		 finishedPopUp1,
		 finishedPopUp2,
		 playerSwiped,
		 movingFastEnough,
		 turnedOnPopUp1,
		 allEnemiesDead,
		 finishedTutorial;

	public Vector2 playerStartPos,
				   enemy1StartPos,
				   enemy2StartPos;

	Vector2 playerPos,
			startPos;

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
		playerSwiped = Manager.me.playerSwiped;
		
		if (!finishedTutorial){
			Tutorial();
		}
		else{

		}
	}
	

	void FixedUpdate () {
		if (player != null){
			playerPos = player.transform.position;
		}
		
	}


	void Tutorial(){
			if (!finishedPopUp1 || !finishedPopUp2){
				timer += Time.deltaTime;
			
				if (!finishedPopUp1){
					if (timer > firstEnemySpawnDelay && !spawnedFirstEnemy){
						GameObject newEnemy = (GameObject) Instantiate(enemyBasic, enemy1StartPos, Quaternion.identity);
						//activeEnemies.Add(newEnemy);
						spawnedFirstEnemy = true;
						timer = 0f;
					}
					else if (timer > popUp1DelayTime && !popUp1.activeSelf && spawnedFirstEnemy && !turnedOnPopUp1){
						popUp1.SetActive(true);
						timer = 0f;
						turnedOnPopUp1 = true;
					}
					else if(popUp1.activeSelf && playerSwiped){
						finishedPopUp1 = true;
						timer = 0f;
						popUp1.SetActive(false);
					}
					else{
						//timer = 0f;
					}
				}
					
				else if(!finishedPopUp2){
					if (timer > popUp2DelayTime && !popUp2.activeSelf){
						popUp2.SetActive(true);
					}
					else if (popUp2.activeSelf && playerSwiped){
						Debug.Log("turned off");
						popUp2.SetActive(false);
						finishedPopUp2 = true;
					}
				}
				
			}

			if (finishedPopUp1 && Manager.me.score == 1){
				allEnemiesDead = (GameObject.FindGameObjectWithTag("Enemy") == null);
				if (allEnemiesDead){
					GameObject newEnemy = (GameObject) Instantiate(enemyBasic, enemy2StartPos, Quaternion.identity);
				}
			}

			if (Manager.me.score == 2 && finishedPopUp1 && finishedPopUp2){
				finishedTutorial = true;
			}
		}
	

}
