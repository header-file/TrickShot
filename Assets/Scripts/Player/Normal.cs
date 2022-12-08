using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Bullet
{
    void Awake()
    {
        Type = BulletType.NORMAL;
    }

    void Update()
    {
        Sprite.transform.up = transform.up;
    }

    void OnEnable()
    {
        Speed = 10.0f;
        if(GameManager.Inst() != null)
            BounceCount = GameManager.Inst().BltManager.BDatas[(int)Type].BounceCount;
    }
}
