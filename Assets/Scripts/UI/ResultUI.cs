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


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Clear()
    {
        Results[0].SetActive(true);
        Results[1].SetActive(false);
        SetStars(CheckStar());

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
        return 3;
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
    }

    public void OnClickStageBtn()
    {
        GameManager.Inst().UiManager.Stage.gameObject.SetActive(true);
    }

    public void OnClickRetryBtn()
    {
        GameManager.Inst().StgManager.StartStage();

        gameObject.SetActive(false);
    }

    public void OnClickNextBnt()
    {
        GameManager.Inst().StgManager.StartStage(1);

        gameObject.SetActive(false);
    }
}
