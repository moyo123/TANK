using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    private StatusData myStatus;            //リソースファイル取得用の変数
    private GameObject[] CurrentBullet;     //発射されている弾を格納するための配列
    private GameObject bulletPrefab;        //弾のプレハブ
    private float shotIntervalCount;        //弾を発射する間隔をカウントする変数
    private int layerMask;                   //レイヤーマスク用の変数

    // Use this for initialization
    void Start()
    {

        myStatus = Resources.Load<StatusData>("PlayerStatus");      //リソースファイル取得
        CurrentBullet = new GameObject[myStatus.MAX_NUMBER_OF_SHOTS];       //配列のメモリ確保
        shotIntervalCount = myStatus.SHOT_INTERVAL_TIME;                    //時間カウントするやつ
        bulletPrefab = (GameObject)Resources.Load("Prefab/BulletPrefab");       //プレハブ取得

        int groundLayer = LayerMask.NameToLayer("Ground");                  //Groundのレイヤー取得
        layerMask = 1 << groundLayer;                                       //レイヤーマスクの設定

    }

    // Update is called once per frame
    void Update()
    {

        //カウントが指定した時間を超えたら弾が発射できるようになる
        if (shotIntervalCount >= myStatus.SHOT_INTERVAL_TIME)
        {
            //マウス左クリックした時
            if (Input.GetMouseButtonDown(0))
            {
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
                            Vector3 clickDirection = hit.point - transform.position;                            //クリックした方向のベクトルを取得
                            Vector3 shotDirection = new Vector3(clickDirection.x, 0, clickDirection.z);         //Yの座標を0にする
                            GameObject instanceBullet = Instantiate(bulletPrefab);                              //プレハブから弾を生成
                            instanceBullet.transform.position = transform.position + shotDirection.normalized * 3f;     //弾の座標を設定、戦車からちょっと離して場所に生成する
                            instanceBullet.GetComponent<MoveBullet>().Initialize(shotDirection);                //弾にベクトルを設定する

                            CurrentBullet[i] = instanceBullet;                                                  //生成した弾を配列に格納する

                            shotIntervalCount = 0;                                                              //カウントを0に戻す
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            shotIntervalCount += Time.deltaTime;            //カウントする
        }


    }
}
