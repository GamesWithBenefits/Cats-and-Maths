using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public int score, coins;
    public bool[] skins = new Boolean[1];

    public SaveSystem()
    {
        Load();
    }
    
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + "/player.fun";
        Stream stream = new FileStream(savePath, FileMode.Create);
        
        PlayerData data = new PlayerData {skins = skins, highScore = score, coins = coins};

        formatter.Serialize(stream, data);
        stream.Close();
    }

    private void Load()
    {
        if (!File.Exists(Application.dataPath + "/Save data/Score.secure")) return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Save data/Score.secure", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
           
        score = data.highScore;
        coins = data.coins;
        skins = data.skins;
    }
}