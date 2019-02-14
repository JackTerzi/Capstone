using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager me;

    public GameObject player,
                      playerPrefab,
                      spaceShipTutorialPrefab,
                      spaceShipBasicPrefab,
                      enemyBasicPrefab,
                      enemySuicidePrefab;

    public List<GameObject> activeEnemies = new List<GameObject>();
    public List<GameObject> activeSpaceShips = new List<GameObject>();

    public int score,
               numSpaceShipsSpawned,
               numEnemiesOnScreen;

    public bool isGameOver,
                playerShouldShoot,
                playerShouldDash,
                playerSwiped,
                finishedTutorial;

   //public Scene dash, shoot, dashAndShoot, mainMenu;


    void Awake(){
        DontDestroyOnLoad(this.gameObject);
 
        //Debug.Log("awoken");
        if (me == null){
            Debug.Log("I was created");
            me = this;
        }
        else{
  
            Debug.Log("I was destroyed");
            isGameOver = false;
            Destroy(this.gameObject);
        }

        
        

    }


	void Start () {
        me.isGameOver = false;
        me.player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void Update () {
        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }

        numEnemiesOnScreen = activeEnemies.Count;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            playerSwiped = true;
        }
        else{
            playerSwiped = false;
        }

        if (isGameOver){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            me.isGameOver = false;
            me.score = 0;
            me.activeEnemies = new List<GameObject>();
            me.activeSpaceShips = new List<GameObject>();
            me.numEnemiesOnScreen = 0;
            me.numSpaceShipsSpawned = 0;

        }
	}



    public void SpawnSpaceShip(GameObject spaceShip, Vector2 spawnPos){
        GameObject newSpaceShip = (GameObject) Instantiate(spaceShip, new Vector2(spawnPos.x, spawnPos.y), Quaternion.identity);
        activeSpaceShips.Add(newSpaceShip);
        Debug.Log("spawned ship: " + newSpaceShip.gameObject.name);

        if (Time.timeSinceLevelLoad < 1f){
            numSpaceShipsSpawned = 1;
        }
        else{
            numSpaceShipsSpawned ++;
        }

        //numSpaceShipsSpawned++;
        Debug.Log(numSpaceShipsSpawned);
        Debug.Log(this.gameObject.name);
    }


    public static void LoadLevel(string level){

    }

}