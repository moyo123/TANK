using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Test_Navigation_kaito : MonoBehaviour
{

    private GameObject target;                  //  プレイヤー


   [SerializeField] private StatusData myStatusData;            //StatusData取得用
    private GameObject bulletPrefab;            //弾のプレハブ取得用
    private NavMeshAgent myNavMeshAgent;        //NavMeshAgent取得用
    private Rigidbody myRigidbody;              //Rigidbody取得用



    private Vector3 maxRange;                 //ランダムで移動させる時の一番右上の座標を格納するための変数
    private Vector3 minRange;                 //ランダムで移動させる時の一番左下の座標を格納するための変数
    private float stoppingDistance = 5;       //目標地点のどのぐらい手前まで来たら止まるかのやつ
    private bool isMove = false;              //今が動いているかを判別するやつ
    private float moveIntervalTime = 3;       //次動くまでにどのぐらい止まっておくかの時間、単位は秒
    private float moveIntervalCount = 0;    　//時間をカウントするやつ
    private Vector3 destinationPosition;      //行き先の座標
    //private float stopTime = 0;               //なんか目標までの距離に到達していないのに勝手に止まっちゃう現象が起きちゃうから、止まった時間が長いと勝手に座標を更新して動かす処理を追加するためのやつ


    private GameObject Turret;                //自分の砲塔取得用
    private float shotIntervalTime;
    private float shotIntervalCount;
    private GameObject[] currentBullet;



    // Use this for initialization
    void Start()
    {
        //StatusDataをアタッチしていなかった場合、Enemy1のStatusをつける
        //if (!myStatusData)
        //{
        //Debug.Log(myStatusData);
            myStatusData = Resources.Load<StatusData>("TestEnemy1_kaito");          //自分のStatusDataを取得
        //}
        
        bulletPrefab = (GameObject)Resources.Load("Prefab/BulletPrefab");       //弾のプレハブを取得

        target = GameObject.FindGameObjectWithTag("Player");                    //プレイヤーを取得
        myNavMeshAgent = GetComponent<NavMeshAgent>();                          //NavMeshAgentを取得
        myNavMeshAgent.angularSpeed = myStatusData.ROTATE_SPEED;
        myNavMeshAgent.speed = myStatusData.MOVE_SPEED;


        Turret = transform.Find("Turret").gameObject;                           //自分の砲台を取得
            
        myRigidbody = GetComponent<Rigidbody>();                                //リジッドボディ取得
        myRigidbody.useGravity = false;                                         //重力をオフに設定
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;   //回転とY軸へ勝手に移動しないように設定


        //４つの壁からxとzの最大と最小の座標を取得する
        GameObject walls = GameObject.Find("Walls");
        foreach (Transform child in walls.transform)
        {
            if (child.position.x < 0)
                minRange.x = child.position.x;
            else if (child.position.x > 0)
                maxRange.x = child.position.x;
            else if (child.position.z < 0)
                minRange.z = child.position.z;
            else if (child.position.z > 0)
                maxRange.z = child.position.z;


        }


        shotIntervalTime = myStatusData.SHOT_INTERVAL_TIME;
        shotIntervalCount = 0;
        currentBullet = new GameObject[myStatusData.MAX_NUMBER_OF_SHOTS];
    }

    // Update is called once per frame
    void Update()
    {

        MoveRandomly();     //ランダムで動かすやつ
        Shot();             //弾を発射するやつ


    }



    //ランダムで行き先を決めて取得する
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(minRange.x, maxRange.x);     //x座標をランダムで決める
        float z = Random.Range(minRange.z, maxRange.z);     //z座標をランダムで決める
        Vector3 randomPosition = new Vector3(x, 1, z);      //ランダムで決めた値を入れる
        return randomPosition;                              //ランダムで決めた値を返す
    }



    //弾を撃つ
    private void Shot()
    {
        if (shotIntervalCount >= shotIntervalTime)
        {
            RaycastHit hit;
            if (Physics.BoxCast(Turret.transform.position, bulletPrefab.transform.localScale / 2, Turret.transform.forward, out hit, Quaternion.identity, 100))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    for(int i = 0; i < myStatusData.MAX_NUMBER_OF_SHOTS; i++)
                    {
                        if (currentBullet[i] == null)
                        {
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

        else
        {
            shotIntervalCount += Time.deltaTime;
        }


    }


    //ランダムで動かす
    private void MoveRandomly()
    {


        //指定した時間を超えたら動かす
        if (moveIntervalCount >= moveIntervalTime)
        {
            //動いていない状態だったら値を決めて動かすようにする。
            if (!isMove)
            {
                destinationPosition = GetRandomPosition();                          //ランダムで座標を取る
                //GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);      //テスト用。目標地点にcubeを生成して目で見えるようにしてた
                //g.transform.position = destinationPosition;                         //

                myNavMeshAgent.SetDestination(destinationPosition);                 //ナヴィゲーションの行き先を設定する
                myNavMeshAgent.stoppingDistance = stoppingDistance;                 //行き先にどのぐらい近づいたら停止するかを設定する

                isMove = true;
            }
            else
            {
                //行き先に指定した値まで近づいたら停止させる
                float Distance = (destinationPosition - transform.position).magnitude;
                //Debug.Log("distance " + Distance);
                if (Distance <= stoppingDistance+2)                      //****************  何故か、stoppingDistanceの距離よりも近くなったら止まるって書いても、近くないのに止まる時があるから+2だけ伸ばしてる。原因が分かったなら修正してほしい
                {
                    isMove = false;                                      //フラグを動いていない状態に戻す
                    moveIntervalCount = 0;                               //カウントを0にする
                    moveIntervalTime = Random.Range(0, 5);               //次動くまでの時間を設定する
                }



            }

        }
        else
        {           
            moveIntervalCount += Time.deltaTime;                        //時間をカウントする
        }

        myRigidbody.velocity = Vector3.zero;                         　 //勝手に移動しないように加速度を常に0にする

        //砲塔を常にプレイヤーに向かせる
        Vector3 look = new Vector3(target.transform.position.x, Turret.transform.position.y, target.transform.position.z);
        Turret.transform.LookAt(look);             

    }

}
