using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public static Manager me;
    [Header("Game Properties")]
    public GameObject player,
                      playerPrefab,
                      playerSpawnPoint;
    public GameObject enemy1, enemy2, enemy3, enemy4, enemy5;
    public List<GameObject> enemyBag;
    public TextMesh LevelText;
    Animator textAnimator;
    public GameObject[] activeEnemies;
    public int score,
               numEnemiesOnScreen,
               level,
               numEnemies;
    public bool isGameOver,
                playerShouldShoot,
                playerShouldDash,
                playerSwiped;
    public float spawnTime;
    float logicTimer;
    public bool runLevel,
                runTransition;

    [Space(30)]
    [Header("Main Menu Properties")]
    public GameObject button1, button2, button3;

   //public Scene dash, shoot, dashAndShoot, mainMenu;


    void Awake(){
        me = this;
 
    
        

    }


	void Start () {
        if (me.level == 1)
        {
            LevelText.text = me.level.ToString();
            me.isGameOver = false;
            me.player = GameObject.FindGameObjectWithTag("Player");
            textAnimator = LevelText.GetComponent<Animator>();




            //first level stuff
            StartCoroutine(FirstLevel());
        }
        else if (me.level ==0 )
        {
            StartCoroutine(MainMenuEntry());
        }

    }
	

	void Update () {
        if (player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }








        //transition between levels
        if (!me.isGameOver && me.runTransition)
        {
            StartCoroutine(NextLevel());
        }


        //level logic
        if (!me.isGameOver && me.runLevel)
        {
            logicTimer += Time.deltaTime;
            if(logicTimer >= spawnTime && me.enemyBag.Count > 0 ) {
                int x = (int)Random.Range(0, Manager.me.enemyBag.Count);
               
                Instantiate(Manager.me.enemyBag[x], new Vector3(Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x), Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).y, Camera.main.ViewportToWorldPoint(Vector3.one).y)), Quaternion.identity);
                me.numEnemiesOnScreen += 1;
                me.enemyBag.Remove(Manager.me.enemyBag[x]);
                me.enemyBag.TrimExcess();
                logicTimer = 0;
            }

            if (me.enemyBag.Count <= 0 && me.numEnemiesOnScreen <= 0)
            {
                //stop logic and move to next level
                me.runLevel = false;
                me.runTransition = true;
            }
        }
       



      

        if (isGameOver){
            StopAllCoroutines();

            me.runLevel = false;
            me.runTransition = false;
            me.score = 0;
            me.level = 1;
            me.LevelText.text = me.level.ToString();
            me.activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            var tempBullets = GameObject.FindGameObjectsWithTag("Bullet");
            for( int i = 0; i< tempBullets.Length; i++)
            {
                Destroy(tempBullets[i]);
            }
            for (int i = 0; i<me.activeEnemies.Length; i++)
            {
                Destroy(me.activeEnemies[i]);
            }
            me.numEnemiesOnScreen = 0;
            for(int i = 0; i<enemyBag.Count; i++){
            
                enemyBag.Remove(enemyBag[i]);
            }
            player = Instantiate(playerPrefab, playerSpawnPoint.transform);
            me.numEnemiesOnScreen = 0;

            player.transform.eulerAngles = Vector3.zero;
            StartCoroutine(FirstLevel());
            me.isGameOver = false;

        }
    }




    public static void LoadLevel(string level){

    }
    private IEnumerator NextLevel()
    {
        Debug.Log("Next LEvel Ran");
        me.runTransition = false;

        me.level += 1;
       

        me.numEnemiesOnScreen = 0;

        textAnimator.Play("LevelCounter");
        yield return new WaitForSeconds(3);
        LevelText.text = me.level.ToString();
        textAnimator.Play("LevelCounterReturn");
        yield return new WaitForSeconds(3);
        // calculate number of enemies based on the level
        numEnemies = Manager.me.level * 2 + 5;
        // number of each enemy should be % based 

        BagFiller(numEnemies);
        me.runLevel = true;
        logicTimer = 0;
    }

    private IEnumerator FirstLevel()
    {
        Debug.Log("First Level Ran");

        yield return new WaitForSeconds(2);
        textAnimator.Play("LevelCounterReturn");
        yield return new WaitForSeconds(3);
        numEnemies = Manager.me.level * 2 + 5;
        // number of each enemy should be % based 

        BagFiller(numEnemies);
        me.runLevel = true;
        // calculate number of enemies based on the level
       

    }
    public void BagFiller(int nE)
    {
        switch (Manager.me.level % 4)
        {
            case 1:
                // fill bag with 5 enemy 1s
                for (int i = 0; i < nE; i++)
                {
                    enemyBag.Add(enemy1);
                }
                break;
                //Debug.Log(nE);
                //for (int i = 0; i < nE; i++)
                //{
                //    if (i < nE * .6)
                //    {
                //        me.enemyBag.Add(enemy1);

                //    }
                //    else
                //    {
                //        me.enemyBag.Add(enemy2);

                //    }
                //}
                //break;


            case 2:
                //fill bag with 3 enemy 1s and 2 enemy 2s
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .6)
                    {
                        me.enemyBag.Add(enemy1);

                    }
                    else
                    {
                       me.enemyBag.Add(enemy2);

                    }
                }
                break;
                case 3:
                    // fill bag with 2 enemy 1s and 2 enemy 2s and 1 enemy 3
                    for (int i = 0; i < nE; i++)
                    {
                        if (i < nE * .2)
                        {
                            enemyBag.Add(enemy1);

                        }
                        else 
                        {
                            enemyBag.Add(enemy2);

                        }
                       
                    }
                    break;
                case 0:
                //fill bag with 3 enemy 3s and 1 enemy 4 and 1 enemy1
                for (int i = 0; i < nE; i++)
                {
                    enemyBag.Add(enemy2);
                }
                break;
                
                //case 5:
                //    //fill bag with 4 enemy 4s and 1 enemy 5
                //    for (int i = 0; i < nE; i++)
                //    {
                //        if (i < nE * .8)
                //        {
                //            enemyBag.Add(enemy4);

                //        }
                //        else
                //        {
                //            enemyBag.Add(enemy5);
                //        }
                //    }
                //    break;
                //case 6:
                //    //fill bag with 1 enemy2 3 enemy 4s and 1 enemy 5
                //    for (int i = 0; i < nE; i++)
                //    {
                //        if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy2);

                //        }
                //        else if (i < nE * .8)
                //        {
                //            enemyBag.Add(enemy4);

                //        }
                //        else
                //        {
                //            enemyBag.Add(enemy5);
                //        }
                //    }
                //    break;
                //case 7:
                //    //fill bag with 1 enemy3 2 enemy 4s and 2 enemy 5
                //    for (int i = 0; i < nE; i++)
                //    {
                //        if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy1);

                //        }
                //        else if (i < nE * .8)
                //        {
                //            enemyBag.Add(enemy3);

                //        }
                //        else
                //        {
                //            enemyBag.Add(enemy4);
                //        }
                //    }
                //    break;
                //case 8:
                //    //fill bag with 4 enemy 4s and 1 enemy 5
                //    for (int i = 0; i < nE; i++)
                //    {
                //        if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy1);

                //        }
                //        else if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy2);

                //        }
                //        else if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy3);

                //        }
                //        else if (i < nE * .2)
                //        {
                //            enemyBag.Add(enemy4);

                //        }
                //        else
                //        {
                //            enemyBag.Add(enemy5);
                //        }
                //    }
                //    break;
                //case 9:
                ////fill bag with 1 of each enemy
                //for (int i = 0; i < nE; i++)
                //{
                //    if (i < nE * .2)
                //    {
                //        enemyBag.Add(enemy1);

                //    }
                //    else if (i < nE * .2)
                //    {
                //        enemyBag.Add(enemy2);

                //    }
                //    else if (i < nE * .2)
                //    {
                //        enemyBag.Add(enemy3);

                //    }
                //    else if (i < nE * .2)
                //    {
                //        enemyBag.Add(enemy4);

                //    }
                //    else
                //    {
                //        enemyBag.Add(enemy5);
                //    }
                //}
                //break;
        }
    }

    public IEnumerator MainMenuEntry()
    {
        yield return new WaitForSeconds(2);
        button1.GetComponent<Animator>().Play("ButtonSlide1");
        yield return new WaitForSeconds(.3f);
        button2.GetComponent<Animator>().Play("ButtonSlide1");
        yield return new WaitForSeconds(.3f);
        button3.GetComponent<Animator>().Play("ButtonSlide1");


    }
}