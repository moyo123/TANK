using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class EnemyStatusData : StatusData {


    [SerializeField, Header("弾を撃つ時の１セットの数(仮)")]
    private int ComboNum;
    public int COMBO_NUM { get { return ComboNum; } }

    [SerializeField, Header("弾を撃つ時の１セット毎の間隔(仮)")]
    private float ComboIntervalTime;
    public float COMBO_INTERVAL_TIME { get { return ComboIntervalTime; } }






}
