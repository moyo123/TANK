using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromtCheck : MonoBehaviour {

    private float rotationSmooth = 3f;

    [SerializeField]
    private float levelSize;
    private Vector3 targetPosition;
    public Vector3 getTargetPosition { get { return targetPosition; } }

    private bool InstanceFlg = false;
    public bool instanceflg { get { return InstanceFlg; } set { InstanceFlg = value; } }

    private float changeTargetSqrDistance = 10f;

    Rigidbody rigidbody;

    [SerializeField]
    public GameObject Target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        targetPosition = GetRandomPositionOnLevel();
        Instantiate(Target, targetPosition, Quaternion.identity);
        InstanceFlg = true;
    }

    private void Update()
    {
        rigidbody.velocity = Vector3.zero;

        float sqrDistanceToTarget = Vector3.SqrMagnitude(transform.position - targetPosition);
        if (sqrDistanceToTarget < changeTargetSqrDistance)
        {
            Destroy(GameObject.FindGameObjectWithTag("Target"));

            targetPosition = GetRandomPositionOnLevel();
            Instantiate(Target, targetPosition,Quaternion.identity);
            InstanceFlg = true;
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag != "Floor")
    //    {
    //        Destroy(Target);

    //        Instantiate(Target, targetPosition, Quaternion.identity);
    //    }
    //}

    public Vector3 GetRandomPositionOnLevel()
    {
        return new Vector3(Random.Range(-levelSize, levelSize), 0, Random.Range(-levelSize, levelSize));
    }

}
