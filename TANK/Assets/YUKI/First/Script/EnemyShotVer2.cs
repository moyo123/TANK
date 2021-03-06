﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotVer2 : MonoBehaviour {

    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject bulletPrefab;

    Ray ray;
    RaycastHit hit;

    private float attackInterval = 2f;
    private float turretRotationSmooth = 30f;
    private float lastAttackTime;

    private float lookRotation = 30.0f;
    private float rotateSpeed = 10.0f;
    // Use this for initialization
    void Start () {
        Debug.Log(transform.eulerAngles.y + lookRotation);
        lookRotation = transform.eulerAngles.y + lookRotation;
        turret = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        // 砲台をプレイヤーの方向に向ける
        Debug.Log(Mathf.Ceil(turret.transform.eulerAngles.y) + Mathf.Ceil(transform.eulerAngles.y) + "::" + (Mathf.Ceil(transform.eulerAngles.y)+360f) + Mathf.Ceil(lookRotation));


        if (Mathf.Ceil(turret.transform.eulerAngles.y) + Mathf.Ceil(transform.eulerAngles.y) != Mathf.Ceil(transform.eulerAngles.y) + Mathf.Ceil(lookRotation))
        {
            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + lookRotation, 0)), rotateSpeed * Time.deltaTime);
        }
        else if(Mathf.Ceil(turret.transform.eulerAngles.y) + Mathf.Ceil(transform.eulerAngles.y) == Mathf.Ceil(transform.eulerAngles.y) + Mathf.Ceil(lookRotation)
                || Mathf.Ceil(turret.transform.eulerAngles.y) + Mathf.Ceil(transform.eulerAngles.y) == (Mathf.Ceil(transform.eulerAngles.y) + 360f) + Mathf.Ceil(lookRotation))
        {
            lookRotation *= -1f;
        }

        ray = new Ray(turret.transform.position , muzzle.transform.position - turret.transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 0);
    }
}
