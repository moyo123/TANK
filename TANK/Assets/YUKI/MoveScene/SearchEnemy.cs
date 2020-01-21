using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchEnemy : MonoBehaviour {

    GameObject[] tagObjects; //敵の数
    [SerializeField] Text GameEnd;　//クリア、失敗時のテキストを入れる

    [SerializeField] private string[] GameEndString;　//クリア、失敗時の文字を入れる　（[0] = クリア時 | [1] = 失敗時）

    [SerializeField] private string SceneName;  //次のシーンの名前を入れる（失敗時は勝手にタイトルに戻る）
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tagObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (!GameObject.FindWithTag("Player"))　//プレイヤーがいないとき（失敗時）
        {
            if (GameEndString[1] == "バグ有、次ステージ移動")
            {
                GameEnd.text = GameEndString[1];
                Invoke("MoveScene", 2);
            }
            else
            {
                GameEnd.text = GameEndString[1];
                Invoke("FailedMoveScene", 2);
            }
        }
        if(tagObjects.Length == 0 && GameObject.FindWithTag("Player"))　//プレイヤーがいる且つ敵が全滅時（クリア）
        {
            if (GameEndString[0] == "All CLEAR")
            {
                GameEnd.text = GameEndString[0];
                Invoke("MoveScene", 3.5f);
            }
            else
            {
                GameEnd.text = GameEndString[0];
                Invoke("MoveScene", 2);
            }
        }
    }

    void MoveScene()　//次ステージ行き
    {
            //Debug.Log(SceneName);
            SceneManager.LoadScene(SceneName);
    }
    void FailedMoveScene()　//タイトル行き
    {
        SceneManager.LoadScene("StartScene");
    }

}
