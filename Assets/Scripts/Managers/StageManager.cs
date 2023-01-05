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
        public List<Vector2> TargetPositions;

        public void Initialize()
        {
            TargetPositions = new List<Vector2>();
        }
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
        public Vector2 HintPos;

        public void Initialize()
        {
            Blocks = new List<BlockData>();
            Enemies = new List<EnemyData>();
            Cylinders = new Bullet.BulletType[Constants.MAX_CYLINDER];
        }
    }

    public StageData[,] Stages;
    public int CurWorld;
    public int CurStage;
    public int ReaWorld;
    public int ReaStage;


    void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Datas/Stage");
        Stages = new StageData[Constants.MAX_WORLD, Constants.MAX_STAGE];
        for (int i = 0; i < Constants.MAX_WORLD; i++)
            for (int j = 0; j < Constants.MAX_STAGE; j++)
                Stages[i, j].Initialize();
        SetMapDatas(data);

        data = CSVReader.Read("Datas/Hint");
        SetHintData(data);

        data = CSVReader.Read("Datas/Enemy");
        SetEnemyDatas(data);

        data = CSVReader.Read("Datas/Bullet_Count");
        SetCylinderDatas(data);

        CurWorld = 1;
        CurStage = 1;
        ReaWorld = 1;
        ReaStage = 1;
    }

    void SetMapDatas(List<Dictionary<string, object>> data)
    {
        int index = 0;

        List<Dictionary<string, object>> targetData = CSVReader.Read("Datas/Stage_Movable");
        int moveIndex = 0;

        while (data.Count > index)
        {
            BlockData block = new BlockData();
            block.Initialize();
            block.Type = (Block.BlockType)int.Parse(data[index]["Type"].ToString());
            block.Pos = new Vector2(float.Parse(data[index]["x"].ToString()), float.Parse(data[index]["y"].ToString()));
            block.Size = new Vector2(float.Parse(data[index]["Width"].ToString()), float.Parse(data[index]["Height"].ToString()));

            if(block.Type == Block.BlockType.MOVABLE)
            {
                Vector2 pos = new Vector2(float.Parse(targetData[moveIndex]["x1"].ToString()), float.Parse(targetData[moveIndex]["y1"].ToString()));
                block.TargetPositions.Add(pos);

                pos = new Vector2(float.Parse(targetData[moveIndex]["x2"].ToString()), float.Parse(targetData[moveIndex]["y2"].ToString()));
                block.TargetPositions.Add(pos);

                moveIndex++;
            }

            Stages[int.Parse(data[index]["World"].ToString()) - 1, int.Parse(data[index]["Stage"].ToString()) - 1].Blocks.Add(block);

            index++;
        }
    }

    void SetHintData(List<Dictionary<string, object>> data)
    {
        int index = 0;

        while (data.Count > index)
        {
            Stages[int.Parse(data[index]["World"].ToString()) - 1, int.Parse(data[index]["Stage"].ToString()) - 1].HintPos =
                new Vector2(float.Parse(data[index]["x"].ToString()), float.Parse(data[index]["y"].ToString()));

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
            for (int i = 0; i < Constants.MAX_CYLINDER; i++)
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
        GameManager.Inst().BltManager.ResetLines();
        GameManager.Inst().HntManager.ResetLine();
        GameManager.Inst().UiManager.Mid.Result.IsHintUsed = false;
        GameManager.Inst().UiManager.MenuBar.HintBtn.interactable = true;
        GameManager.Inst().UiManager.Down.Cylinder.ResetCylinder();

        MapSetting(CurWorld, CurStage);
        EnemySetting(CurWorld, CurStage);

        GameManager.Inst().Player.SetCylinder();
    }

    public void NextStage()
    {
        CurStage++;

        if(CurStage > Constants.MAX_STAGE)
        {
            CurStage = 1;
            CurWorld++;
        }

        StartStage();
    }

    public void StartStage(int Stage)
    {
        CurStage = Stage;

        StartStage();
    }

    void MapSetting(int World, int Stage)
    {
        Block[] obj = GameManager.Inst().ObjManager.ObjectPool.GetComponentsInChildren<Block>();
        for (int i = 0; i < obj.Length; i++)
            obj[i].gameObject.SetActive(false);

        for (int i = 0; i < Stages[World - 1, Stage - 1].Blocks.Count; i++)
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

                    Movable move = block.GetComponent<Movable>();
                    move.TargetPositions[0] = Stages[World - 1, Stage - 1].Blocks[i].TargetPositions[0];
                    move.TargetPositions[1] = Stages[World - 1, Stage - 1].Blocks[i].TargetPositions[1];
                    move.SetMoving(true);
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

        Enemy[] e = GameManager.Inst().ObjManager.EnemyPool.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < e.Length; i++)
            e[i].gameObject.SetActive(false);

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
