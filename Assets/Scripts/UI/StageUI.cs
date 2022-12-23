using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public Text WorldNumber;
    public GameObject Stage;


    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        int world = GameManager.Inst().StgManager.CurWorld;
        WorldNumber.text = world.ToString();

        ShowStage(world);
    }

    void ShowStage(int World)
    {
        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Inst().StgManager.Stages[World - 1, i].Blocks.Count <= 0)
                return;

            GameObject stageSlot = GameManager.Inst().ObjManager.MakeObj("StageSlot");
            stageSlot.transform.parent = Stage.transform;

            StageSlot slot = stageSlot.GetComponent<StageSlot>();
            slot.SetStageNumber(i + 1);

            //º° ¼¼ÆÃ

        }
    }
}
