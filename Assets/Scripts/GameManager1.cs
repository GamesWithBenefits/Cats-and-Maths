using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static SaveSystem SaveSystem;
    public Text coinNum;
    public GameObject petsPanel;
    public Transform items;

    private void Start()
    {
        if (SaveSystem == null)
        {
            SaveSystem = new SaveSystem();
        }

        items = petsPanel.transform.GetChild(0).GetChild(0);
        coinNum.text = SaveSystem.coins.ToString();
        AdsManager.Instance.ShowAds(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        Debug.Log(petsPanel.activeSelf);
        if (!petsPanel.activeSelf)
        {
            petsPanel.SetActive(true);
            for (int i = 0; i < items.childCount; i++)
            {
                if (SaveSystem.skins[i].bought)
                {
                    items.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    items.GetChild(i).GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    items.GetChild(i).GetChild(2).gameObject.SetActive(false);
                    items.GetChild(i).GetChild(3).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            petsPanel.SetActive(false);
        }
    }

    public void Buy(int skinID)
    {
        if (SaveSystem.skins[skinID].bought) return; 
        SaveSystem.coins -= SaveSystem.skins[skinID].price;
        coinNum.text = SaveSystem.coins.ToString();
        SaveSystem.skins[skinID].bought = true;
        SaveSystem.Save();
        items.GetChild(skinID).GetChild(2).gameObject.SetActive(true);
        items.GetChild(skinID).GetChild(3).gameObject.SetActive(false);
    }
}
