using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : Bullet
{
    void Awake()
    {
        Type = BulletType.TIME;
    }

    void Update()
    {
        Sprite.transform.up = transform.up;
    }

    void OnEnable()
    {
        Speed = 10.0f;
        if (GameManager.Inst() != null)
            BounceCount = GameManager.Inst().BltManager.BDatas[(int)Type].BounceCount;
    }
}
