using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int[] StarCounts;
    public int ReachedWorld;
    public int ReachedStage;


    public void SetStar(int World, int Stage, int Stars) { StarCounts[(World - 1) * Constants.MAX_STAGE + (Stage - 1)] = Stars; }
    public int GetStar(int World, int Stage) { return StarCounts[(World - 1) * Constants.MAX_STAGE + (Stage - 1)]; }

    public void SaveData()
    {
        ReachedWorld = GameManager.Inst().StgManager.ReaWorld;
        ReachedStage = GameManager.Inst().StgManager.ReaStage;
    }

    public void LoadData()
    {
        if (StarCounts == null || StarCounts.Length != Constants.MAX_WORLD * Constants.MAX_STAGE)
            StarCounts = new int[Constants.MAX_WORLD * Constants.MAX_STAGE];

        GameManager.Inst().StgManager.ReaWorld = ReachedWorld;
        GameManager.Inst().StgManager.ReaStage = ReachedStage;
    }

    public void ResetData()
    {
        StarCounts = new int[Constants.MAX_WORLD * Constants.MAX_STAGE];
        ReachedWorld = 1;
        ReachedStage = 1;
    }
}
