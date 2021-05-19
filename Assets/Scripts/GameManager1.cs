using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public Text coinNum;

    private void Start()
    {
        coinNum.text = PlayerPrefs.GetInt("coins").ToString();
        TimeFix();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    private async void TimeFix()
    {
        for (int i = 0; i < 16; i++)
        {
            await Task.Delay(250);
            Time.timeScale = 1;
        }
        AdsManager.Instance.HideAds(0);
        AdsManager.Instance.HideAds(1);
        AdsManager.Instance.HideAds(2);
        AdsManager.Instance.HideAds(3);
        AdsManager.Instance.ShowAds(1);
    }
}
