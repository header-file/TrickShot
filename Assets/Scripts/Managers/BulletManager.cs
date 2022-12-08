using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public struct BulletData
    {
        public int BounceCount;
    }

    public LineRenderer Line;

    public BulletData[] BDatas;

    int LineCount;


    void Awake()
    {
        BDatas = new BulletData[4];
        SetBulletData();

        LineCount = 0;
    }

    void SetBulletData()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Datas/BulletData");
        for(int i = 1; i < 4; i++)
        {
            BDatas[i].BounceCount = int.Parse(data[i - 1]["BounceCount"].ToString());
        }
    }

    public void SetLineData(Bullet.BulletType Type)
    {
        LineCount = 0;
        Line.positionCount = BDatas[(int)Type].BounceCount + 1;
        Line.SetPosition(LineCount++, GameManager.Inst().Player.transform.position);
        Line.widthMultiplier = 0.0f;
    }

    public void SetLinePos(Vector2 pos)
    {
        Line.SetPosition(LineCount++, pos);

        if (LineCount >= Line.positionCount)
            ShowLine();
    }

    void ShowLine()
    {
        Line.widthMultiplier = 0.25f;
    }
}
