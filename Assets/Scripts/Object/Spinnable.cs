using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinnable : Block
{
    public Vector2 OriginPos;


    void Start()
    {
        Box = GetComponent<BoxCollider2D>();
        Sprite = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Sprite.gameObject.transform.position = OriginPos;
    }

    void OnEnable()
    {
        ReturnSprite();
        OriginPos = transform.position;
    }
}
