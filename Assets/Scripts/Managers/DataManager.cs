using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string SaveDataFileName = "SavedData.json";

    public GameData _gameData;
    public GameData GameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadData();
                SaveData();
            }

            return _gameData;
        }
    }

    void Awake()
    {
        DataManager[] objs = FindObjectsOfType<DataManager>();
        if (objs.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

        LoadData();
    }

    void Start()
    {
        _gameData.LoadData();
        SaveData();
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + SaveDataFileName;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            Debug.Log("Load Success");

            string fromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(fromJsonData);

            //GameData.ResetData();
        }
        else
        {
            Debug.Log("Write New File");

            _gameData = new GameData();
            _gameData.ResetData();
        }
    }

    public void SaveData()
    {
        _gameData.SaveData();

        string ToJsonData = JsonUtility.ToJson(_gameData);
        string filePath = Application.persistentDataPath + SaveDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        //Debug.Log("Save Complete");
    }

    void OnApplicationQuit()
    {
        SaveData();
    }
}
