using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    Animator movement;
	Rigidbody2D rb;

	Vector2 startPos,
		moveDirection,
		endpos,
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
    public GameObject bullet;
    public GameObject fire;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
        movement = GetComponent<Animator>();
		previousMoveDirection = Vector2.zero;

	}
	
	// Update is called once per frame
	void Update ()
	{
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
                        Shoot();
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
                        Shoot();
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

			//Debug.Log (message);
		}
	}



	void FixedUpdate ()
	{
        float drag;
        if (speed > 1)
            drag = dragMagnitude * (speed) * Time.fixedDeltaTime;
        else
            drag = dragMagnitude;
		if (addVelocity && speed < maxSpeed) {
			speed += addedSpeed;
			addVelocity = false;
		} else {
			speed -= drag;

		}
		if (speed > maxSpeed) {
			speed = maxSpeed;
		}

        AnimCheck();
        rb.MoveRotation(Mathf.LerpAngle(Geo.ToAng(transform.right), lookAngle, .35f));
       // rb.MoveRotation ( Geo.ToAng(transform.right) - ((Geo.ToAng(transform.right)-lookAngle)*5)*Time.fixedDeltaTime);
        rb.MovePosition ((Vector2)transform.position  + (Vector2) transform.right *speed * Time.fixedDeltaTime);
	}


    void AnimCheck(){
        if (speed <= 0)
        {
            speed = 0;
            movement.SetBool("isIdle", true);
        }
        else
        {
            movement.SetBool("isIdle", false);
        }

        if (speed > runAnimSpeed)
        {
            movement.SetBool("isRunning", true);
            if (Manager.me.shouldDash)
                fire.SetActive(true);
        }
        else if (speed > walkAnimSpeed)
        {
            if(Manager.me.shouldDash)
                fire.SetActive(false);
            movement.SetBool("isRunning", false);
            movement.SetBool("isWalking", true);

        }
        else
        {   
            if (Manager.me.shouldDash)
                fire.SetActive(false);
            movement.SetBool("isWalking", false);
            movement.SetBool("isRunning", false);
            movement.SetBool("isIdle", true);

        }
    }



    void Shoot(){
        //  GameObject myBullet = Instantiate(bullet,transform.position,Quaternion.identity);

        //  myBullet.transform.right = gameObject.transform.right;
        //  myBullet.transform.parent = gameObject.transform;
        if (Manager.me.shouldShoot)
        {
            addVelocity = true;

            float bulletSpawnAng,
             bulletSpawnPos;
            movement.SetBool("isShooting", true);
            for (int i = 0; i < numBullets; i++)
            {
                //bullSpawnAng [i] = (i / numBullets) * 30*((-1)^i);
                //bullSpawnPos [i] = (i / numBullets) * 50f*((-1)^i);
                float power = Mathf.Pow(-1, i);
                bulletSpawnAng = (i / numBullets) * 20f * power;
                bulletSpawnPos = (i / numBullets) * .3f * power;
                Instantiate(bullet, transform.position + gameObject.transform.right.normalized * shotgunOffset + Geo.PerpVect(gameObject.transform.right, true) * bulletSpawnPos, Quaternion.Euler(0, 0, Geo.ToAng(gameObject.transform.right) - bulletSpawnAng));
            }
            // SoundManager.me.Play(shootingSound);
            // ManagerScript.me.bullets -= 1;
            // ManagerScript.me.screenShakeTimer = shootShaking;
            // bulletVisHolder.SendMessage("Shot", null, SendMessageOptions.DontRequireReceiver);
            movement.SetBool("isShooting", false);

            //if (ManagerScript.me.bullets <= 0)
            //{
            //    reloading = true;
            //    SoundManager.me.Play(reloadingSound);
            //    shootDelay = setShootDelay;

            //}
        }
    
     }



    private void OnDestroy()
    {
        Manager.me.gameOver = true;
    }




  
}
