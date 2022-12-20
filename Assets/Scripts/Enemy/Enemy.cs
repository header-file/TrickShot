using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        RED = 0,
        GREEN = 1,
    }

    protected EnemyType Type;
    protected bool IsInvincible;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsInvincible)
            return;

        if (collision.gameObject.tag == "BlockBullet")
        {
            collision.gameObject.GetComponent<Bullet>().Twinkle();
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
}
