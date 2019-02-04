using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Enemy enemy;
    public GameObject enemyBullet;
    public SpriteRenderer spr;
    Color startColor;
    public float chargeSpeed;

    public bool canShoot;
    float chargeTimer;
    // Use this for initialization
    void Start () {

    
        enemy  = new Enemy(this.GetComponent<Rigidbody2D>(), 1, 1, canShoot);
        spr = gameObject.GetComponent<SpriteRenderer>();
        startColor = spr.color;
	}

    // Update is called once per frame
    void Update()
    {
        if (!Manager.me.gameOver)
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

    private void OnDestroy()
    {
        Manager.me.score++;
        Manager.me.enemiesOnScreen--;
    }
}
