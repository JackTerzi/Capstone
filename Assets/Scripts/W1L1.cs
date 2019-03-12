using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour
{

    public float popUp1DelayTime, // should equal the time it takes for the first enemy to completely spawn
                 popUp1AnimTime,
                 // popUp2DelayTime,
                 // popUp2AnimTime,
                 //timeBeforePopUp2Reappears,
                 numOfSpaceShips; // including the first 1 for tutorial

    float timer;
    public Animator Hand;
    public GameObject enemySpaceShip,
                      popUp1,
                      //  popUp2,
                      greyOut,
                      nextLevelArrow,
                      door;

    bool finishedPart1,
         finishedPart2,
         //alreadyShowedPopUp2,
         finishedLevel;

    public Vector2 playerStartPos,
                   enemy1StartPos,
                   enemy2StartPos;


    void Awake()
    {
        Manager.me.player = (GameObject)Instantiate(Manager.me.playerPrefab, playerStartPos, Quaternion.identity);
       

    }


    void Start()
    {
       
    }



	void Update(){
		
	}
	

	void Tutorial(){
		
       
	}


	void ActivateNextLevel(){
		nextLevelArrow.SetActive(true);
		door.GetComponent<SpriteRenderer>().enabled = false;
	}



    }

        