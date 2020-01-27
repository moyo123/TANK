using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;

public class MoveBullet_kaito : MonoBehaviour
{
    //[SerializeField]
    //private  StatusData myStatus;     //リソースファイル取得用の変数
    private Vector3 Direction;                //進む方向
    private int currentNumberOfReflection;    //反射した回数を格納するための変数
    private string parentTag;                 //自分を撃った親のタグ名
    private int maxReflection;                //反射する回数
    private float moveSpeed;                  //進むスピード
    private AudioSource BulletSound;          //SE

    // Use this for initialization
    void Start()
    {

        ////リソースファイル入ってなかったら取得してくる
        //if (myStatus == null)
        //{
        //    switch (parentTag)
        //    {
        //        case "Player":
        //            myStatus = Resources.Load<StatusData>("PlayerStatus");  //取得する
        //            break;

        //        case "Enemy":
        //            myStatus = Resources.Load<StatusData>("TestEnemy1_kaito");
        //            break;
        //    }
        //}

        transform.LookAt(transform.position + Direction);        //進む方向に向かせる
        transform.Rotate(90, 0, 0);//***********************************    
        currentNumberOfReflection = 0;                           //反射した回数のやつ
        BulletSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 velocity = Direction * myStatus.BULLET_MOVE_SPEED * Time.deltaTime;     //進む距離を計算する
        Vector3 velocity = Direction * moveSpeed * Time.deltaTime;                        //進む距離を計算する
        transform.Translate(velocity, Space.World);                                       //進ませる

    }



    //自分が生成された時に値を設定するための関数
    public void Initialize(Vector3 _direction ,float _moveSpeed,int _maxReflection, string _parentTag)
    {
        Direction = _direction.normalized;  //進む方向を格納する
        moveSpeed = _moveSpeed;             //進むスピード
        maxReflection = _maxReflection;     //反射する回数
        parentTag = _parentTag;             //親のタグ名を格納
    }

    private void THISDES()
    {
        Destroy(this.gameObject);       //自分のオブジェクトを消す
    }


    //何かに当たった時の処理
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":    //壁
                //反射の回数が上限を超えていない場合反射させる
                if (currentNumberOfReflection < maxReflection)
                {
                    Direction = Vector3.Reflect(Direction, collision.contacts[0].normal);   //反射した時のベクトルを求める
                    transform.LookAt(transform.position + Direction);                       //進む方向に向かせる
                    transform.Rotate(90, 0, 0);//***********************************
                    BulletSound.PlayOneShot(BulletSound.clip);
                    currentNumberOfReflection++;                                            //反射の回数を１つ増やす
                    Debug.Log(currentNumberOfReflection + "回反射したよ");
                    Debug.Log(System.DateTime.Now.Hour.ToString() + "H:" +
                            System.DateTime.Now.Minute.ToString() + "M:" +
                              System.DateTime.Now.Second.ToString() + "S");
                }
                else
                {
                    Destroy(this.gameObject);       //自分のオブジェクトを消す
                }

                break;

            case "Player":      //プレイヤー
                AudioManager.Instance.PlaySE("爆破・破砕音02");
                GetComponent<ExplosionEffect>().Effect(collision.transform);
                Destroy(collision.gameObject);          //衝突したオブジェクトを消す
                Destroy(this.gameObject);            //自分のオブジェクトを消す
                break;

            case "Enemy":       //敵
                AudioManager.Instance.PlaySE("爆破・破砕音02");
                GetComponent<ExplosionEffect>().Effect(collision.transform);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);            //自分のオブジェクトを消す
                break;


            case "Bullet":      //弾
                                // Destroy(this.gameObject);           //自分のオブジェクトを消す

                //反射の回数が上限を超えていない場合反射させる
                if (currentNumberOfReflection < maxReflection)
                {
                    Direction = Vector3.Reflect(Direction, collision.contacts[0].normal);   //反射した時のベクトルを求める
                    transform.LookAt(transform.position + Direction);                       //進む方向に向かせる
                    transform.Rotate(90, 0, 0);//***********************************
                    BulletSound.PlayOneShot(BulletSound.clip);
                    currentNumberOfReflection++;                                            //反射の回数を１つ増やす

                    Invoke("THISDES", 3f);
                }
                else 
                {
                    Destroy(this.gameObject);       //自分のオブジェクトを消す
                }



                break;


        }
    }
}