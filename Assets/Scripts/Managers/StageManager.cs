using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public struct BlockData
    {
        public Block.BlockType Type;
        public Vector2 Pos;
        public Vector2 Size;
    }

    public struct StageData
    {
        public List<BlockData> Blocks;
        public Bullet.BulletType[] Cylinders;

        public void Initialize()
        {
            Blocks = new List<BlockData>();
            Cylinders = new Bullet.BulletType[6];
        }
    }

    public StageData[,] Stages;
    public int CurWorld = 1;
    public int CurStage = 1;

    void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Datas/Stage");
        Stages = new StageData[1,10];
        for (int i = 0; i < 1; i++)
            for (int j = 0; j < 10; j++)
                Stages[i, j].Initialize();
        SetMapDatas(data);

        data = CSVReader.Read("Datas/Bullet_Count");
        SetCylinderDatas(data);
    }

    void SetMapDatas(List<Dictionary<string, object>> data)
    {
        int index = 0;

        while (data.Count > index)
        {
            BlockData block = new BlockData();
            block.Type = (Block.BlockType)int.Parse(data[index]["Type"].ToString());
            block.Pos = new Vector2(float.Parse(data[index]["x"].ToString()), float.Parse(data[index]["y"].ToString()));
            block.Size = new Vector2(float.Parse(data[index]["Width"].ToString()), float.Parse(data[index]["Height"].ToString()));

            Stages[int.Parse(data[index]["World"].ToString()) - 1, int.Parse(data[index]["Stage"].ToString()) - 1].Blocks.Add(block);

            index++;
        }
    }

    void SetCylinderDatas(List<Dictionary<string, object>> data)
    {
        int index = 0;

        while(data.Count > index)
        {
            for (int i = 0; i < 6; i++)
                Stages[int.Parse(data[index]["World"].ToString()) - 1, int.Parse(data[index]["Stage"].ToString()) - 1].Cylinders[i] = (Bullet.BulletType)int.Parse(data[index][i.ToString()].ToString());

            index++;
        }
    }

    void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        MapSetting(CurWorld, CurStage);

        GameManager.Inst().Player.SetCylinder();
    }

    public void NextStage()
    {
        CurStage++;
        MapSetting(CurWorld, CurStage);

        GameManager.Inst().Player.SetCylinder();
    }

    public void StartStage(int Stage)
    {
        MapSetting(CurWorld, Stage);

        GameManager.Inst().Player.SetCylinder();
    }

    void MapSetting(int World, int Stage)
    {
        for(int i = 0; i < Stages[World - 1, Stage - 1].Blocks.Count; i++)
        {
            switch(Stages[World - 1, Stage - 1].Blocks[i].Type)
            {
                case Block.BlockType.WALL:
                    Block block = GameManager.Inst().ObjManager.MakeObj("Wall").GetComponent<Block>();
                    block.transform.position = Stages[World - 1, Stage - 1].Blocks[i].Pos;
                    block.SetSize(Stages[World - 1, Stage - 1].Blocks[i].Size);
                    break;

                case Block.BlockType.BREAKABLE:
                    block = GameManager.Inst().ObjManager.MakeObj("Break").GetComponent<Block>();
                    block.transform.position = Stages[World - 1, Stage - 1].Blocks[i].Pos;
                    block.SetSize(Stages[World - 1, Stage - 1].Blocks[i].Size);
                    break;

                case Block.BlockType.MOVABLE:
                    block = GameManager.Inst().ObjManager.MakeObj("Move").GetComponent<Block>();
                    block.transform.position = Stages[World - 1, Stage - 1].Blocks[i].Pos;
                    block.SetSize(Stages[World - 1, Stage - 1].Blocks[i].Size);
                    break;

                case Block.BlockType.SPINNABLE:
                    block = GameManager.Inst().ObjManager.MakeObj("Spin").GetComponent<Block>();
                    block.transform.position = Stages[World - 1, Stage - 1].Blocks[i].Pos;
                    block.SetSize(Stages[World - 1, Stage - 1].Blocks[i].Size);
                    break;

                case Block.BlockType.ELEVATABLE:
                    block = GameManager.Inst().ObjManager.MakeObj("Elevate").GetComponent<Block>();
                    block.transform.position = Stages[World - 1, Stage - 1].Blocks[i].Pos;
                    block.SetSize(Stages[World - 1, Stage - 1].Blocks[i].Size);
                    break;
            }
        }
    }
}
