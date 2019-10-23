using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class MoveBullet : MonoBehaviour
{

    private static StatusData myStatus;     //リソースファイル取得用の変数
    private Vector3 Direction;          //進む方向
    private int currentNumberOfReflection;  //反射した回数を格納するための変数
    private string parentTag;

    // Use this for initialization
    void Start()
    {

        //リソースファイル入ってなかったら取得してくる
        if (myStatus == null)
        {
            myStatus = Resources.Load<StatusData>("PlayerStatus");  //取得する
        }

        transform.LookAt(transform.position + Direction);       //進む方向に向かせる
        transform.Rotate(90, 0, 0);//***********************************    
        currentNumberOfReflection = 0;      //反射した回数のやつ

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 velocity = Direction * myStatus.BULLET_MOVE_SPEED * Time.deltaTime;     //進む距離を計算する
        transform.Translate(velocity, Space.World);                                      //進ませる

    }



    //自分が生成された時に値を設定するための関数
    public void Initialize(Vector3 _direction)
    {
        Direction = _direction.normalized;  //進む方向を格納する
    }


    //何かに当たった時の処理
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":    //壁
                //Debug.Log("hitWall");
                //反射の回数が上限を超えていない場合反射させる
                if (currentNumberOfReflection < myStatus.MAX_NUMBER_OF_REFLECTION)
                {
                    Direction = Vector3.Reflect(Direction, collision.contacts[0].normal);
                    transform.LookAt(transform.position + Direction);       //進む方向に向かせる
                    transform.Rotate(90, 0, 0);//***********************************
                    currentNumberOfReflection++;        //反射の回数を１つ増やす
                }
                else
                {
                    Destroy(this.gameObject);       //自分のオブジェクトを消す
                }

                break;

            case "Player":      //プレイヤー
                //Debug.Log("hitPlayer");
                Destroy(this.gameObject);            //自分のオブジェクトを消す
                break;

            case "Enemy":       //敵
                //Debug.Log("hitEnemy");
                Destroy(this.gameObject);            //自分のオブジェクトを消す
                break;



        }
    }
}
