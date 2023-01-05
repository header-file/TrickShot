using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    public Button HintBtn;


    public void OnClickPauseBtn()
    {
        GameManager.Inst().UiManager.Mid.Pause.gameObject.SetActive(true);
    }

    public void OnClickHintBtn()
    {
        GameManager.Inst().UiManager.Mid.Result.IsHintUsed = true;
        GameManager.Inst().HntManager.DrawLine();

        HintBtn.interactable = false;
    }
}
