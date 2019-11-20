using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 5f;

    private float rotationSmooth = 3f;
    private float levelSize = 10f;

    private Vector3 targetPosition;

    private float changeTargetSqrDistance = 10f;

    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        targetPosition = GetRandomPositionOnLevel();
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward*1);
       // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0);
       if(Physics.Raycast(ray, out hit, 0.5f))
        {
            targetPosition = GetRandomPositionOnLevel();
        }

        // 目標地点との距離が小さければ、次のランダムな目標地点を設定する
        float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);
        if (sqrDistanceToTarget < changeTargetSqrDistance)
        {
            targetPosition = GetRandomPositionOnLevel();
        }

        // 目標地点の方向を向く
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);

        // 前方に進む
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            targetPosition = GetRandomPositionOnLevel();
        }
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
    }
}