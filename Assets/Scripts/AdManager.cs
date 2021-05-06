using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private BannerView _bannerView;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { RequestBanner();});
       
    }

    private void RequestBanner()
    {
        _bannerView = new BannerView("ca-app-pub-4174137669541969/8894010796", AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
    }

}
