using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_kaito : EnemyBase {

	// Use this for initialization
	void Start () {

        Initialize();           //最初に初期化したりいろいろ取得したりするやつ


    }

    // Update is called once per frame
    void Update () {

        MoveRandomly();
        Shot();

	}

  protected override void aiueo() 
    {
        Debug.Log("enemy1 aiueo");
    }

}
