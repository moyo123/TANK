using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    private GameObject Explosion;
    private GameObject Smoke;

    // Use this for initialization
    void Start()
    {
        Smoke = Resources.Load("Prefab/SmokeEffect", typeof(GameObject)) as GameObject;
        Explosion = Resources.Load("Prefab/BigExplosionEffect", typeof(GameObject)) as GameObject;
    }

    public void Effect(Transform other)
    {
        Instantiate(Smoke, other.position, Quaternion.identity);
        Instantiate(Explosion, other.position, Quaternion.identity);
    }

}
