using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Image[] tiles;


	public int tileXStep,
			   tileYStep;


	void SetTiles(){
		int x = 0, y = 0;
		foreach (Image i in tiles){
			i.rectTransform.position = new Vector3((-1 * tileXStep) + (x * tileXStep), tileYStep - (y * tileYStep), 0);
			Debug.Log("x " + ((-1 * tileXStep) + (x * tileXStep)));
			Debug.Log("y " + (tileYStep - (y * tileYStep)));
			//Debug.Log(x + " " + y);
			x++;
			if (x == 3){
				x = 0;
				y++;
			}
			if (y == 3){
				Debug.Log("tile positions set");
			}

		}
	}



	void Start () {
		SetTiles();
	}
	

	void Update () {
		
	}
}
