using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Vector3 MousePos;

    public bool IsReload;

    bool IsAiming;


    void Awake()
    {
        IsReload = true;
        IsAiming = false;
    }

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !IsReload || GameManager.Inst().Player.IsCylinderEmpty())
            return;

        IsAiming = true;

        GameManager.Inst().Player.Line.gameObject.SetActive(true);
        GameManager.Inst().Player.IsGuiding = true;

        GameManager.Inst().UiManager.Down.Cylinder.gameObject.SetActive(false);

        if (GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].Type == Bullet.BulletType.TIME)
        {
            GameManager.Inst().StartToGray();
            Time.timeScale = 0.25f;
        }
    }

    void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !IsAiming)
            return;

        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameManager.Inst().Player.Rotate(MousePos);
    }

    void OnMouseUp()
    {
        if (!IsAiming)
            return;

        IsReload = false;
        IsAiming = false;

        if (GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].Type == Bullet.BulletType.TIME)
        {
            Time.timeScale = 1.0f;
            GameManager.Inst().StartEndGray();
        }

        GameManager.Inst().UiManager.Down.Cylinder.gameObject.SetActive(true);

        GameManager.Inst().Player.Line.gameObject.SetActive(false);
        GameManager.Inst().Player.IsGuiding = false;

        GameObject bullet;
        switch(GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].Type)
        {
            case Bullet.BulletType.NORMAL:
                bullet = GameManager.Inst().ObjManager.MakeObj("Normal");
                break;

            case Bullet.BulletType.PIERCE:
                bullet = GameManager.Inst().ObjManager.MakeObj("Pierce");
                break;

            case Bullet.BulletType.TIME:
                bullet = GameManager.Inst().ObjManager.MakeObj("Time");
                break;

            default:
                return;
        }
        
        bullet.transform.position = GameManager.Inst().Player.transform.position;
        bullet.transform.up = GameManager.Inst().Player.transform.up;
        bullet.GetComponent<Bullet>().Shoot();

        GameManager.Inst().Player.AfterShot();
    }
}
