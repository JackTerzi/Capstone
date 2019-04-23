using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour{

    Animator movementAnimator;

	Rigidbody2D rb;

	Vector2 startPos,
		moveDirection,
		previousMoveDirection;

    int frameCount;

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

    float 
		lookAngle,
		previousAngle;

    public GameObject bullet,
                      fire,
                      deathEffect;

    public AudioClip walkSound, runSound;

	void Start (){
		rb = GetComponent<Rigidbody2D> ();
        movementAnimator = GetComponent<Animator>();
		previousMoveDirection = Vector3.zero;

	}
	

	void Update (){
		if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);
            frameCount++;                       //increment frame 
            switch (touch.phase) {

			case TouchPhase.Began:
				
				startPos = (touch.position);
				
				break;
			
			case TouchPhase.Stationary:
                    if (frameCount > 10)
                    {
                        lookAngle = Geo.ToAng(startPos, touch.position);
                    }
                    break;
			
			case TouchPhase.Moved:
			        if (Mathf.Abs (lookAngle - previousAngle) < 160 && (touch.position - startPos).magnitude > 4) {
				        moveDirection = (touch.position) - startPos;
                        lookAngle = Geo.ToAng(startPos, (touch.position));
                    }
                    else{
                       
                    }
                    if(frameCount > 10){
                        lookAngle = Geo.ToAng(startPos, (touch.position));
                    }

				break;

			case TouchPhase.Ended:
                    if(((touch.position) - startPos).magnitude < 8){
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
        if (Manager.me.playerShouldDash){
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
            rb.MovePosition (transform.position +  transform.right * speed * Time.fixedDeltaTime);
        }

	}


    void AnimCheck(){
        if (speed <= 0)
        {
            speed = 0;
            movementAnimator.SetBool("isIdle", true);
        }
        else
        {
            if (Utility.IsDefined(walkSound))
            {
                SoundManager.me.Play(walkSound);

            }
            movementAnimator.SetBool("isIdle", false);
        }

        if (speed > runAnimSpeed)
        {
            if (Utility.IsDefined(runSound))
            {
                SoundManager.me.Play(runSound);

            }

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
        Instantiate(deathEffect, transform.position, Quaternion.Euler(-90, 0, 0));
        Manager.me.isGameOver = true;
    }

    //Vector3 Vector2to3(Vector2 ourVec)
    //{
    //    Vector3 newVec;
    //    newVec = new Vector3(ourVec.x, 0, ourVec.y);
    //    return newVec;
    //}
}
