using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    private BannerView _bannerView;
    private BannerView _banner1, _banner2, _banner3, _banner4;
    private RewardedAd _rewardedAd;
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
            "ca-app-pub-4174137669541969/1634236363",
            "ca-app-pub-4174137669541969/4218067488",
            "ca-app-pub-4174137669541969/6237532646",
            "ca-app-pub-4174137669541969/1380151157",
            "ca-app-pub-4174137669541969/1058741041"
        };
        string rewardAdId = "ca-app-pub-4174137669541969/7905837535";
#else
        _adUnitId = "unexpected_platform";
#endif
        _adUnitId = adUnitId;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            _banner1 = new BannerView(adUnitId[0], AdSize.Banner, AdPosition.Top);
            _banner2 = new BannerView(adUnitId[1], AdSize.Banner, AdPosition.Bottom);
        }
        else
        {
            _rewardedAd = new RewardedAd(rewardAdId);
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
            case 2: _banner3 = RequestBanner(_adUnitId[2], AdPosition.Top);
                break;
            case 3: _banner4 = RequestBanner(_adUnitId[3], AdPosition.Bottom);
                break;
        }
    }

    void OnDestroy()
    {
        _banner1?.Destroy();
        _banner2?.Destroy();
        _banner3?.Destroy();
        _banner4?.Destroy();
    }

    public void RewardedVideo()
    {
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.Show();
    }
    
    private void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.IncreaseCoins();
    }

}
