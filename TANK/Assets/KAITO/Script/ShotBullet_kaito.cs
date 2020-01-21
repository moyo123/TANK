using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ShotBullet_kaito : MonoBehaviour
{
    private StatusData myStatus;                      //リソースファイル取得用の変数
    private GameObject[] CurrentBullet;               //発射されている弾を格納するための配列
    private GameObject bulletPrefab;                  //弾のプレハブ
    private float shotIntervalCount;                  //弾を発射する間隔をカウントする変数
    public int ammoCount;                            //残弾数をカウントする変数
    private int layerMask;                            //レイヤーマスク用の変数
    private float instanceBulletDistance=3;           //弾を生成する時、戦車からどのぐらい離すかの距離

    private RectTransform reticle;                    //レティクルの画像

    private GameObject Turret;                        //砲塔のオブジェクトを取得する用の変数

    // Use this for initialization
    void Start()
    {

        myStatus = Resources.Load<StatusData>("PlayerStatus");                //リソースファイル取得
        CurrentBullet = new GameObject[myStatus.MAX_NUMBER_OF_SHOTS];         //配列のメモリ確保
        shotIntervalCount = myStatus.SHOT_INTERVAL_TIME;                      //時間カウントするやつ
        bulletPrefab = (GameObject)Resources.Load("Prefab/BulletPrefab");     //プレハブ取得

        int groundLayer = LayerMask.NameToLayer("Ground");                    //Groundのレイヤー取得
        layerMask = 1 << groundLayer;                                         //レイヤーマスクの設定

        //レティクルの画像のtransformを取得
        var g = GameObject.Find("Reticle");
        if (g)
        {
            Debug.Log("みつけたよ");
            reticle = g.GetComponent<RectTransform>();
        }
        else
        {
            Debug.Log("レティクルの画像が用意されていないか、名前が違う");
        }


        //自分の子どもから砲塔のオブジェクトを取得
         var t = transform.Find("Turret");
        if (t)
        {
            Debug.Log("Turretあったよ");
            Turret = t.gameObject; 
        }
        else
        {
            Debug.Log("砲塔が用意されていないか、名前が違う");
        }


    }



    // Update is called once per frame
    void Update()
    {
        DisplayReticle();                  //レティクルを表示

        ChangeDirectionOfTurret();         //砲塔の向きを変える

        ClickToShot();                     //クリックした座標に向かって弾を撃つ

        CountAmmo();
    }

    private void CountAmmo()
    {
        ammoCount = 0;
        for (int i = 0; i < myStatus.MAX_NUMBER_OF_SHOTS; i++)
        {
            if (CurrentBullet[i] == null)
            {
                ammoCount++;
            }
        }
    }

    //private void DisplayAmmo()
    //{
    //    Instantiate(Ammo);
    //    Ammo.transform.SetParent(canvas.transform, false);
    //}

    //レティクルを表示させる
    private void DisplayReticle()
    {
        if (reticle)
        {
            float mouseX = Input.mousePosition.x;               //マウスのx座標取得
            float mouseY = Input.mousePosition.y;               //マウスのy座標取得
            mouseX = Mathf.Clamp(mouseX, 0, Screen.width);      //レティクルのx座標が画面内に収まるように制限する
            mouseY = Mathf.Clamp(mouseY, 0, Screen.height);     //レティクルのy座標が画面内に収まるように制限する
            reticle.position = new Vector3(mouseX, mouseY, 0);  //レティクルに座標を代入
        }
    }



    /******************************************************
     ***    クリックした座標に向かって弾を撃つ   **********
     * ***************************************************/
    private void ClickToShot()
    {
        //カウントが指定した時間を超えたら弾が発射できるようになる
        if (shotIntervalCount >= myStatus.SHOT_INTERVAL_TIME)
        {
            //マウス左クリックした時
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(ammoCount);
                for (int i = 0; i < myStatus.MAX_NUMBER_OF_SHOTS; i++)
                {
                    //配列の中にnullがあったらそこに弾を格納する、なかったら弾を生成しない
                    if (CurrentBullet[i] == null)
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //マウスをクリックした方向にレイを生成

                        //レイが地面に当たった時、その場所の方向に向かって弾を発射する
                        if (Physics.Raycast(ray, out hit, 100, layerMask))
                        {
                            Vector3 clickDirection = hit.point - transform.position;                              //クリックした方向のベクトルを取得
                            clickDirection = new Vector3(clickDirection.x, 0, clickDirection.z);                  //Yの座標を0にする

                            GameObject instanceBullet = Instantiate(bulletPrefab);                                //プレハブから弾を生成
                            instanceBullet.transform.position = transform.position + clickDirection .normalized * instanceBulletDistance;     //弾の座標を設定、戦車からちょっと離した場所に生成する
                            instanceBullet.GetComponent<MoveBullet_kaito>().Initialize(clickDirection,myStatus.BULLET_MOVE_SPEED,myStatus.MAX_NUMBER_OF_REFLECTION , this.gameObject.tag);                //弾にベクトル、スピード、反射の回数、自分のタグを設定する

                            CurrentBullet[i] = instanceBullet;                                                    //生成した弾を配列に格納する

                            shotIntervalCount = 0;                                                                //カウントを0に戻す
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            shotIntervalCount += Time.deltaTime;            //時間をカウントする
        }
        　
    }


    //砲塔の方向を変える　テスト用
    private void ChangeDirectionOfTurret()
    {
        if (Turret)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);             //レイを生成する、同じようなこと2回してることになるから本当は関数にしてまとめた方がいいはず

            //レイが地面に当たった時、その場所の方向に向かって砲塔を向ける
            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                //Vector3 mousePoint = hit.point - Turret.transform.position;        //自分の座標から、レイが当たった場所に向かってのベクトルを取得
                //mousePoint = new Vector3(mousePoint.x, 0, mousePoint.z);           //Y座標だけ0にする

                //Turret.transform.LookAt(Turret.transform.position + mousePoint);   //求めた方向に砲塔を向かせる

                Turret.transform.LookAt(new Vector3(hit.point.x, 1, hit.point.z));

            }
        }
    }

    Transform GetTurretFromChild(Transform _transform)
    {
        //if (_transform.childCount > 0)
        //{

        //    foreach (Transform child in _transform)
        //    {
        //        if (child.gameObject.tag == "Turret")
        //        {
        //            return child;
        //        }
        //        else
        //        {
        //            Transform c = GetTurretFromChild(child);
        //        }

        //    }
        //}


        return null;
    }

}
