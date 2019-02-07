using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour {
    Rigidbody2D rb;
    public float bulletSpeed,
                bulletAccel,
                bulletStop;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.MovePosition(transform.position + (transform.right * bulletSpeed * Time.fixedDeltaTime));
        bulletSpeed -= bulletAccel;
        if (bulletSpeed <= bulletStop)
        {
            Destroy(gameObject);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);                     
        }
        
        if(collision.gameObject.layer != LayerMask.NameToLayer("Walls") && collision.gameObject.layer != LayerMask.NameToLayer("Bashable"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}
