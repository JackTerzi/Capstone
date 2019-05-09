using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	public Color color;
	public float index;
	private SpriteRenderer s;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		s.color = Color.Lerp(color, color + Color.white * 0.1f, Mathf.PingPong(Mathf.Sin(Time.time + index/100f), 1f));
	}


	public void Initialize()
	{
		s = GetComponent<SpriteRenderer>();
		s.color = color;
		StartCoroutine(Scale());
	}
	
	IEnumerator Scale()
	{
	
		float t = 0;
		while (t < 1)
		{
			transform.up = Vector3.Lerp(Vector3.right, Vector3.up, t);
			transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.95f, t);
			t += Time.deltaTime * (index/100f + 1f);
			yield return null;
		}

		
		transform.localScale = Vector3.one * 0.95f;
		
		//
		FlipTile();
	}
	
	
	//call me on new level
	public void FlipTile()
	{
		StartCoroutine(Flip());
	}
	IEnumerator Flip()
	{
		float t = 0;
		while (t < 1)
		{
			transform.forward = Vector3.Lerp(Vector3.forward, Vector3.right, Mathf.PingPong(t * 2, 1));
			t += Time.deltaTime * (index/100f + 1f)/2f;
			yield return null;
		}
	}
}
