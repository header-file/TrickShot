using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
    void Awake()
    {
        Type = EnemyType.GREEN;
    }

    void OnEnable()
    {
        IsInvincible = false;
    }
}
