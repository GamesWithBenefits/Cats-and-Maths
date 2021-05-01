using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    private BannerView _bannerView;
    private List<BannerView> _banners = new List<BannerView>();
    private RewardedAd _rewardedAd;
    
    public static AdsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
#if UNITY_ANDROID
        string[] adUnitId =
        {
            "ca-app-pub-4174137669541969/8894010796",
            "ca-app-pub-4174137669541969/7805765180",
            "ca-app-pub-4174137669541969/4218067488",
            "ca-app-pub-4174137669541969/6237532646",
            "ca-app-pub-4174137669541969/1380151157",
            "ca-app-pub-4174137669541969/1058741041"
        };
        string rewardAdId = "ca-app-pub-4174137669541969/7905837535";
#else
        _adUnitId = "unexpected_platform";
#endif
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            _banners.Add(RequestBanner(adUnitId[0], AdPosition.Bottom));
        }
        else
        {
            _banners.Add(RequestBanner(adUnitId[2], AdPosition.TopRight));
            _banners.Add(RequestBanner(adUnitId[3], AdPosition.BottomLeft));
            _banners.Add(RequestBanner(adUnitId[4], AdPosition.Bottom));
            _banners.Add(RequestBanner(adUnitId[5], AdPosition.BottomRight));
            _rewardedAd = new RewardedAd(rewardAdId);
        }
    }

    private BannerView RequestBanner(string adUnitId, AdPosition position)
    {
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, position);
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
        return bannerView;
    }

    public void HideAds(int i)
    {
        for (int j = 2*i; j < 2*i+2; j++)
        {
            _banners[j].Hide();
        }
    }
    
    public void ShowAds(int i)
    {
        for (int j = 2*i; j < 2*i+2; j++)
        {
            _banners[j].Show();
        }
    }

    public void Cleanup()
    {
        for (int j = 0; j < 6; j++)
        {
            _banners[j].Destroy();
        }
    }

    public void RewardedVideo()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
    }
}
