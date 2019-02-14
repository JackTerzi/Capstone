using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour {

	float timer;

	// Use this for initialization
	void Start () {
		
	}
	
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
    void SpawnEnemy()
    {
        Debug.Log("hi");
        Vector2 newEnemyPosition = Random.insideUnitCircle * 3;
        int i = (int)Mathf.Round(Random.Range(.5f, 2.5f));
        GameObject enemyToSpawn = null;
        if (i == 1)
        {
            enemyToSpawn = Manager.me.enemyBasicPrefab;
        }
        else if (i == 2)
        {
            enemyToSpawn = Manager.me.enemySuicidePrefab;
        }
            GameObject newEnemy = (GameObject)Instantiate(enemyToSpawn, newEnemyPosition, Quaternion.identity);
    }
}


/*




   // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
    void SpawnEnemy()
    {
        Debug.Log("hi");
        Vector2 newEnemyPosition = Random.insideUnitCircle * 3;
        int i = (int)Mathf.Round(Random.Range(.5f, 2.5f));
        GameObject enemyToSpawn = null;
        if (i == 1)
        {
            enemyToSpawn = Manager.me.enemyBasicPrefab;
        }
        else if (i == 2)
        {
            enemyToSpawn = Manager.me.enemySuicidePrefab;
        }
            GameObject newEnemy = (GameObject)Instantiate(enemyToSpawn, newEnemyPosition, Quaternion.identity);
    }
}


 */