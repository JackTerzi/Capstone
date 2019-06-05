using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuicideEnemy : MonoBehaviour {

    public float playerBuffer, inZone, runSpeed;

    public float detonateTimer,
    startRunTimer;

    public AudioClip explosionSound, runSound, hurtSound;

    bool running, arrived, wall;

    Vector2 targetPos;
    Transform player;
    Rigidbody2D rb;
    Enemy enem;
    public GameObject explotionEffect, deathEffect, circleBoy;
    public SpriteRenderer glowEffect;
    Color tempColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enem = new Enemy(rb, false);
        tempColor = glowEffect.color;
        if (Utility.IsDefined(Manager.me.player))
        {
            player = Manager.me.player.transform;
            targetPos = (Manager.me.player.transform.position + (player.transform.right.normalized * playerBuffer));
            //GetComponent<UnityEngine.AI.NavMeshAgent>().destination = Manager.me.player.transform.position;
        }
    }

	void Update () {
       




        if (detonateTimer <= 0)
        {
            Detonate();
        }
       

    }


    void FixedUpdate(){

            transform.eulerAngles = new Vector3(0f,0f,Geo.ToAng(transform.position, targetPos));
        if(((Vector2)transform.position - targetPos).magnitude > inZone && !wall)
        {
            rb.MovePosition(transform.position + transform.right * runSpeed * Time.fixedDeltaTime);
            arrived = true;

        }
        else
        {
            detonateTimer -= Time.fixedDeltaTime;
            tempColor.a = 1 - Mathf.Pow(detonateTimer, 2);
            glowEffect.color = tempColor;
        }

    }


    void Hit(){
        Destroy(gameObject);
        Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));
        if (Utility.IsDefined(hurtSound))
        {
            SoundManager.me.Play(hurtSound);
        }
        Manager.me.score += 10 * Manager.me.multiplier;
        Manager.me.multiplier++;
        Manager.me.multiTime = 4f;
        Manager.me.numEnemiesOnScreen--;
        Manager.me.activeEnemies.Remove(gameObject);



    }

    void Detonate()
    {
        if (Utility.IsDefined(explosionSound))
            SoundManager.me.Play(explosionSound);
        Instantiate(explotionEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(this.gameObject);
        Manager.me.score++;
        Manager.me.numEnemiesOnScreen--;
        Manager.me.activeEnemies.Remove(gameObject);

    }

    void OnDestroy(){

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            wall = true;
        }
    }

}