using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour {

	public float minimumBashSpeed,
				 firstEnemySpawnDelay,
				 popUp1DelayTime, // the time it takes for the first enemy to completely spawn
				 popUp1AnimTime,
				 popUp2DelayTime,
				 popUp2AnimTime,
				 timeBeforePopUp2Reappears;
	
	float timer;

	public GameObject playerPrefab,
					  enemyBasic,
					  popUp1,
					  popUp2,
					  greyOut;

	GameObject player;

	bool finishedPart1,
		 finishedPart2,
		 alreadyShowedPopUp2,
		 allEnemiesDead,
		 finishedTutorial;

	public Vector2 playerStartPos,
				   enemy1StartPos,
				   enemy2StartPos;

	Movement playerMovement;


	void Awake () {
			//player = Manager.me.player;
			player = (GameObject) Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
			playerMovement = player.GetComponent<Movement>();
			minimumBashSpeed = playerMovement.runAnimSpeed;
			popUp1.SetActive(false);
			popUp2.SetActive(false);
		
	}


	void Start(){
		GameObject newEnemy = (GameObject) Instantiate(enemyBasic, enemy1StartPos, Quaternion.identity);
		Manager.me.playerShouldDash = false;
	}


	void Update(){
		timer+= Time.unscaledDeltaTime;

		if (!finishedTutorial){
			Tutorial();
		}
		else{

		}
	}
	

	void FixedUpdate () {
		//if (player != null){
			//playerPos = player.transform.position;
		//}
		
	}


	void Tutorial(){

		if (!finishedPart1){

			if (timer > popUp1DelayTime){
				if (!popUp1.activeSelf){
					popUp1.SetActive(true);
					Manager.me.playerShouldDash = true;
					timer = 0f;
					Time.timeScale = 0f; 
					greyOut.SetActive(true);
				}

			}

			//if (popUp1.activeSelf && (timer > popUp1AnimTime || Manager.me.playerSwiped)){
			if (popUp1.activeSelf && (Manager.me.playerSwiped)){
				popUp1.SetActive(false);
				finishedPart1 = true;
				Time.timeScale = 1f;
				timer = 0f;
				greyOut.SetActive(false);
				return;
			}

		}

		else if(!finishedPart2){

			if (!alreadyShowedPopUp2){
				if (timer > popUp2DelayTime || Manager.me.playerSwiped){
					if (!popUp2.activeSelf){
						popUp2.SetActive(true);
						Time.timeScale = 0f;
						timer = 0f;
						greyOut.SetActive(true);
						return;
					}

					if (timer > popUp2AnimTime || Manager.me.playerSwiped){
						popUp2.SetActive(false);
						Time.timeScale = 1f;
						greyOut.SetActive(false);
						alreadyShowedPopUp2 = true;
						timer = 0f;
					}

				}
			}
			// already showed popup 2
			else{
				if (timer > timeBeforePopUp2Reappears){
					if (!popUp2.activeSelf){
						popUp2.SetActive(true);
						Time.timeScale = 0f;
						timer = 0f;
						greyOut.SetActive(true);
					}

				}

				if (popUp2.activeSelf == true && (timer > popUp2AnimTime || Manager.me.playerSwiped)){
					popUp2.SetActive(false);
					Time.timeScale = 1f;
					timer = 0f;
					greyOut.SetActive(false);
					}

				if (Manager.me.score == 1){
					finishedPart2 = true;
					finishedTutorial = true;
					Debug.Log("finished tutorial");
				}  
			}
		}			
	} // end of tutorial function

}