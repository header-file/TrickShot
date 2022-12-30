using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlot : MonoBehaviour
{
    public Text StageNumber;
    public Image[] Stars;
    public Button Button;


    public void SetStageNumber(int num) { StageNumber.text = num.ToString(); }

    public void SetStars(int count)
    {
        for (int i = 0; i < 3; i++)
            Stars[i].color = Color.gray;

        for(int i = 0; i < count; i++)
            Stars[i].color = GameManager.Inst().UiManager.Mid.Result.StarBright;
    }

    public void OnClickBtn()
    {
        int stage = int.Parse(StageNumber.text);

        GameManager.Inst().StgManager.StartStage(stage);

        GameManager.Inst().UiManager.Stage.gameObject.SetActive(false);
    }
}
