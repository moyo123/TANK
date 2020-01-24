using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAmmo : MonoBehaviour {
    [SerializeField] GameObject[] Ammo;
    private int ammoCount;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ammoCount = this.GetComponent<ShotBullet_kaito>().ammoCount;

        UpdateAmmo();
    }

    void UpdateAmmo()
    {
        for(int i=0; Ammo.Length > i; i++)
        {
            Ammo[i].SetActive(false);
        }

        for(int i=0; ammoCount > i; i++)
        {
            Ammo[i].SetActive(true);
        }
    }
}
