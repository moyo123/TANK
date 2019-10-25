using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_kaito : MonoBehaviour
{

    private StatusData myStatus;        //リソースファイル取得用の変数


    // Use this for initialization
    void Start()
    {
        myStatus = Resources.Load<StatusData>("PlayerStatus");     //リソースファイル取得

    }

    // Update is called once per frame
    void Update()
    {



    }


    //Update()に動く処理を書くと壁にぶつかった時に何故かガタガタするからこっちに書く
    private void FixedUpdate()
    {
        PlayerMove();   //プレイヤーの回転と移動の処理


    }

    //プレイヤーの回転と移動
    private void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  //左右の入力
        float vertical = Input.GetAxisRaw("Vertical");      //上下の入力
        Vector3 toDirection = (new Vector3(horizontal, 0, vertical)).normalized;    //入力した方向を格納

        //キーが入力されたら
        if (toDirection != Vector3.zero)
        {
            //プレイヤーの向いてる方向とキーの入力した方向が同じだった場合
            if (transform.forward == toDirection)
            {
                Vector3 velocity = transform.forward * myStatus.MOVE_SPEED * Time.deltaTime;    //進む距離を計算する
                transform.Translate(velocity, Space.World);                                     //移動させる
            }
            //プレイヤーが目標の方向に向いていない時、そこまで回転させる
            else
            {
                //回転させる
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(toDirection), Time.deltaTime * myStatus.ROTATE_SPEED);
                //Vector3.RotateTowards
            }
        }
    }



}
