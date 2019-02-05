using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour {
    public static Manager me;
    public GameObject player;
    public float enemiesOnScreen,
                currrentLevel;
    public int score;
    public bool gameOver,
                shouldShoot,
                shouldDash;
    public Text scoreText;

  
   //public Scene dash, shoot, dashAndShoot, mainMenu;
	// Use this for initialization
	void Start () {
        me = this;
        player = GameObject.FindWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Utility.IsDefined(scoreText)){
            scoreText.text = "Score: " + score;
        }


        if (gameOver){
            scoreText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            scoreText.text = "Game Over. Final Score: " + score + "\nTap to Restart";
            scoreText.alignment = TextAnchor.MiddleCenter;
            if(Input.GetMouseButtonDown(0)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}

    public void Loading(string level)
    {
        SceneManager.LoadScene(level);
    }

}
