﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy1, enemy2, enemy3, enemy4, enemy5;

    public float spawnTime;
    
    public List<GameObject> enemyBag;
    float timer;    // Use this for initialization
    void Start () {

        // calculate number of enemies based on the level
        int numEnemies;
        numEnemies = Manager.me.level * 2 + 5;
        // number of each enemy should be % based 

        BagFiller(numEnemies);

       
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if(timer >= spawnTime)
        {
           
            Instantiate(enemy1, new Vector3(Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x), Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).y, Camera.main.ViewportToWorldPoint(Vector3.one).y)),Quaternion.identity);
            timer = 0;
        }
    }



    /// Put Enemies in a bag like tetris 
    /// Pull a random enemy and remove from the bag 
    /// This way enemies don't spawn in too many times in a row
    /// 

    public void BagFiller(int nE)
    {
        switch (Manager.me.level % 9)
        {
            case 1:
                // fill bag with 5 enemy 1s
                for (int i = 0; i < nE; i++)
                {
                    enemyBag.Add(enemy1);
                }
                break;




            case 2:
                //fill bag with 3 enemy 1s and 2 enemy 2s
                for (int i = 0; i < nE; i++)
                {
                    if(i< nE * .6)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else
                    {
                        enemyBag.Add(enemy2);

                    }
                }
                break;
            case 3:
                // fill bag with 2 enemy 1s and 2 enemy 2s and 1 enemy 3
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .4)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else if(i < nE * .8)
                    {
                        enemyBag.Add(enemy2);

                    }
                    else
                    {
                        enemyBag.Add(enemy3);
                    }
                }
                break;
            case 4:
                //fill bag with 3 enemy 3s and 1 enemy 4 and 1 enemy1
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .2)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else if (i < nE * .8)
                    {
                        enemyBag.Add(enemy3);

                    }
                    else
                    {
                        enemyBag.Add(enemy4);
                    }
                }
                break;
            case 5:
                //fill bag with 4 enemy 4s and 1 enemy 5
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .8)
                    {
                        enemyBag.Add(enemy4);

                    }
                    else
                    {
                        enemyBag.Add(enemy5);
                    }
                }
                break;
            case 6:
                //fill bag with 1 enemy2 3 enemy 4s and 1 enemy 5
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .2)
                    {
                        enemyBag.Add(enemy2);

                    }
                    else if (i < nE * .8)
                    {
                        enemyBag.Add(enemy4);

                    }
                    else
                    {
                        enemyBag.Add(enemy5);
                    }
                }
                break;
            case 7:
                //fill bag with 1 enemy3 2 enemy 4s and 2 enemy 5
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .2)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else if (i < nE * .8)
                    {
                        enemyBag.Add(enemy3);

                    }
                    else
                    {
                        enemyBag.Add(enemy4);
                    }
                }
                break;
            case 8:
                //fill bag with 4 enemy 4s and 1 enemy 5
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .2)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy2);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy3);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy4);

                    }
                    else
                    {
                        enemyBag.Add(enemy5);
                    }
                }
                break;
            case 9:
                //fill bag with 1 of each enemy
                for (int i = 0; i < nE; i++)
                {
                    if (i < nE * .2)
                    {
                        enemyBag.Add(enemy1);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy2);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy3);

                    }
                    else if (i < nE * .2)
                    {
                        enemyBag.Add(enemy4);

                    }
                    else
                    {
                        enemyBag.Add(enemy5);
                    }
                }
                break;
        }
    }

}
