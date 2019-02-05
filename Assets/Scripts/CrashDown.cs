using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDown : MonoBehaviour {
    public float sizeIncrease,
    timer,
    maxSize;
    public int numEnemiesInside;
    SpriteRenderer spr;
    CircleCollider2D col;
    bool spawnedEnems;
    public GameObject enemyInside;
    Bashable bsh;

	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        bsh = GetComponent<Bashable>();
	}
	
	// Update is called once per frame
    void Update () {
        if(transform.localScale.x < maxSize){
            transform.localScale = new Vector3(transform.localScale.x + sizeIncrease, transform.localScale.y + sizeIncrease, 1);
        }
        else{
            col.enabled = true;
            spr.color = Color.blue;
            bsh.enabled = true;
            if (!spawnedEnems)
            {
                for (int i = 0; i < numEnemiesInside; i++){
                    GameObject temp = Instantiate(enemyInside, (Vector2)transform.position + Random.insideUnitCircle * maxSize, Quaternion.identity);
                }
                spawnedEnems = true;
            }
        }

    }
}
