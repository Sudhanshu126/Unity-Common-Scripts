using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Properties
    public static DataManager Instance {  get; private set; }
    public PlayerData _PlayerData { get; private set; }

    //---MonoBehaviour methods---
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }

    //---Class methods---
    //Saves player data
    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(_PlayerData);
        string path = Application.persistentDataPath + "/PlayerData.json";

        File.WriteAllText(path, data);
    }

    //Loads player data
    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/GameData.json";

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            _PlayerData = JsonUtility.FromJson<PlayerData>(data);
        }
        else
        {
            _PlayerData = new PlayerData();
        }
    }
