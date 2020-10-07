using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    private BannerView bannerView;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        //テスト用広告
        //string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //本番用広告
        string adUnitId = "ca-app-pub-4063646496460487/8825489074";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
