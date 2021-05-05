using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    private BannerView _banner1 = null, _banner2 = null, _banner3 = null, _banner4 = null;
    private RewardedAd _rewardedAd = null;
    private InterstitialAd _interstitial = null;
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
            "ca-app-pub-4174137669541969/8894010796",
            "ca-app-pub-4174137669541969/4755648752",
            "ca-app-pub-4174137669541969/4218067488",
            "ca-app-pub-4174137669541969/6237532646",
            "ca-app-pub-4174137669541969/1380151157",
            "ca-app-pub-4174137669541969/1058741041"
        };
        string rewardAdId = "ca-app-pub-4174137669541969/7905837535";
        string interstitialAdId = "ca-app-pub-4174137669541969/3730519300";
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
            _rewardedAd.OnAdLoaded += (o, args) => { _rewardedAd.Show();};

            _interstitial = new InterstitialAd(interstitialAdId);
            _interstitial.OnAdClosed += InterstitialHandleOnAdClosed;
            _interstitial.OnAdLoaded += (o, args) => { _interstitial.Show();};
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
            case 0:
                _banner1 = RequestBanner(_adUnitId[0], AdPosition.Top);
                _banner1.OnAdLoaded += (o, args) => { _banner1.Show();};
                break;
            case 1:
                _banner2 = RequestBanner(_adUnitId[1], AdPosition.Bottom);
                _banner2.OnAdLoaded += (o, args) => { _banner1.Show();};
                break;
            case 2:
                _banner1 = RequestBanner(_adUnitId[2], AdPosition.Top);
                _banner1.OnAdLoaded += (o, args) => { _banner1.Show();};
                break;
            case 3:
                _banner2 = RequestBanner(_adUnitId[3], AdPosition.Bottom);
                _banner2.OnAdLoaded += (o, args) => { _banner1.Show();};
                break;
            case 4:
                _banner3 = RequestBanner(_adUnitId[4], AdPosition.Top);
                _banner3.OnAdLoaded += (o, args) => { _banner1.Show();};
                break;
            case 5:
                _banner4 = RequestBanner(_adUnitId[5], AdPosition.Bottom);
                _banner4.OnAdLoaded += (o, args) => { _banner1.Show();};
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

    public void InterstitialAd()
    {
        _interstitial.LoadAd(new AdRequest.Builder().Build());
    }

    private void InterstitialHandleOnAdClosed(object sender, EventArgs args)
    {
        ShowAds(2);
        ShowAds(3);
        GameManager.Instance.Pause();
    }
}
