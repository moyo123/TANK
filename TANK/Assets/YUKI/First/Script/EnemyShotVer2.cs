using System.Collections;
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
        turret = transform.GetChild(0).gameObject.GetComponent<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        // 砲台をプレイヤーの方向に向ける
        //Quaternion targetRotation = Quaternion.LookRotation(new Vector3(this.transform.localEulerAngles.x + 1.0f, 0, 0));
        //turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * turretRotationSmooth);

        if (Mathf.Ceil(turret.transform.eulerAngles.y) + Mathf.Ceil(transform.eulerAngles.y) != Mathf.Ceil(transform.eulerAngles.y) + Mathf.Ceil(lookRotation))
        {
            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + lookRotation, 0)), rotateSpeed * Time.deltaTime);
            Debug.Log("iffffffffffffffffff");
        }
        else//if(turret.transform.eulerAngles.y + transform.eulerAngles.y == transform.eulerAngles.y + lookRotation)
        {
            //lookRotation *= -1;
            Debug.Log("elseだよー");
        }

        Debug.Log("aa");
        ray = new Ray(turret.transform.position , muzzle.transform.position - turret.transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 0);
    }
}
