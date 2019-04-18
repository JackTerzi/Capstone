using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : MonoBehaviour {

    public float runSpeed;

    public float detonateTimer,
    startRunTimer;

    bool running;

    Vector2 playerPos;

    Rigidbody2D rb;
    Enemy enem;
    public GameObject explotionEffect;
    public SpriteRenderer glowEffect;
    Color tempColor;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        enem = new Enemy(rb, false);
        tempColor = glowEffect.color;
        //GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Manager.me.player.transform.position;
	}
	

	void Update () {
        if(startRunTimer <= 0 && !running)
        {

            runSpeed *= 2;
            running = true;
        }
        else
        {
            startRunTimer -= Time.deltaTime;

        }
        if (detonateTimer <= 0)
        {
            Detonate();
        }
        else
        {
            detonateTimer -= Time.deltaTime;
            tempColor.a = 1 - Mathf.Pow(detonateTimer, 2);
            glowEffect.color = tempColor;
        }

    }


    void FixedUpdate(){
        if (Utility.IsDefined(Manager.me.player)){
            playerPos = Manager.me.player.transform.position;
            transform.eulerAngles = new Vector3(0f,0f,Geo.ToAng(transform.position, playerPos));
            rb.MovePosition(transform.position + transform.right * runSpeed * Time.fixedDeltaTime);
        }
    }


    void Hit(){
        Destroy(gameObject);
    }

    void Detonate()
    {
        Instantiate(explotionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnDestroy(){ 
        Manager.me.score++;
        Manager.me.numEnemiesOnScreen--;
    }

}
