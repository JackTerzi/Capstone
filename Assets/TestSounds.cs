using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSounds : MonoBehaviour {

	public AudioClip clip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SoundManager.me.Play (clip);
		}
	}
}
