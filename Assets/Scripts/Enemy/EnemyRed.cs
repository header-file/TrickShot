using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    public EnemyTextMesh TextMesh;
    public GameObject Shield;

    int ShieldCount;


    void Awake()
    {
        Type = EnemyType.RED;
        Shield = transform.GetChild(0).gameObject;
        TextMesh = transform.GetChild(1).GetComponent<EnemyTextMesh>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInvincible)
            return;

        if (collision.gameObject.tag == "BlockBullet")
        {
            collision.gameObject.GetComponent<Bullet>().Twinkle();
            collision.gameObject.SetActive(false);

            Die();

            //게임 클리어
            Time.timeScale = 0.25f;
            Invoke("ReturnTime", 0.75f);
        }
    }

    public void DeclineCount()
    {
        ShieldCount--;
        TextMesh.SetNumber(ShieldCount);

        if (ShieldCount <= 0)
        {
            TextMesh.gameObject.SetActive(false);
            Shield.SetActive(false);
            Invoke("NotInvincible", Time.deltaTime);
        }
    }

    public void ReturnCount()
    {
        TextMesh.gameObject.SetActive(true);
        Shield.SetActive(true);

        Invoke("OnEnable", Time.deltaTime);
    }

    void NotInvincible()
    {
        IsInvincible = false;
    }

    void ReturnTime()
    {
        Time.timeScale = 1.0f;
    }

    void OnEnable()
    {
        ShieldCount = 3;
        TextMesh.SetNumber(ShieldCount);
        IsInvincible = true;
    }
}
