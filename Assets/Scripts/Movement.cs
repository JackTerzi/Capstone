﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    Animator movementAnimator;
	Rigidbody2D rb;

	Vector2 startPos,
		moveDirection,
		previousMoveDirection;

    int frameCount;

	float 
		lookAngle,
		previousAngle;

	bool addVelocity;

	public float speed,
		sameDirCheck,
		maxSpeed,
		addedSpeed,
		dragMagnitude,
        runAnimSpeed,
        walkAnimSpeed,
        numBullets,
        shotgunOffset;

    public GameObject bullet,
                      fire;


	void Start (){
		rb = GetComponent<Rigidbody2D> ();
        movementAnimator = GetComponent<Animator>();
		previousMoveDirection = Vector2.zero;

	}
	

	void Update (){
		if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);
            frameCount++;                       //increment frame 
            switch (touch.phase) {

			case TouchPhase.Began:
				
				startPos = touch.position;
				
				break;
			
			case TouchPhase.Stationary:
                    if (frameCount > 10)
                    {
                        lookAngle = Geo.ToAng(startPos, touch.position);
                    }
                    break;
			
			case TouchPhase.Moved:
			        if (Mathf.Abs (lookAngle - previousAngle) < 160 && (touch.position - startPos).magnitude > 4) {
				        moveDirection = touch.position - startPos;
                        lookAngle = Geo.ToAng(startPos, touch.position);
                    }
                    else{
                       
                    }
                    if(frameCount > 10){
                        lookAngle = Geo.ToAng(startPos, touch.position);
                    }

				break;

			case TouchPhase.Ended:
                    if((touch.position - startPos).magnitude < 8){
                        Shoot();
                    }
                    if (frameCount <10 && (moveDirection.normalized - previousMoveDirection.normalized).magnitude < sameDirCheck && (moveDirection - previousMoveDirection).magnitude > 1) {
    					addVelocity = true;
                        previousMoveDirection = moveDirection;
                    }
    				previousAngle = lookAngle;
                    frameCount = 0;                                 ///sets frames to 0 
				break;

			}

		}
	}



	void FixedUpdate (){
        float drag;
        if (speed > 1)
            drag = dragMagnitude * (speed) * Time.fixedDeltaTime;
        else
            drag = dragMagnitude;
		if (addVelocity && speed < maxSpeed) {
            //Manager.me.playerSwiped = true;
			speed += addedSpeed;
			addVelocity = false;
		} else {
            Manager.me.playerSwiped = false;
			speed -= drag;

		}
		if (speed > maxSpeed) {
			speed = maxSpeed;
		}

        AnimCheck();
        rb.MoveRotation(Mathf.LerpAngle(Geo.ToAng(transform.right), lookAngle, .35f));
        rb.MovePosition ((Vector2)transform.position + (Vector2) transform.right * speed * Time.fixedDeltaTime);
	}


    void AnimCheck(){
        if (speed <= 0)
        {
            speed = 0;
            movementAnimator.SetBool("isIdle", true);
        }
        else
        {
            movementAnimator.SetBool("isIdle", false);
        }

        if (speed > runAnimSpeed)
        {
            movementAnimator.SetBool("isRunning", true);
            if (Manager.me.playerShouldDash)
                fire.SetActive(true);
        }
        else if (speed > walkAnimSpeed)
        {
            if(Manager.me.playerShouldDash)
                fire.SetActive(false);
            movementAnimator.SetBool("isRunning", false);
            movementAnimator.SetBool("isWalking", true);

        }
        else
        {   
            if (Manager.me.playerShouldDash)
                fire.SetActive(false);
            movementAnimator.SetBool("isWalking", false);
            movementAnimator.SetBool("isRunning", false);
            movementAnimator.SetBool("isIdle", true);

        }
    }



    void Shoot(){

        if (Manager.me.playerShouldShoot){
            addVelocity = true;

            float bulletSpawnAng,
            bulletSpawnPos;
            movementAnimator.SetBool("isShooting", true);
            for (int i = 0; i < numBullets; i++){

                float power = Mathf.Pow(-1, i);
                bulletSpawnAng = (i / numBullets) * 20f * power;
                bulletSpawnPos = (i / numBullets) * .3f * power;
                Instantiate(bullet, transform.position + gameObject.transform.right.normalized * shotgunOffset + Geo.PerpVect(gameObject.transform.right, true) * bulletSpawnPos, Quaternion.Euler(0, 0, Geo.ToAng(gameObject.transform.right) - bulletSpawnAng));
            }

            movementAnimator.SetBool("isShooting", false);
        }
    
     }


    void OnDestroy(){
        Manager.me.isGameOver = true;
    }


}
