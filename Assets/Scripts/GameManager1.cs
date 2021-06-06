using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static SaveSystem SaveSystem;
    public Text coinNum;
    public GameObject petsPanel;

    private void Start()
    {
        if (SaveSystem == null)
        {
            SaveSystem = new SaveSystem();
        }
        coinNum.text = SaveSystem.coins.ToString();
        AdsManager.Instance.ShowAds(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        petsPanel.SetActive(true);
    }
}
