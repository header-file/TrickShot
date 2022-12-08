using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : Bullet
{
    void Awake()
    {
        Type = BulletType.PIERCE;
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
