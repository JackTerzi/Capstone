using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {
    float liveTimer;
	// Use this for initialization
	void Start () {
        liveTimer = .2f;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isActiveAndEnabled)
        {
            liveTimer -= Time.deltaTime;
            if (liveTimer < 0)
            {
                liveTimer = .2f;
                this.gameObject.SetActive(false);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag != "Player")
        {
            col.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
            col.SendMessage("Reverse", SendMessageOptions.DontRequireReceiver);
        }

    }
}
