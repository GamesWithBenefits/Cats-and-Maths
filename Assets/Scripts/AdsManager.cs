using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    private BannerView _bannerView;
    private BannerView _banner1, _banner2, _banner3, _banner4;
    private RewardedAd _rewardedAd;
    private InterstitialAd _interstitial;
    private string[] _adUnitId;
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
            "ca-app-pub-3940256099942544/6300978111",
            "ca-app-pub-3940256099942544/6300978111",
            "ca-app-pub-3940256099942544/6300978111",
            "ca-app-pub-3940256099942544/6300978111",
            "ca-app-pub-3940256099942544/6300978111",
            "ca-app-pub-3940256099942544/6300978111"
        };
        string rewardAdId = "ca-app-pub-3940256099942544/5224354917";
        string interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
#else
        _adUnitId = "unexpected_platform";
#endif
        _adUnitId = adUnitId;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            ShowAds(1);
        }
        else
        {
            _rewardedAd = new RewardedAd(rewardAdId);
            _rewardedAd.OnUserEarnedReward += VidHandleUserEarnedReward;
            _rewardedAd.OnAdLoaded += VidHandleLoaded;

            _interstitial = new InterstitialAd(interstitialAdId);
            _interstitial.OnAdClosed += InterstitialHandleOnAdClosed;
            _interstitial.OnAdLoaded += InterstitialHandleOnAdLoaded;
            _interstitial.OnAdFailedToLoad += InterstitialHandleOnAdClosed;
        }
    }

    private BannerView RequestBanner(string adUnit, AdPosition position)
    {
        BannerView banner = new BannerView(adUnit, AdSize.Banner, position);
        banner.LoadAd(new AdRequest.Builder().Build());
        return banner;
    }

    public void HideAds(int i)
    {
        switch (i)
        {
            case 0: _banner1.Hide();
                _banner1.Destroy();
                break;
            case 1: _banner2.Hide();
                _banner2.Destroy();
                break;
            case 2: _banner3.Hide();
                _banner3.Destroy();
                break;
            case 3: _banner4.Hide();
                _banner4.Destroy();
                break;
        }
    }
    
    public void ShowAds(int i)
    {
        switch (i)
        {
            case 0: _banner1 = RequestBanner(_adUnitId[0], AdPosition.Top);
                break;
            case 1: _banner2 = RequestBanner(_adUnitId[1], AdPosition.Bottom);
                break;
            case 2: _banner1 = RequestBanner(_adUnitId[2], AdPosition.Top);
                break;
            case 3: _banner2 = RequestBanner(_adUnitId[3], AdPosition.Bottom);
                break;
            case 4: _banner3 = RequestBanner(_adUnitId[4], AdPosition.Top);
                break;
            case 5: _banner4 = RequestBanner(_adUnitId[5], AdPosition.Bottom);
                break;
        }
    }

    void OnDestroy()
    {
        _banner1?.Destroy();
        _banner2?.Destroy();
        _banner3?.Destroy();
        _banner4?.Destroy();
        _interstitial?.Destroy();
    }

    public void RewardedAd()
    {
        _rewardedAd.LoadAd(new AdRequest.Builder().Build());
    }
    private void VidHandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.IncreaseCoins();
    }
    private void VidHandleLoaded(object sender, EventArgs args)
    {
        _rewardedAd.Show();
    }
    
    public void InterstitialAd()
    {
        _interstitial.LoadAd(new AdRequest.Builder().Build());
    }
    private void InterstitialHandleOnAdLoaded(object sender, EventArgs args)
    {
        _interstitial.Show();
    }

    private void InterstitialHandleOnAdClosed(object sender, EventArgs args)
    {
        ShowAds(2);
        ShowAds(3);
        GameManager.Instance.Pause();
    }
}
