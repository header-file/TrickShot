using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        WALL = 0,
        BREAKABLE = 1,
        MOVABLE = 2,
        SPINNABLE = 3,
        ELEVATABLE = 4,
    }

    public BoxCollider2D Box;
    public GameObject Sprite;

    public BlockType Type;

    Color Color;


    public void SetSize(Vector2 size) { Sprite.transform.localScale = size; Box.size = Sprite.transform.localScale; }

    void Awake()
    {
        SetDefaultColor();
    }

    public void SetDefaultColor()
    {
        Color = Sprite.GetComponent<SpriteRenderer>().color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BlockBullet" ||
            collision.gameObject.tag == "PierceBullet")
        {
            Hit();
        }
    }

    public void Hit()
    {
        Sprite.GetComponent<SpriteRenderer>().color = Color.white;
        Invoke("ReturnSprite", 0.1f);
    }

    public void ReturnSprite()
    {
        Sprite.GetComponent<SpriteRenderer>().color = Color;
    }

    void OnEnable()
    {
        ReturnSprite();
    }
}
