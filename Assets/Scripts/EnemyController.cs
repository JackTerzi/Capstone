using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Enemy enemy;

    public GameObject enemyBullet;

    SpriteRenderer spr;

    public Color startColor,
                 flashColor;

    public float chargeSpeed;

    float chargeTimer;

    public bool canShoot;


    void Start () {   
        enemy  = new Enemy(this.GetComponent<Rigidbody2D>(), 1, 1, canShoot);
        spr = GetComponent<SpriteRenderer>();
	    Manager.me.activeEnemies.Add(this.gameObject);
	}


    void Update(){
        if (!Manager.me.isGameOver){
            enemy.Movement();
            if (enemy.Shoot()){
                StartCoroutine(Timer(chargeSpeed));

            }
        }
    }


    public IEnumerator Timer(float time){
        WaitForSeconds hi = new WaitForSeconds(time); // Hi!
        spr.color = flashColor;
        yield return hi;
        GameObject thisBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        thisBullet.transform.right = gameObject.transform.right;
        spr.color = startColor;
    }


    void Hit(){
        Manager.me.score++;
        Manager.me.activeEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }


}