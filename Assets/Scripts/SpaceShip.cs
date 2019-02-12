using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

    public float sizeIncrease,
                 maxSize,
                 timeBetweenEnemySpawns,
                 enemySpawnRadius,
                 enemySpawnAngleStep;

    float timer,
          firstEnemySpawnAngle;

    public int numEnemiesInside;

    int numEnemiesSpawned,
        spawnDirection;

    SpriteRenderer spr;

    CircleCollider2D col;

    bool spawnedAllEnemies;

    public GameObject enemyInside;

    Bashable bsh;


	void Start () {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        bsh = GetComponent<Bashable>();
	}
	

    void Update () {
        

        if(transform.localScale.x < maxSize){
            transform.localScale = new Vector3(transform.localScale.x + sizeIncrease, transform.localScale.y + sizeIncrease, 1);
        }
        else{
            spr.color = Color.blue;
            Vector2 newEnemySpawnPos;

            if (!spawnedAllEnemies)
            {
                timer += Time.unscaledDeltaTime;

                if (numEnemiesSpawned == 0){
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
                }

            }
        }
    }

    
    void OnDestroy(){
        Manager.me.activeSpaceShips.Remove(this.gameObject);
    }

}