using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour{

    Animator movementAnimator;

	Rigidbody rb;

	Vector3 startPos,
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

    float lookAngle,
		  previousAngle;

    public GameObject bullet,
                      fire;


	void Start (){
		rb = GetComponent<Rigidbody> ();
        movementAnimator = GetComponent<Animator>();
		previousMoveDirection = Vector3.zero;

        Vector3 newVec = Vector2to3(new Vector2(2,1));
        Debug.Log(newVec);

	}
	

	void Update (){
		if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);
            frameCount++;                       //increment frame 
            switch (touch.phase) {

			case TouchPhase.Began:
				
				startPos = Vector2to3(touch.position);
                    //Debug.Log(startPos);
				
				break;
			
			case TouchPhase.Stationary:
                    if (frameCount > 10)
                    {
                        lookAngle = Geo.ToAng3(startPos, Vector2to3(touch.position));
                        //Debug.Log(lookAngle);
                    }
                    break;
			
			case TouchPhase.Moved:
			        if (Mathf.Abs (lookAngle - previousAngle) < 160 && (Vector2to3(touch.position) - startPos).magnitude > 4) {
				        moveDirection = Vector2to3(touch.position) - startPos;
                        lookAngle = Geo.ToAng3(startPos, Vector2to3(touch.position));
                    }
                    else{
                       
                    }
                    if(frameCount > 10){
                        lookAngle = Geo.ToAng3(startPos, Vector2to3(touch.position));
                    }

				break;

			case TouchPhase.Ended:
                    if((Vector2to3(touch.position) - startPos).magnitude < 8){
                        Shoot();
                    }
                    if (frameCount <10 && (moveDirection.normalized - previousMoveDirection.normalized).magnitude < sameDirCheck && (moveDirection - previousMoveDirection).magnitude > 1) {
    					//Debug.Log("vroom vroom");
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
        //Debug.Log(speed);
        if (Manager.me.playerShouldDash){
            float drag;
            if (speed > 1)
                drag = dragMagnitude * (speed) * Time.fixedDeltaTime;
            else
                drag = dragMagnitude;
            if (addVelocity && speed < maxSpeed) {
                //Debug.Log("we goin fast");
                speed += addedSpeed;
                addVelocity = false;
            } 
            else {
                Manager.me.playerSwiped = false;
                speed -= drag;

            }
            if (speed > maxSpeed) {
                speed = maxSpeed;
            }

                AnimCheck();
                //rb.MoveRotation(Mathf.LerpAngle(Geo.ToAng3(transform.up), lookAngle, .35f));
                transform.eulerAngles = new Vector3(0, lookAngle, 0);
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
                Instantiate(bullet, transform.position + gameObject.transform.up.normalized * shotgunOffset + Geo.PerpVect(gameObject.transform.up, true) * bulletSpawnPos, Quaternion.Euler(0, 0, Geo.ToAng3(gameObject.transform.up) - bulletSpawnAng));
            }

            movementAnimator.SetBool("isShooting", false);
        }
    
     }


    void OnDestroy(){
        Manager.me.isGameOver = true;
    }

    Vector3 Vector2to3(Vector2 vec2)
    {
        return new Vector3(vec2.x, 0, vec2.y);
    }
}
