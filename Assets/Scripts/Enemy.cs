﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy {

    int health,
        speed;

    float randomShoot;

    bool canShoot;

    Rigidbody2D enem;

    GameObject player;



    public Enemy (Rigidbody2D me, int health, int speed, bool canShoot){
        this.health = health;
        this.speed = speed;
        this.enem = me;
        this.randomShoot = Time.time + Random.Range(2f, 6f);
        this.canShoot = canShoot;
        Manager.me.numEnemiesOnScreen++;
        player = Manager.me.player;

    }


    public Enemy(Rigidbody2D me, int health, int speed){
        this.health = health;
        this.speed = speed;
        this.enem = me;
        this.randomShoot = Time.time + Random.Range(2f, 6f);
        Manager.me.numEnemiesOnScreen++;
        player = Manager.me.player;

    }


    public void Movement(){
        this.enem.transform.right = player.transform.position - this.enem.transform.position;
    }


    public bool Shoot(){
        if (canShoot && Time.time > randomShoot){
            randomShoot = Time.time + Random.Range(1.5f, 4f);
            return true;
        }

        else{
            return false;
        }
    }

}
