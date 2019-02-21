using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Image[] tiles;
	string[] levels = {"1-1 Tutorial", "1-2"};


	public int tileXStep,
			   tileYStep,
			   centerXOffset,
			   centerYOffset;


	void SetTiles(){
		int x = 0, y = 0;
		foreach (Image i in tiles){
			i.rectTransform.anchoredPosition = new Vector3((-1 * tileXStep) + (x * tileXStep) + centerXOffset, tileYStep - (y * tileYStep) + centerYOffset, 0);
			//Debug.Log(x + " " + y);
			x++;
			if (x == 3){
				x = 0;
				y++;
			}
			if (y == 3){
				Debug.Log("tile positions set");
			};

		}
	}



	void Start () {
		SetTiles();
		bool tilesMatchLevels = NumLevelsSameAsNumTiles();
		if (!tilesMatchLevels){
			Debug.LogError("Number of levels not equal to number of tiles");
		}
	}
	

	void Update () {
		
	}


	bool NumLevelsSameAsNumTiles(){
		if (tiles.Length != levels.Length){
			return true;
		}
		else{
			return false;
		}
	}


}
