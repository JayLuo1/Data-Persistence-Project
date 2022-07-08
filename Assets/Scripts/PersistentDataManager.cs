using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance;
    
    public string playerName;
    
    public string highScoreName;
    
    public int highScoreValue;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }
    
    [System.Serializable]
    class SaveData
    {
        public string highScoreName;
        public int highScoreValue;
    }
    
    private string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/savefile.json";
    }
    
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScoreName = highScoreName;
        data.highScoreValue = highScoreValue;

        string json = JsonUtility.ToJson(data);
      
        File.WriteAllText(GetSaveFilePath(), json);
    }
    
    public void LoadHighScore()
    {
        string path = GetSaveFilePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.highScoreName;
            highScoreValue = data.highScoreValue;
        }
        else
        {
            highScoreName = "Nobody";
            highScoreValue = 0;
        }
    }
}
