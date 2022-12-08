using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Block
{
    public MeshRenderer Mesh;
    public TextMesh Text;

    int Count;


    void Awake()
    {
        SetDefaultColor();

        Mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        Text = transform.GetChild(0).GetComponent<TextMesh>();
        Box = GetComponent<BoxCollider2D>();
        Sprite = transform.GetChild(1).gameObject;
    }

    void Start()
    {
        Mesh.sortingLayerName = "Default";
        Mesh.sortingOrder = 100;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BlockBullet" ||
            collision.gameObject.tag == "PierceBullet")
        {
            Hit();

            Count--;
            Text.text = Count.ToString();

            if (Count <= 0)
                gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        ReturnSprite();
        Count = 2;
        Text.text = Count.ToString();
        Box.size = Sprite.transform.localScale;
    }
}
