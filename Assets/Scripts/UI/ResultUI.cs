using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject[] Results;
    public Image[] Stars;
    public Color StarBright;
    public Button NextBtn;

    public bool IsHintUsed;


    void Start()
    {
        IsHintUsed = false;
        gameObject.SetActive(false);
    }

    public void Clear()
    {
        Results[0].SetActive(true);
        Results[1].SetActive(false);
        SetStars(CheckStar());

        GameManager.Inst().StgManager.ReaStage++;
        if(GameManager.Inst().StgManager.ReaStage >= Constants.MAX_STAGE)
        {
            GameManager.Inst().StgManager.ReaWorld++;
            GameManager.Inst().StgManager.ReaStage = 1;
        }

        NextBtn.interactable = true;

        gameObject.SetActive(true);
    }

    public void Fail()
    {
        Results[0].SetActive(false);
        Results[1].SetActive(true);
        SetStars(0);

        NextBtn.interactable = false;

        gameObject.SetActive(true);
    }

    int CheckStar()
    {
        int starCount = 1;

        if (!IsHintUsed)
            starCount++;

        if (GameManager.Inst().Player.Cylinder[GameManager.Inst().Player.CurBulletIdx].Type != Bullet.BulletType.NONE)
            starCount++;

        return starCount;
    }

    void SetStars(int Count)
    {
        for(int i = 0; i < Stars.Length; i++)
        {
            if (i < Count)
                Stars[i].color = StarBright;
            else
                Stars[i].color = Color.gray;
        }

        //Save
        GameManager.Inst().DatManager.GameData.SetStar(GameManager.Inst().StgManager.CurWorld, GameManager.Inst().StgManager.CurStage, Count);
    }

    public void OnClickStageBtn()
    {
        GameManager.Inst().UiManager.Stage.Show();

        gameObject.SetActive(false);
    }

    public void OnClickRetryBtn()
    {
        GameManager.Inst().StgManager.StartStage();

        gameObject.SetActive(false);
    }

    public void OnClickNextBnt()
    {
        GameManager.Inst().StgManager.NextStage();

        gameObject.SetActive(false);
    }
}
