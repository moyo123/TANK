using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotVer2 : MonoBehaviour {

    public Transform turret;
    public Transform muzzle;
    public GameObject bulletPrefab;

    Ray ray;
    RaycastHit hit;

    private float attackInterval = 2f;
    private float turretRotationSmooth = 30f;
    private float lastAttackTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 砲台をプレイヤーの方向に向ける
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Random.Range(-50,50),turret.position.y,turret.position.z) - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * turretRotationSmooth);


        ray = new Ray(turret.position , muzzle.position - turret.position);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 0);

    }
}
