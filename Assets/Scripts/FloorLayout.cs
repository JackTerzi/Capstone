using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLayout : MonoBehaviour {
    Camera cam;
    public GameObject square;

    float startX, startY, xIncrement, yIncrement;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        xIncrement = Screen.width / 5f;
        yIncrement = Screen.height / 9f;
        startX = Screen.width / 9.5f;
        startY = Screen.height / 18;

        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(square, cam.ScreenToWorldPoint(new Vector3(startX + xIncrement * i, startY + yIncrement * j, 1)), Quaternion.identity);
            }
        }


    }

    // Update is called once per frame
    void Update () {
	}
}
