using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cylinder : MonoBehaviour
{
    public GameObject CylinderBase;
    public Image[] Bullets;
    public Text BounceCount;

    bool IsRotating;
    float TargetRot;
    float RotZ;


    void Awake()
    {
        CylinderBase = transform.GetChild(0).gameObject;
        IsRotating = false;
        TargetRot = 0.0f;
    }

    void Start()
    {
        
    }

    public void SetUpCylinder()
    {
        for (int i = 0; i < Bullets.Length; i++)
            Bullets[i].color = GameManager.Inst().UiManager.BulletColors[(int)GameManager.Inst().Player.Cylinder[i].Type];

        BounceCount.text = GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].BounceCount.ToString();
    }

    void Update()
    {
        if (IsRotating)
            Rotate();
    }

    public void Shot(int Index)
    {
        Bullets[Index].gameObject.SetActive(false);
        IsRotating = true;
        TargetRot = 60.0f * (Index + 1);
        RotZ = CylinderBase.transform.localRotation.eulerAngles.z;
    }

    void Rotate()
    {
        RotZ += (10.0f / 6.0f);

        CylinderBase.transform.Rotate(Vector3.forward, (10.0f / 6.0f));

        if (Mathf.Abs(RotZ - TargetRot) <= 0.001f)
        {
            IsRotating = false;

            //BounceCount ¹Ù²ñ
            BounceCount.text = GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].BounceCount.ToString();

            GameManager.Inst().IptManager.IsReload = true;
        }
    }
}
