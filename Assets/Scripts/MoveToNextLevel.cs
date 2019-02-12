using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour {


	//BoxCollider2D boxCol;
	public Object scene;
	public string nextSceneName;

	
	void Awake(){
		//boxCol = GetComponent<BoxCollider2D>();

	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			SceneManager.LoadScene(nextSceneName.ToString());
		}
	}

}
