using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public Text WorldNumber;
    public GameObject Stage;

    CanvasGroup StageGrid;
    bool IsFade;
    float FadeGyesu;


    void Start()
    {
        IsFade = false;
        FadeGyesu = -1.0f;
        StageGrid = Stage.GetComponent<CanvasGroup>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (IsFade)
            Fading();
    }

    void Fading()
    {
        if (StageGrid.alpha <= 0.0f)
        {
            Show();
            FadeGyesu *= -1.0f;
        }

        StageGrid.alpha += (FadeGyesu / 15.0f);

        Color color = Color.white;
        color.a = StageGrid.alpha;
        WorldNumber.color = color;

        if (StageGrid.alpha >= 1.0f)
        {
            FadeGyesu *= -1.0f;
            IsFade = false;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        int world = GameManager.Inst().StgManager.CurWorld;
        WorldNumber.text = world.ToString();

        EraseStage();
        ShowStage(world);
    }

    void EraseStage()
    {
        for(int i = 0; i < Stage.transform.childCount; i++)
        {
            GameObject slot = Stage.transform.GetChild(i).gameObject;
            slot.transform.parent = GameManager.Inst().ObjManager.UIPool.transform;
            slot.SetActive(false);
        }
    }

    void ShowStage(int World)
    {
        for (int i = 1; i <= Constants.MAXSTAGE; i++)
        {
            if (GameManager.Inst().StgManager.Stages[World - 1, i - 1].Blocks.Count <= 0)
                return;

            GameObject stageSlot = GameManager.Inst().ObjManager.MakeObj("StageSlot");
            stageSlot.transform.parent = Stage.transform;

            StageSlot slot = stageSlot.GetComponent<StageSlot>();
            slot.SetStageNumber(i);

            //º° ¼¼ÆÃ

        }
    }

    public void OnClickNextBtn()
    {
        if (IsFade)
            return;

        GameManager.Inst().StgManager.CurWorld++;

        if (GameManager.Inst().StgManager.CurWorld > Constants.MAXWORLD)
            GameManager.Inst().StgManager.CurWorld = 1;

        IsFade = true;
    }

    public void OnClickPrevBtn()
    {
        if (IsFade)
            return;

        GameManager.Inst().StgManager.CurWorld--;

        if (GameManager.Inst().StgManager.CurWorld <= 0)
            GameManager.Inst().StgManager.CurWorld = Constants.MAXWORLD;

        IsFade = true;
    }
}
