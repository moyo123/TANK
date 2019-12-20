using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManeger : MonoBehaviour {

    [SerializeField] private string SceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveScene();
        }

	}

    void MoveScene()
    {
        Debug.Log(SceneName);
        SceneManager.LoadScene(SceneName);
    }

}
