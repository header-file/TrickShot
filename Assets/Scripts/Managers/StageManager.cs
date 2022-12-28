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

    public struct EnemyData
    {
        public Enemy.EnemyType Type;
        public Vector2 Pos;
    }

    public struct StageData
    {
        public List<BlockData> Blocks;
        public List<EnemyData> Enemies;
        public Bullet.BulletType[] Cylinders;
        public int StarCount;

        public void Initialize()
        {
            Blocks = new List<BlockData>();
            Enemies = new List<EnemyData>();
            Cylinders = new Bullet.BulletType[Constants.MAXCYLINDER];
            StarCount = 0;
        }
    }

    public StageData[,] Stages;
    public int CurWorld = 1;
    public int CurStage = 1;

    void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Datas/Stage");
        Stages = new StageData[Constants.MAXWORLD, Constants.MAXSTAGE];
        for (int i = 0; i < Constants.MAXWORLD; i++)
            for (int j = 0; j < Constants.MAXSTAGE; j++)
                Stages[i, j].Initialize();
        SetMapDatas(data);

        data = CSVReader.Read("Datas/Enemy");
        SetEnemyDatas(data);

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

    void SetEnemyDatas(List<Dictionary<string, object>> data)
    {
        int index = 0;

        while (data.Count > index)
        {
            EnemyData enemy = new EnemyData();
            enemy.Type = (Enemy.EnemyType)int.Parse(data[index]["Type"].ToString());
            enemy.Pos = new Vector2(float.Parse(data[index]["x"].ToString()), float.Parse(data[index]["y"].ToString()));

            Stages[int.Parse(data[index]["World"].ToString()) - 1, int.Parse(data[index]["Stage"].ToString()) - 1].Enemies.Add(enemy);

            index++;
        }
    }

    void SetCylinderDatas(List<Dictionary<string, object>> data)
    {
        int index = 0;

        while(data.Count > index)
        {
            for (int i = 0; i < Constants.MAXCYLINDER; i++)
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
        EnemySetting(CurWorld, CurStage);

        GameManager.Inst().Player.SetCylinder();
    }

    public void NextStage()
    {
        CurStage++;

        StartStage();
    }

    public void StartStage(int Stage)
    {
        CurStage = Stage;

        StartStage();
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

    void EnemySetting(int World, int Stage)
    {
        for (int i = 0; i < Stages[World - 1, Stage - 1].Enemies.Count; i++)
        {
            switch (Stages[World - 1, Stage - 1].Enemies[i].Type)
            {
                case Enemy.EnemyType.RED:
                    Enemy enemy = GameManager.Inst().ObjManager.MakeObj("Red").GetComponent<Enemy>();
                    enemy.transform.position = Stages[World - 1, Stage - 1].Enemies[i].Pos;
                    break;

                case Enemy.EnemyType.GREEN:
                    enemy = GameManager.Inst().ObjManager.MakeObj("Green").GetComponent<Enemy>();
                    enemy.transform.position = Stages[World - 1, Stage - 1].Enemies[i].Pos;
                    break;
            }
        }
    }
}
