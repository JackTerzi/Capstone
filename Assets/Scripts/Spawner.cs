using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy1, enemy2, enemy3, enemy4, enemy5;

    public float spawnTime;
    float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		if(timer >= spawnTime)
        {
            Instantiate(enemy1, new Vector3(Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x), Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).y, Camera.main.ViewportToWorldPoint(Vector3.one).y)),Quaternion.identity);
            timer = 0;
        }
    }
}
