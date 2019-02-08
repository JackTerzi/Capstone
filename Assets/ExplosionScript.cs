using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float maxSize;
    public float growSpeed;
   
    // Use this for initialization
    void Awake()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale = new Vector3(transform.localScale.x + growSpeed, transform.localScale.y + growSpeed, 1);

          

        }
        else
        {
         
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (LayerMask.LayerToName(col.gameObject.layer) == "Enemy")
        {
            col.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
        }
    }
}
