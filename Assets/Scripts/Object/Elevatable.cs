using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevatable : Block
{
    public SpriteRenderer Renderer;

    public bool IsUp;
    Color color;


    void Start()
    {
        Box = GetComponent<BoxCollider2D>();
        Sprite = transform.GetChild(0).gameObject;
        Renderer = Sprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!IsUp)
            Elevate();
        else
            Demote();
    }

    void Elevate()
    {
        if (1.0f - color.a <= 0.001f)
            return;

        color = Renderer.color;
        color.a = Mathf.Lerp(color.a, 1.0f, Time.deltaTime * 2.0f);
        Renderer.color = color;

        if (1.0f - color.a <= 0.001f)
            Invoke("SetUpT", 1.0f);
    }

    void Demote()
    {
        if (color.a - 0.5f <= 0.001f)
            return;

        color = Renderer.color;
        color.a = Mathf.Lerp(color.a, 0.5f, Time.deltaTime * 2.0f);
        Renderer.color = color;

        if (color.a - 0.5f <= 0.001f)
            Invoke("SetUpF", 1.0f);
    }

    void SetUpT()
    {
        Box.enabled = false;
        IsUp = true;
    }

    void SetUpF()
    {
        Box.enabled = true;
        IsUp = false;
    }

    void OnEnable()
    {
        ReturnSprite();
        Box.size = Sprite.transform.localScale;
        Box.enabled = false;
        IsUp = true;
        color = Renderer.color;
        color.a = 1.0f;
        Renderer.color = color;
    }
}
