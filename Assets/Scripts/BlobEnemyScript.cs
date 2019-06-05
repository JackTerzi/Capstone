using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobEnemyScript : MonoBehaviour {
    [Header("Chasing Player")]
    Rigidbody2D rb;
    bool trueIsTowards, changeDir;
     float startSpeed, changeDirTimer;
    public float speed, drag;

    Vector2 dir;
    [Header("Chasing Enemy")]
    bool enemyTargeted;

    void Start () {
        startSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Manager.me.player != null && !enemyTargeted)
        {
            Movement();
        }
        /*Keep Track of player's position and default to move like the other enemies do. 
		 * if there's another enemy on screen target the closest one.
		 * eat the enemy on contact
		 * grow in size with each enemy        
		 * if you eat enough enemies (3? ) then do an attack        
		 * Don't eat laser guys maybe?
		 * Remove from enemy Bag
		 */
    }


    private void FixedUpdate()
    {

        rb.MovePosition((Vector2)gameObject.transform.position + (dir.normalized * speed * Time.smoothDeltaTime));

    }






    void Movement() //Moves in a random direction unless it has a target or if player is close to it
    {
        changeDirTimer-= Time.deltaTime;

        if(changeDirTimer <= 0 || changeDir)
        {
            speed -= drag;
            if (speed <= 0)
            {
                dir = Random.insideUnitCircle;
                changeDirTimer = 1f;
                changeDir = false;
                speed = startSpeed;
            }
        }
        else
        {
            speed = startSpeed;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            changeDir = true;
            speed = 0;

        }
    }

   
}
