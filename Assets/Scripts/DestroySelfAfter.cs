using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfter : MonoBehaviour {
    float timer;
    public float destroyTimer;
    public bool fadeOut, isText, isSprite;
    SpriteRenderer spr;
    TextMesh tm;
	// Use this for initialization
	void Start () {
        if (isText)
        {
            tm = GetComponent<TextMesh>();
        }else if (isSprite)
        {
            spr = GetComponent<SpriteRenderer>();

        }
        timer = destroyTimer;
	}
	
	// Update is called once per frame
	void Update () {
        destroyTimer -= Time.deltaTime;

        if (fadeOut)
        {
            if (isText)
            {
                tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, destroyTimer / timer);

            }else if (isSprite)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, destroyTimer / timer);

            }
        }
        else
        {
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
