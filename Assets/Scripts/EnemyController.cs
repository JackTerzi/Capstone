using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Enemy enemy;
    public GameObject enemyBullet;
    SpriteRenderer spr;
    Color startColor;
    public float chargeSpeed;

    public bool canShoot;
    float chargeTimer;


    void Start () {

    
        enemy  = new Enemy(this.GetComponent<Rigidbody2D>(), 1, 1, canShoot);
        spr = GetComponent<SpriteRenderer>();
        startColor = spr.color;
	}


    void Update(){
        if (!Manager.me.isGameOver)
        {
            enemy.Movement();
            if (enemy.Shoot())
            {

                StartCoroutine(Timer(chargeSpeed));

            }
        }
    }


    public IEnumerator Timer(float time){
        WaitForSeconds hi = new WaitForSeconds(time);
        spr.color = Color.white;
        yield return hi;
        GameObject thisBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        thisBullet.transform.right = gameObject.transform.right;
        spr.color = startColor;
    }


    void Hit(){
        Destroy(gameObject);
    }


    void OnDestroy(){
        Manager.me.score++;
        Manager.me.enemiesOnScreen--;
    }


}