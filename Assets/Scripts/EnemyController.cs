using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Enemy enemy;

    public GameObject enemyBullet, deathEffect;

    SpriteRenderer spr;
    public ParticleSystem ps;
    public Color startColor,
                 flashColor;

    public float chargeSpeed;

    float chargeTimer;

    public bool canShoot;

    public AudioClip chargeSound, shootSound, hurtSound;

    void Start () {   
        enemy  = new Enemy(this.GetComponent<Rigidbody2D>(), 1, 1, canShoot);
        spr = GetComponent<SpriteRenderer>();
	
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
        if (Utility.IsDefined(chargeSound))
            SoundManager.me.Play(chargeSound);

        yield return hi;
        if (Utility.IsDefined(shootSound))
            SoundManager.me.Play(shootSound);
        GameObject thisBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        thisBullet.transform.right = gameObject.transform.right;
        spr.color = startColor;
    }


    void Hit(){
        Manager.me.score++;
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));
        Manager.me.numEnemiesOnScreen--;
        if (Utility.IsDefined(hurtSound))
        {
            SoundManager.me.Play(hurtSound);
        }

    }

    private void OnDestroy()
    {
    }


}