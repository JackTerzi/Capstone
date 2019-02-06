using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemies;

    float spawnTime;

    public float maxEnemOnScreen;


	void Start () {
       
        spawnTime = Time.time + .5f;
	}
	

	void Update () {
        if (!Manager.me.isGameOver){
            if (Time.time > spawnTime && Manager.me.enemiesOnScreen < maxEnemOnScreen){
                GameObject temp = Instantiate(enemies, new Vector2(Random.Range(-3f, 3f), Random.Range(-5.2f, 5.2f)), Quaternion.identity);
                spawnTime = Time.time + .5f;

            }
        }
	}
    
}
