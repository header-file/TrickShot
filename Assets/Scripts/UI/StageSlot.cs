using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    public Text StageNumber;
    public Image[] Stars;


    public void SetStageNumber(int num) { StageNumber.text = num.ToString(); }

    public void OnClickBtn()
    {
        int stage = int.Parse(StageNumber.text);

        GameManager.Inst().StgManager.StartStage(stage);

        GameManager.Inst().UiManager.Stage.gameObject.SetActive(false);
    }
}
