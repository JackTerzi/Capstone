using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public float maxSize,
                 timeToReachMaxSize,
                 timeBetweenEnemySpawns,
                 enemySpawnRadius,
                 enemySpawnAngleStep;

    float timer,
         spawnTime,
          firstEnemySpawnAngle;

    public int numEnemiesInside;

    int numEnemiesSpawned,
        spawnDirection;

    SpriteRenderer spr;

    public Color targetColor,
                 startColor;

    CircleCollider2D col;

    bool shipLanded,
         spawnedAllEnemies;

    public GameObject enemyInside;

    Bashable bsh;


	void Start () {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        bsh = GetComponent<Bashable>();
        spawnTime = Time.time;
	}
	

    void Update () {
        timer += Time.deltaTime;

        if(transform.localScale.x < maxSize){
            transform.localScale = new Vector2(maxSize * (Time.time - spawnTime), maxSize * (Time.time - spawnTime));
        }
        else{
            //transform.localScale = new Vector2(maxSize, maxSize);
            Vector2 newEnemySpawnPos;

            if (!spawnedAllEnemies)
            {                
                if (numEnemiesSpawned == 0){
                    spr.color = startColor;
                    firstEnemySpawnAngle = Random.Range(0f,2f * Mathf.PI);
                    float f = Random.Range(0f,1f);
                    if (f < .5f){
                        spawnDirection = 1;
                    }
                    else{
                        spawnDirection = -1;
                    }
                }

                if (timer > timeBetweenEnemySpawns){
                    newEnemySpawnPos.x = enemySpawnRadius * Mathf.Cos(firstEnemySpawnAngle + spawnDirection * enemySpawnAngleStep * numEnemiesSpawned);
                    newEnemySpawnPos.y = enemySpawnRadius * Mathf.Sin(firstEnemySpawnAngle + spawnDirection * enemySpawnAngleStep * numEnemiesSpawned);
                    newEnemySpawnPos += (Vector2) transform.position;

                    GameObject newEnemy = (GameObject) Instantiate(enemyInside, newEnemySpawnPos, Quaternion.identity);
                    Manager.me.activeEnemies.Add(newEnemy);
                    numEnemiesSpawned++;
                    timer = 0f;
                    
                }
                
                if (numEnemiesInside == numEnemiesSpawned){
                    spawnedAllEnemies = true;
                    col.enabled = true;
                    bsh.enabled = true;
                    spr.color = targetColor;
                }

            }
        }
    }

    
    void OnDestroy(){
        Manager.me.activeSpaceShips.Remove(this.gameObject);
    }

}