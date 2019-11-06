using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour {

    float Speed = 20f;
    Rigidbody ShellRb;

	// Use this for initialization
	void Start () {
        ShellRb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ShellRb.AddForce(transform.forward * Speed);
	}
}
