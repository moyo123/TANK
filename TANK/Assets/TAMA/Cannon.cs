using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    Plane plane = new Plane();
    float distance = 0;
    float duration = 10;
    public int layerMask;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit raycastHit;

        // カメラとマウスの位置を元にRayを準備
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        //plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        //if (plane.Raycast(ray, out distance))
        //{
        //    // 距離を元に交点を算出して、交点の方を向く
        //    var lookPoint = ray.GetPoint(distance);
        //    transform.LookAt(lookPoint);
        //    Debug.Log(lookPoint.z);
        //    //Debug.Log(lookPoint);

        //    //transform.LookAt(new Vector3(0, 0, lookPoint.z));
        //}
        if (Physics.Raycast(ray, out raycastHit, 100))
        {
            Quaternion MousePos = Quaternion.LookRotation(raycastHit.point - transform.position, Vector3.up);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, MousePos, 0.1f);

            //Debug.Log(lookPoint);

            //transform.LookAt(new Vector3(0, 0, lookPoint.z));
        }

    }
}
