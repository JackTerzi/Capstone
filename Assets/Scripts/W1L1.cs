using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour {

	public float popUp1DelayTime, // should equal the time it takes for the first enemy to completely spawn
				 popUp1AnimTime,
				 popUp2DelayTime,
				 popUp2AnimTime,
				 timeBeforePopUp2Reappears,
				 numOfSpaceShips; // including the first 1 for tutorial
	
	float timer;

	public GameObject playerPrefab,
					  enemySpaceShip,
					  popUp1,
					  popUp2,
					  greyOut,
					  nextLevelArrow,
					  door;

	GameObject player;
	
	bool finishedPart1,
		 finishedPart2,
		 alreadyShowedPopUp2,
		 finishedLevel;

	public Vector2 playerStartPos,
				   enemy1StartPos,
				   enemy2StartPos;


	void Awake () {
			player = (GameObject) Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
			popUp1.SetActive(false);
			popUp2.SetActive(false);
		
	}


	void Start(){
		Manager.me.SpawnSpaceShipTutorial(enemy1StartPos);
		Manager.me.playerShouldDash = false;
		Debug.Log(Manager.me.playerShouldDash);
		Manager.me.playerShouldShoot = false;
		timer = 0f;
	}


	void Update(){
		timer+= Time.unscaledDeltaTime;

		if (!Manager.me.finishedTutorial){
			Tutorial();
		}
		else if (!finishedLevel){
			WaveEnemies();
		}
		else{
			ActivateNextLevel();
		}
	}
	

	void Tutorial(){
		if (!finishedPart1){

			if (timer > popUp1DelayTime){
				Debug.Log("timer flag passed: " + Time.unscaledTime.ToString());
				if (!popUp1.activeSelf){
					Debug.Log("popup1 active flag passed: " + Time.unscaledTime.ToString());
					popUp1.SetActive(true);
					greyOut.SetActive(true);
					Time.timeScale = 0f; 
					timer = 0f;
					Manager.me.playerShouldDash = true;

				}

			}

			//if (popUp1.activeSelf && (timer > popUp1AnimTime || Manager.me.playerSwiped)){
			if (popUp1.activeSelf && (Manager.me.playerSwiped)){
				Debug.Log("popup1 active and playerSwiped flag passed: " + Time.unscaledTime.ToString());
				popUp1.SetActive(false);
				greyOut.SetActive(false);
				Time.timeScale = 1f;
				timer = 0f;
				finishedPart1 = true;
				return;
			}

		}

		else if(!finishedPart2){

			if (!alreadyShowedPopUp2){ // first time showing pop up 2
				if (timer > popUp2DelayTime || Manager.me.playerSwiped){
					if (!popUp2.activeSelf){
						popUp2.SetActive(true);
						greyOut.SetActive(true);
						Time.timeScale = 0f;
						timer = 0f;
						return;
					}

					if (timer > popUp2AnimTime || Manager.me.playerSwiped){
						popUp2.SetActive(false);
						greyOut.SetActive(false);
						Time.timeScale = 1f;
						timer = 0f;
						alreadyShowedPopUp2 = true;
					}

				}
			}
			
			else{ // not first time showing pop up 2
				if (timer > timeBeforePopUp2Reappears){
					if (!popUp2.activeSelf){
						popUp2.SetActive(true);
						greyOut.SetActive(true);
						Time.timeScale = 0f;
						timer = 0f;
					}

				}

				if (popUp2.activeSelf == true && (timer > popUp2AnimTime || Manager.me.playerSwiped)){
					popUp2.SetActive(false);
					greyOut.SetActive(false);
					Time.timeScale = 1f;
					timer = 0f;
					
					}

				if (Manager.me.score == 1){
					finishedPart2 = true;
					Manager.me.finishedTutorial = true;
					Debug.Log("finished tutorial");
				}  
			}
		}			
	} // end of tutorial function


	void WaveEnemies(){
		// spawns spaceships until numOfSpaceShips reached
		if (Manager.me.numSpaceShipsSpawned < numOfSpaceShips && Manager.me.activeSpaceShips.Count == 0 && Manager.me.numEnemiesOnScreen == 0){
			Manager.me.SpawnSpaceShipBasic(enemy2StartPos);
		}
		if (numOfSpaceShips == Manager.me.numSpaceShipsSpawned && Manager.me.activeEnemies.Count == 0 && Manager.me.activeSpaceShips.Count == 0){
			Debug.Log("finished level");
			finishedLevel = true;
		}

	}


	void ActivateNextLevel(){
		nextLevelArrow.SetActive(true);
		door.GetComponent<SpriteRenderer>().enabled = false;
	}


}