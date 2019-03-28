using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
           //Destroy(col.gameObject);
            Manager.me.isGameOver = true;
            Destroy(col.gameObject);
        }
        if (LayerMask.LayerToName(col.gameObject.layer) == "Enemy")
        {
            col.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
        }
        
    }
}
