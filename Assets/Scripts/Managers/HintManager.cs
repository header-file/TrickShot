using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public LineRenderer Line;


    void Start()
    {
        for (int i = 0; i < 2; i++)
            Line.SetPosition(i, GameManager.Inst().Player.transform.position);
    }

    public void DrawLine()
    {
        Line.SetPosition(1, GameManager.Inst().StgManager.Stages[GameManager.Inst().StgManager.CurWorld - 1, GameManager.Inst().StgManager.CurStage - 1].HintPos);
    }

    public void ResetLine()
    {
        Line.SetPosition(1, GameManager.Inst().Player.transform.position);
    }
}
