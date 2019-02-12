using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEnemy : MonoBehaviour {
    Rigidbody2D rb;
    Enemy enem;
    public GameObject explotionEffect;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        enem = new Enemy(rb, false);
	}
	
	void Update () {
		
	}

    private void FixedUpdate()
    {

    }
    void Hit()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player"){
            GameObject expl = Instantiate(explotionEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Manager.me.score++;
        Manager.me.numEnemiesOnScreen--;
        Manager.me.activeEnemies.Remove(this.gameObject);
    }
}
