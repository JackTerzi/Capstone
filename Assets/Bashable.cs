using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bashable : MonoBehaviour {
    public float speed;
    Vector2 dir;
    bool beenHit;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(beenHit){
            rb.MovePosition((Vector2)transform.position + (Vector2)dir * speed * Time.fixedDeltaTime);
        }
	}

    void Hit(){
        if(!beenHit){
            dir =   transform.position - Manager.me.player.transform.position;
            dir = dir.normalized;
            beenHit = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Walls")){
            Destroy(gameObject);

        }
        if(beenHit){
            collision.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
