using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {

	public Transform target;


	void Start () {
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
			}
	

	void Update () {
		if (Mathf.Abs(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position )) < .1f){
			GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
			Debug.Log("close enough");
		}
		else{
			Debug.Log("not close enough");
			GetComponent<UnityEngine.AI.NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
		}
	}
}
