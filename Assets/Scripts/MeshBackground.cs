using System.Collections;
using Boo.Lang;
using UnityEngine;

public class MeshBackground : MonoBehaviour
{

	
	public GameObject tile;
	
	private System.Collections.Generic.List<Tile> tiles;
	private int cameraHeight;

	private int cameraWidth;
	// Use this for initialization
	void Start ()
	{
		tiles = new System.Collections.Generic.List<Tile>();
		cameraHeight= (int)Camera.main.orthographicSize;
		cameraWidth = cameraHeight * 2;
		Initialize();
	}

	void Initialize()
	{
		for (int i = 0; i <= cameraWidth; i++)
		{
			for (int j = 0; j <= cameraHeight; j++)
			{
				GameObject newTile = Instantiate(tile, new Vector3(j - ((float)cameraHeight/2f), i - ((float)cameraHeight/2f + 1), 0), Quaternion.identity);
				newTile.transform.localScale = Vector3.zero;
				Tile tileScript = newTile.GetComponent<Tile>();
				tileScript.color = new Color((float) i / (float)cameraHeight, (float) j / (float)cameraWidth, 1) * 0.25f;
				tileScript.index = i * cameraHeight + j;
				tileScript.Initialize();
				tiles.Add(tileScript);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
