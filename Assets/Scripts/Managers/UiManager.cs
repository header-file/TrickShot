using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public Down Down;
    public Mid Mid;
    public StageUI Stage;

    public Color[] BulletColors;

    void Start()
    {
        Down.Cylinder.SetUpCylinder();
    }
}
