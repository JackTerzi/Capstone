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

	}
}
