using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyTextMesh TextMesh;
    public GameObject Shield;

    int ShieldCount;
    bool IsInvincible;


    void Awake()
    {
        Shield = transform.GetChild(0).gameObject;
        TextMesh = transform.GetChild(1).GetComponent<EnemyTextMesh>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInvincible)
            return;

        if(collision.gameObject.tag == "BlockBullet")
        {
            collision.gameObject.SetActive(false);

            Die();
        }
    }

    public void Die()
    {
        GameObject exp = GameManager.Inst().ObjManager.MakeObj("Explosion");
        exp.transform.position = transform.position;

        gameObject.SetActive(false);
    }

    public void DeclineCount()
    {
        ShieldCount--;
        TextMesh.SetNumber(ShieldCount);

        if(ShieldCount <= 0)
        {
            TextMesh.gameObject.SetActive(false);
            Shield.SetActive(false);
            Invoke("NotInvincible", 0.0001f);
        }
    }

    public void ReturnCount()
    {
        TextMesh.gameObject.SetActive(true);
        Shield.SetActive(true);

        ShieldCount = 2;
        TextMesh.SetNumber(ShieldCount);
    }

    void NotInvincible()
    {
        IsInvincible = false;
    }

    void OnEnable()
    {
        ShieldCount = 2;
        TextMesh.SetNumber(ShieldCount);
        IsInvincible = true;
    }
}
