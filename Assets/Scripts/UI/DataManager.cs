using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Text playerName;
    public int playerFinalScore;
    public string bestPlayerName;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /* Serialization is the action of converting complex data into a format in which it can be stored */
    [System.Serializable]
    class SaveData
    {
        public Text playerName;
        public int playerFinalScore;
        public string bestPlayerName;
    }

    public void SaveNameHighScore()
    {
        /* Create a new instance of the SaveData */
        SaveData data = new SaveData();
        data.bestPlayerName = bestPlayerName;
        data.playerFinalScore = playerFinalScore;

        /* Transform the SaveData instance to JSON */
        string json = JsonUtility.ToJson(data);

        /* Write a string to the file: first parameter is the path to the file. The Unity method will give you a folder where you can save data
        that will survive between application reinstall or update and append to it the filename savefile.json
        Second parameter is the text you want to write in the file, here your JSON */
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            playerFinalScore = data.playerFinalScore;
        }
    }
}
