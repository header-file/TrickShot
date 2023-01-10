using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public struct BulletData
    {
        public Bullet.BulletType Type;
        public int BounceCount;
    }

    public LineRenderer Line;
    public LayerMask Filter;

    public BulletData[] Cylinder;
    public bool IsGuiding;
    public int CurBulletIdx;


    public bool IsCylinderEmpty() { return Cylinder[CurBulletIdx].Type == Bullet.BulletType.NONE ? true : false; }

    void Awake()
    {
        IsGuiding = false;
        Cylinder = new BulletData[6];
        for (int i = 0; i < Cylinder.Length; i++)
            Cylinder[i] = new BulletData();

        CurBulletIdx = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (IsGuiding)
            LineRender();
    }

    public void SetCylinder()
    {
        CurBulletIdx = 0;
        for (int i = 0; i < Constants.MAX_CYLINDER; i++)
        {
            Bullet.BulletType bType = GameManager.Inst().StgManager.Stages[GameManager.Inst().StgManager.CurWorld - 1, GameManager.Inst().StgManager.CurStage - 1].Cylinders[i];
            Cylinder[i].Type = bType;
            Cylinder[i].BounceCount = GameManager.Inst().BltManager.BDatas[(int)bType].BounceCount;
        }

        GameManager.Inst().UiManager.Down.Cylinder.SetUpCylinder();
    }

    void LineRender()
    {
        if (!Line.gameObject.activeSelf)
            Line.gameObject.SetActive(true);

        Line.positionCount = 2;
        Line.SetPosition(0, transform.position);
        Vector2 dir = transform.up;
        Vector2 pos = Line.GetPosition(0);

        for (int i = 1; i < Line.positionCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, dir, 100.0f, Filter);

            Line.SetPosition(i, hit.point);
            //pos = hit.point;
            //dir = Vector2.Reflect(dir.normalized, hit.normal);
        }
    }

    public void Rotate(Vector2 MPos)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 norm = (MPos - pos) / Vector2.Distance(MPos, pos);
        float angle = Vector2.Angle(Vector2.up, norm);
        if (MPos.x > transform.position.x)
            angle *= -1;
        Quaternion rot = Quaternion.Euler(0.0f, 0.0f, angle);
        transform.rotation = rot;
    }

    public void AfterShot()
    {
        GameManager.Inst().BltManager.SetLineData(Cylinder[CurBulletIdx].Type);
        GameManager.Inst().UiManager.Down.Cylinder.Shot(CurBulletIdx++);
    }
}
