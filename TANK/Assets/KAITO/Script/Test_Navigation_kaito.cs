using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Test_Navigation_kaito : MonoBehaviour {

    GameObject target;

    NavMeshAgent myNavMeshAgent;

    Rigidbody myRigidbody;

    Vector3 maxRange;           //ランダムで移動させる時の一番右上の座標を格納するための変数
    Vector3 minRange;           //ランダムで移動させる時の一番左下の座標を格納するための変数

    // Use this for initialization
    void Start () {

        target = GameObject.FindGameObjectWithTag("Player");

        myNavMeshAgent = GetComponent<NavMeshAgent>();
        myNavMeshAgent.SetDestination(target.transform.position);

         myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.useGravity = false;
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;


        GameObject walls = GameObject.Find("Walls");
        foreach (Transform child in walls.transform)
        {
            //Debug.Log( child.gameObject.name);
            if (child.position.x < 0)
                minRange.x = child.position.x;
            else if (child.position.x > 0)
                maxRange.x = child.position.x;
            else if (child.position.z < 0)
                minRange.z = child.position.z;
            else if (child.position.z > 0)
                maxRange.z = child.position.z;


        }
        //Debug.Log(minRange);
        //Debug.Log(maxRange);
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            myNavMeshAgent.SetDestination(target.transform.position);
        }

        myRigidbody.velocity = Vector3.zero;
        
	}



    private Vector3 GetRandomPosition()
    {

        return Vector3.zero;



    }

}
