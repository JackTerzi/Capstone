using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager me;

    public GameObject player,
                      spaceShipTutorialPrefab,
                      spaceShipBasicPrefab;

    public List<GameObject> activeEnemies = new List<GameObject>();
    public List<GameObject> activeSpaceShips = new List<GameObject>();

    public int score,
               numSpaceShipsSpawned,
               numEnemiesOnScreen;

    public bool isGameOver,
                playerShouldShoot,
                playerShouldDash,
                playerSwiped;

    public Text scoreText;
   //public Scene dash, shoot, dashAndShoot, mainMenu;


    void Awake(){
        if (me == null){
            me = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }


	void Start () {
        player = GameObject.FindWithTag("Player");
        
	}
	

	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            playerSwiped = true;
        }
        else{
            playerSwiped = false;
        }


        if(Utility.IsDefined(scoreText)){
            scoreText.text = "Score: " + score;
        }


        if (isGameOver){
            if (scoreText != null){
                scoreText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                scoreText.text = "Game Over. Final Score: " + score + "\nTap to Restart";
                scoreText.alignment = TextAnchor.MiddleCenter;
            }
            

            if(Input.GetMouseButtonDown(0)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}


    public void SpawnSpaceShipTutorial(Vector2 spawnPos){
        GameObject newSpaceShip = (GameObject) Instantiate(spaceShipTutorialPrefab, new Vector2(spawnPos.x, spawnPos.y), Quaternion.identity);
        activeSpaceShips.Add(newSpaceShip);
        numSpaceShipsSpawned++;
    }


    public void SpawnSpaceShipBasic(Vector2 spawnPos){
        GameObject newSpaceShip = (GameObject) Instantiate(spaceShipBasicPrefab, new Vector2(spawnPos.x, spawnPos.y), Quaternion.identity);
        activeSpaceShips.Add(newSpaceShip);
        numSpaceShipsSpawned++;
    }


    public void Loading(string level){
        SceneManager.LoadScene(level);
    }

}