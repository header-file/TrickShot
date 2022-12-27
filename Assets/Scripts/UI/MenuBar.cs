using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour
{
    public void OnClickPauseBtn()
    {
        GameManager.Inst().UiManager.Mid.Pause.gameObject.SetActive(true);
    }

    public void OnClickHintBtn()
    {
        GameManager.Inst().UiManager.Mid.Result.IsHintUsed = true;
    }
}
