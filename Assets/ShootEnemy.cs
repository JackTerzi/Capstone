using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour {
    public float speed, drag, minDistanceToPlayer,maxDistanceToPlayer, shootTime, chargeSpeed;
    public GameObject enemyBullet, deathEffect;
    public AudioClip chargeSound, shootSound, hurtSound;
    public Color startColor,
                 flashColor;
   

    float startSpeed, shootTimerHolder;
    public bool canAim;
    bool trueIsTowards;
    Rigidbody2D rb;
    SpriteRenderer spr;
	// Use this for initialization
	void Start () {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        startColor = spr.color;
        shootTimerHolder = Time.time + shootTime;

    }

    // Update is called once per frame
    void Update () {
        if (canAim)
        {
            if (Manager.me.player != null)
            {
               transform.right = Vector3.Lerp((Manager.me.player.transform.position - transform.position).normalized, transform.right, Time.deltaTime);
            }
        }


        if(Time.time > shootTimerHolder)
        {
            StartCoroutine(Shoot());
            shootTimerHolder = Time.time + shootTime;
        }

    }


    private void FixedUpdate()
    {
        if (Manager.me.player != null)
        {
            MoveTowardsPlayer();
        }
    }



    IEnumerator Shoot()
    {
        WaitForSeconds firtPause = new WaitForSeconds(chargeSpeed*.75f); // Hi!
        WaitForSeconds secondPause = new WaitForSeconds(chargeSpeed * .25f);
        spr.color = flashColor;
        if (Utility.IsDefined(chargeSound))
            SoundManager.me.Play(chargeSound);

        yield return firtPause;
        canAim = false;
        if (Utility.IsDefined(shootSound))
            SoundManager.me.Play(shootSound);
        GameObject thisBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        thisBullet.transform.right = gameObject.transform.right;
        spr.color = startColor;
        canAim = true;
    }

    void Hit()
    {
        Manager.me.score += 10 * Manager.me.multiplier;
        Manager.me.multiplier++;
        Manager.me.multiTime = 4f;
        Manager.me.activeEnemies.Remove(gameObject);

        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));
        TextMesh multi = Instantiate(Manager.me.MultiText, transform.position, Quaternion.identity);
        multi.text = "X" + Manager.me.multiplier;
        if (Utility.IsDefined(hurtSound))
        {
            SoundManager.me.Play(hurtSound);
        }
    }

    void OnDestroy()
    {
        Manager.me.numEnemiesOnScreen--;
    }

  public void MoveTowardsPlayer()
    {
        if ((Manager.me.player.transform.position - transform.position).magnitude < minDistanceToPlayer)
        {
            trueIsTowards = false;
            speed = startSpeed;
            rb.MovePosition(transform.position + ((transform.position - Manager.me.player.transform.position).normalized * speed * Time.fixedDeltaTime));
        }
        else if (!trueIsTowards && speed > 0)
        {
            rb.MovePosition(transform.position + ((transform.position - Manager.me.player.transform.position).normalized * speed * Time.fixedDeltaTime));
            speed -= drag;
        }
        if ((Manager.me.player.transform.position - transform.position).magnitude > maxDistanceToPlayer)
        {
            trueIsTowards = true;
            speed = startSpeed;
            rb.MovePosition(transform.position + ((-transform.position + Manager.me.player.transform.position).normalized * 2 * speed * Time.fixedDeltaTime));

        }
        else if (trueIsTowards && speed > 0)
        {

            rb.MovePosition(transform.position + ((-transform.position + Manager.me.player.transform.position).normalized * speed * Time.fixedDeltaTime));
            speed -= drag;
        }
    }
}
