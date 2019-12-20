using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_kaito : EnemyBase
{

    const int lookAroundNum = 12;

    // Use this for initialization
    void Start()
    {
        Initialize();                    //最初に初期化したりいろいろ取得したりするやつ
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }




    protected override void Shot()
    {
        if (shotIntervalCount >= shotIntervalTime)
        {
            for (int i = 0; i < myStatusData.MAX_NUMBER_OF_SHOTS; i++)
            {
                if (currentBullet[i] == null)
                {
                    RaycastHit hit;
                    if (Physics.BoxCast(Turret.transform.position, bulletPrefab.transform.localScale / 2, Turret.transform.forward, out hit, Quaternion.identity, 100))
                    {
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            currentBullet[i] = Instantiate(bulletPrefab);
                            currentBullet[i].transform.position = Turret.transform.position + Turret.transform.forward * 1f;
                            currentBullet[i].GetComponent<MoveBullet_kaito>().Initialize(Turret.transform.forward, myStatusData.BULLET_MOVE_SPEED, myStatusData.MAX_NUMBER_OF_REFLECTION, this.gameObject.tag);

                            shotIntervalCount = 0;

                            break;


                        }

                        for (int angle = lookAroundNum; angle < 360; angle += lookAroundNum)
                        {

                            float atan2 = Mathf.Atan2(Turret.transform.forward.y, Turret.transform.forward.x);

                            float addAtan2 = atan2 + angle * Mathf.Deg2Rad;

                            float cos = Mathf.Cos(addAtan2);
                            float sin = Mathf.Sin(addAtan2);

                            Vector3 newVector = new Vector3(cos, 0, sin);
                            //Debug.DrawRay(Turret.transform.position, newVector * 30, Color.red);




                            Physics.BoxCast(Turret.transform.position, bulletPrefab.transform.localScale / 2, newVector, out hit, Quaternion.identity, 100);
                            if (hit.collider.gameObject.tag == "Wall")
                            {
                                Vector3 reflectVector = Vector3.Reflect(newVector, hit.normal);

                                RaycastHit hit2;
                                Physics.BoxCast(hit.point, bulletPrefab.transform.localScale / 2, reflectVector, out hit2, Quaternion.identity, 100);
                                if (hit2.collider.gameObject.tag == "Player")
                                {
                                    //弾生成してBreak//***************************************************   弾の発射方向が違う
                                    currentBullet[i] = Instantiate(bulletPrefab);
                                    currentBullet[i].transform.position = Turret.transform.position + Turret.transform.forward * 1f;
                                    currentBullet[i].GetComponent<MoveBullet_kaito>().Initialize(Turret.transform.forward, myStatusData.BULLET_MOVE_SPEED, myStatusData.MAX_NUMBER_OF_REFLECTION, this.gameObject.tag);

                                    shotIntervalCount = 0;

                                    break;

                                }

                            }






                        }



                    }








                }
            }




        }
        else
        {
            shotIntervalCount += Time.deltaTime;
        }
        //砲塔を常にプレイヤーに向かせる
        Vector3 look = new Vector3(target.transform.position.x, Turret.transform.position.y, target.transform.position.z);
        Turret.transform.LookAt(look);


    }


    //弾を生成する
    private void InstantiateBullet(Vector3 direction)
    {

    }


}
