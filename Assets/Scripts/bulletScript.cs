using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour {
    Rigidbody2D rb;
    public float bulletSpeed;


	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate () {
        rb.MovePosition(transform.position + (transform.right * bulletSpeed * Time.fixedDeltaTime));

       
    }
    void Reverse()
    {

        gameObject.layer = LayerMask.NameToLayer("PlayerProjectiles");
        bulletSpeed = -bulletSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        
        if(collision.gameObject.layer != LayerMask.NameToLayer("Walls") && collision.gameObject.layer != LayerMask.NameToLayer("Bashable"))
        {
            collision.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);

            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}
