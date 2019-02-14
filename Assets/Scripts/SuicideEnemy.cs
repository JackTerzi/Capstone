using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : MonoBehaviour {

    public float runSpeed,
                 explodeDelayTime;

    float timer;

    bool shouldRun;

    Vector2 playerPos;

    Rigidbody2D rb;
    Enemy enem;
    public GameObject explotionEffect;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        enem = new Enemy(rb, false);
        //GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Manager.me.player.transform.position;
	}
	

	void Update () {
        if (!shouldRun){
            timer += Time.deltaTime;
            if (timer >= explodeDelayTime){
                shouldRun = true;
            }
        }
	}


    void FixedUpdate(){
        if (shouldRun){
            playerPos = Manager.me.player.transform.position;
            transform.eulerAngles = new Vector3(0f,0f,Geo.ToAng(transform.position, playerPos));
            rb.MovePosition(transform.position + transform.right * runSpeed * Time.fixedDeltaTime);
        }
    }


    void Hit(){
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col){

        if(col.gameObject.tag == "Player"){
            GameObject expl = Instantiate(explotionEffect, gameObject.transform.position, Quaternion.identity);
            Manager.me.isGameOver = true;
            //Destroy(gameObject);
        }
    }


    void OnDestroy(){ 
        Manager.me.score++;
        Manager.me.numEnemiesOnScreen--;
        Manager.me.activeEnemies.Remove(this.gameObject);
    }

}
