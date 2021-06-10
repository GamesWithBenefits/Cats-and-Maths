using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static int Score, Coins;
    public static skinData[] Skins = new []{new skinData()};

    static SaveSystem()
    {
        Load();
    }
    
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/player.fun";
        Stream stream = new FileStream(savePath, FileMode.Create);
        
        PlayerData data = new PlayerData {skins = Skins, highScore = Score, coins = Coins};

        formatter.Serialize(stream, data);
        stream.Close();
    }

    static void Load()
    {
        if (!File.Exists(Application.dataPath + "/Save data/Score.secure"))
        {
            Debug.Log("No file");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Save data/Score.secure", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
           
        Score = data.highScore;
        Coins = data.coins;
        Skins = data.skins;
    }
}