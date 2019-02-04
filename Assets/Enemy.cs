using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    int health,
    speed;
    float randomShoot;
    bool canShoot;
    Rigidbody2D enem;
    GameObject player;



    public Enemy ( Rigidbody2D me, int h, int s, bool cS){
        this.health = h;
        this.speed = s;
        this.enem = me;
        this.randomShoot = Time.time + Random.Range(2f, 6f);
        this.canShoot = cS;
        Manager.me.enemiesOnScreen++;

        player = Manager.me.player;

    }
    public Enemy(Rigidbody2D me, int h, int s)
    {
        this.health = h;
        this.speed = s;
        this.enem = me;
        this.randomShoot = Time.time + Random.Range(2f, 6f);
        Manager.me.enemiesOnScreen++;
        player = Manager.me.player;

    }

    public void Movement(){
        this.enem.transform.right = player.transform.position - this.enem.transform.position;
    }

    public bool Shoot(){

       

            if (canShoot && Time.time > randomShoot)
            {

                randomShoot = Time.time + Random.Range(1.5f, 4f);
                return true;

            }
            else
            {
                return false;
            }



    }



   


}
