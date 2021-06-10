using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public Text coinNum;
    public GameObject petsPanel;
    public Transform items;
    public GameObject player;
    
    private void Start()
    {
        items = petsPanel.transform.GetChild(0).GetChild(0);
        coinNum.text = SaveSystem.Coins.ToString();
        AdsManager.Instance.ShowAds(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        if (!petsPanel.activeSelf)
        {
            petsPanel.SetActive(true);
            for (int i = 0; i < items.childCount-1; i++)
            {
                Debug.Log(SaveSystem.Skins[i].bought);
                if (SaveSystem.Skins[i].bought)
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
        if (SaveSystem.Skins[skinID].bought) return; 
        SaveSystem.Coins -= SaveSystem.Skins[skinID].price;
        coinNum.text = SaveSystem.Coins.ToString();
        SaveSystem.Skins[skinID].bought = true;
        SaveSystem.Save();
        items.GetChild(skinID).GetChild(2).gameObject.SetActive(true);
        items.GetChild(skinID).GetChild(3).gameObject.SetActive(false);
    }

    public void Select(int skinID)
    {
        items.GetChild(skinID).GetChild(2).gameObject.GetComponent<Button>().interactable = false;
        player.GetComponent<SpriteRenderer>().sprite = items.GetChild(skinID).GetChild(0).GetComponent<Image>().sprite;
    }
}
