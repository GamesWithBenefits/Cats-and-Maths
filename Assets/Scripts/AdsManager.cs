using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using ShowResult = UnityEngine.Advertisements.ShowResult;

public class AdsManager : MonoBehaviour,IUnityAdsListener
{
    public static AdsManager Instance;
    private string GameID = "4105625";
    private bool testMode = false;

    private void Awake()
    {
        Advertisement.Initialize(GameID, testMode);
        Advertisement.AddListener(this);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Advertisement.Load("Pause_Banner");
        Advertisement.Load("GameOver_Banner");
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            ShowAds("MainMenu_Banner");
        }
    }

    public void ShowAds(string adID)
    {
        StartCoroutine(AdEnum(adID));
    }

    IEnumerator AdEnum(string adID)
    {
        while (!Advertisement.IsReady(adID))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(adID);
    }

    public void HideAds()
    {
        Advertisement.Banner.Hide();
    }

    public void RewardedVideo()
    {
        StartCoroutine(RewardEnum());
    }

    IEnumerator RewardEnum()
    {
        while (!Advertisement.IsReady("rewardedVideo"))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsReady(string placementId)
    {
        // Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        // Advertisement.Banner.Show(placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId=="rewardedVideo" && showResult==ShowResult.Finished)
        {
            Debug.Log("1");
            GameManager.Instance.IncreaseCoins();
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // throw new System.NotImplementedException();
    }
}
