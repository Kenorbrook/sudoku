using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdsInit : MonoBehaviour
{
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    private const string ACTION_NAME = "rewarded_video";
    public static AdsInit instance; 
    // Start is called before the first frame update
    void Start()
    {
        instance= this;
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        //“≈—“Œ¬¿ﬂ –≈ À¿Ã¿ œŒ«∆≈ «¿ÃÃ≈Õ»“‹
        this.rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        AddEventReward();
    }
    void AddEventReward()
    {
        rewardedAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
    }
    private void RequestBanner()
    {

        string adUnitId = "ca-app-pub-3940256099942544/6300978111";//GameConfig.instance.admob.androidBanner;


        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        
    }


    public void StartRewardVideo()
    {
        Debug.Log("CliclHint");
        if (this.rewardedAd.IsLoaded())
        {
            
            this.rewardedAd.Show();
            
        }
    }


    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log("reward");
        int amount = GameConfig.instance.rewardedVideoAmount;
        GameManager.Hints += amount;
        SudokuManager.intance.UpdateUI();
        Toast.instance.ShowMessage("You've received " + amount + " hints", 2);
        CUtils.SetActionTime(ACTION_NAME);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
}
