using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    private BannerView _bannerView;
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

    private void Start()
    {
        _bannerView = new BannerView("ca-app-pub-4174137669541969/8894010796", AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
        _bannerView.Show();
    }

    private void OnDestroy()
    {
        _bannerView?.Destroy();
    }
}
