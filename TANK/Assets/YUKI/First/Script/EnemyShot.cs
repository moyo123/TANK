using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public Transform turret;
    public Transform muzzle;
    public GameObject bulletPrefab;

    bool ShotFlg = false;

    private float attackInterval = 2f;
    private float turretRotationSmooth = 30f;
    private float lastAttackTime;

    private Transform player;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {

        // 始めにプレイヤーの位置を取得できるようにする
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        // 砲台をプレイヤーの方向に向ける
        Quaternion targetRotation = Quaternion.LookRotation(player.position - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * turretRotationSmooth);
        ray = new Ray(new Vector3(muzzle.position.x, 1, muzzle.position.z)
                        , new Vector3(player.position.x - muzzle.position.x
                        , 1
                        , player.position.z - muzzle.position.z));

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0);

        // 一定間隔で弾丸を発射する
        if (Physics.Raycast(ray, out hit, 20f)  && Time.time > lastAttackTime + attackInterval && hit.collider.tag == player.tag)
        {
            ShotFlg = true;
            Debug.Log(hit.collider.tag);
        }
        if (ShotFlg == true)
        {
            Debug.Log("hit");
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            lastAttackTime = Time.time;

            ShotFlg = false;
        }

    }
}