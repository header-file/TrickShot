using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickResumeBtn()
    {
        gameObject.SetActive(false);
    }

    public void OnClickRetryBtn()
    {
        GameManager.Inst().StgManager.StartStage();

        gameObject.SetActive(false);
    }

    public void OnClickStageBtn()
    {
        GameManager.Inst().UiManager.Stage.Show();

        gameObject.SetActive(false);
    }

    public void OnClickMainBtn()
    {
        //To Main

        gameObject.SetActive(false);
    }
}
