using UnityEngine;
using GoogleMobileAds.Api;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    private BannerView _banner1;
    private RewardedAd _rewardedAd;
    private InterstitialAd _interstitial;
    private string[] _adUnitId;
    public static AdsManager Instance;
    [CanBeNull]public GameManager gameManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
#if UNITY_ANDROID
        string[] adUnitId =
        {
            "ca-app-pub-4174137669541969/8894010796",
            "ca-app-pub-4174137669541969/1380151157",
        };
        string rewardAdId = "ca-app-pub-4174137669541969/7905837535";
        string interstitialAdId = "ca-app-pub-4174137669541969/3730519300";
#else
        _adUnitId = "unexpected_platform";
#endif
        _adUnitId = adUnitId;
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            _rewardedAd = new RewardedAd(rewardAdId);
            _rewardedAd.OnUserEarnedReward += (o, args) => {gameManager.IncreaseCoins();};
            _rewardedAd.OnAdLoaded += (o, args) => { _rewardedAd.Show();};

            _interstitial = new InterstitialAd(interstitialAdId);
            _interstitial.OnAdClosed += (o, args) => { gameManager.paused = false;};
            _interstitial.OnAdLoaded += (o, args) => { _interstitial.Show();};
            _interstitial.OnAdFailedToLoad += (o, args) => { gameManager.paused = false;};
        }
    }

    private BannerView RequestBanner(string adUnit, AdPosition position)
    {
        BannerView banner = new BannerView(adUnit, AdSize.Banner, position);
        banner.LoadAd(new AdRequest.Builder().Build());
        banner.OnAdLoaded += (o, args) => { banner.Show();};
        return banner;
    }

    public void HideAds()
    {
        _banner1?.Hide();
                _banner1?.Destroy();
    }

    public void ShowAds(int i)
    {
        switch (i)
        {
            case 0:
                _banner1 = RequestBanner(_adUnitId[0], AdPosition.Bottom);
                _banner1.OnAdLoaded += (o, args) => {
                    if (SceneManager.GetActiveScene().name != "Main Menu")
                    {
                        HideAds();
                    }
                };
                break;
            case 1:
                _banner1 = RequestBanner(_adUnitId[1], AdPosition.Bottom);
                break;
        }
    }

    void OnDestroy()
    {
        _banner1?.Destroy();
        _interstitial?.Destroy();
    }

    public void RewardedAd()
    {
        _rewardedAd.LoadAd(new AdRequest.Builder().Build());
    }

    public void InterstitialAd()
    {
        _interstitial.LoadAd(new AdRequest.Builder().Build());
    }
}
