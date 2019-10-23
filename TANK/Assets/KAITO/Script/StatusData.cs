using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class StatusData : ScriptableObject
{



    [SerializeField,Header("動くスピード"),Tooltip("自分が動くスピード")]
    private float MoveSpeed;
    public float MOVE_SPEED { get { return MoveSpeed; } }

    [SerializeField,Header(" 回転するスピード"),Tooltip("自分が回転するスピード")]
    private float RotateSpeed;
    public float ROTATE_SPEED { get { return RotateSpeed; } }

    [SerializeField,Header("弾の動くスピード") ]
    private float BulletMoveSpeed;
    public float BULLET_MOVE_SPEED { get { return BulletMoveSpeed; } }

    [SerializeField ,Header("弾を撃てる最大の数") ]
    private int MaxNumberOfShots;
    public int MAX_NUMBER_OF_SHOTS { get { return MaxNumberOfShots; } }

    [SerializeField, Header("反射する最大の数")]
    private int MaxNumberOfReflection;
    public int MAX_NUMBER_OF_REFLECTION { get { return MaxNumberOfReflection; } }

    [SerializeField, Header("弾を撃つ間隔の時間")]
    private float ShotIntervalTime;
    public float SHOT_INTERVAL_TIME { get { return ShotIntervalTime; } }



}










