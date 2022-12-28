using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        NONE = 0,
        NORMAL = 1,
        PIERCE = 2,
        TIME = 3,
    };

    public Rigidbody2D Rig;
    public GameObject Sprite;

    protected BulletType Type;
    protected int BounceCount;
    protected float Speed;
    protected Vector2 Dir;

    public BulletType GetBulletType() { return Type; }
    public int GetBounceCount() { return BounceCount; }

    public void Shoot()
    {
        Rig.velocity = transform.up * Speed;
        Dir = transform.up;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Enemy")
        {
            Dir = transform.up;

            BounceCount--;

            GameManager.Inst().BltManager.SetLinePos(collision.contacts[0].point);

            StartCoroutine(Timelag());

            EnemyRed[] enemies = FindObjectsOfType<EnemyRed>();
            for (int i = 0; i < enemies.Length; i++)
                if (enemies[i].isActiveAndEnabled)
                    enemies[i].DeclineCount();

            if (BounceCount <= 0)
            {
                Twinkle();

                if (GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].Type == BulletType.NONE)
                    GameManager.Inst().UiManager.Mid.Result.Fail();

                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Timelag()
    {
        Rig.velocity *= 0.1f;
        yield return new WaitForSeconds(0.075f);
        Rig.velocity *= 10.0f;
    }

    public void Twinkle()
    {
        GameObject twinkle = GameManager.Inst().ObjManager.MakeObj("Twinkle");
        twinkle.transform.position = transform.position;
    }

    void OnDisable()
    {
        EnemyRed[] enemies = FindObjectsOfType<EnemyRed>();
        for (int i = 0; i < enemies.Length; i++)
            if (enemies[i].isActiveAndEnabled)
                enemies[i].ReturnCount();

        StopAllCoroutines();
    }
}
