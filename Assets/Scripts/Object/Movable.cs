using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Block
{
    public Vector3[] TargetPositions;
    public float Speed;

    int TargetIndex;


    void Start()
    {
        Box = GetComponent<BoxCollider2D>();
        Sprite = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPositions[TargetIndex], Time.deltaTime * Speed);

        if(Vector3.Distance(transform.position, TargetPositions[TargetIndex]) <= 0.001f)
            TargetIndex = TargetIndex < (TargetPositions.Length - 1) ? TargetIndex + 1 : 0;
    }

    void OnEnable()
    {
        ReturnSprite();
        Box.size = Sprite.transform.localScale;
        TargetIndex = 0;
        Speed = 1.0f;
    }
}
