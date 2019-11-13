using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Test_Navigation_kaito : MonoBehaviour {

    [SerializeField]
    GameObject target;

    NavMeshAgent myNavMeshAgent;


	// Use this for initialization
	void Start () {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.SetDestination(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            myNavMeshAgent.SetDestination(target.transform.position);
        }
		
	}
}
