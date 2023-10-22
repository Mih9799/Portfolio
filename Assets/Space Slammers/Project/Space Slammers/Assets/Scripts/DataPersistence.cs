using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class DataPersistence : MonoBehaviour
{
    public int highscore;
    public int score;
    public int kills;
    public int deaths;
    public int bosskills;
    public int powerups;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadFile();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveFile();
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        SaveData data = new SaveData(highscore, kills, deaths, bosskills, powerups);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();

        highscore = data.highscore;
        kills = data.kills;
        deaths = data.deaths;
        bosskills = data.bosskills;
        powerups = data.powerups;

        Debug.Log(data.highscore);
        Debug.Log(data.kills);
        Debug.Log(data.deaths);
        Debug.Log(data.bosskills);
        Debug.Log(data.powerups);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
