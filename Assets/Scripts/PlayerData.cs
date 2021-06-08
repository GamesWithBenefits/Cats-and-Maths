[System.Serializable]
public class PlayerData
{
    public int highScore, coins;
    public skinData[] skins;
}

public class skinData
{
    public int price = 10000;
    public bool bought = false;
}