using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using TMPro;

namespace ExtremeBalls
{
    public class GoogleAds : MonoBehaviour
    {
        public GameManager gameManager;
        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardBasedVideoAd rewardVideo;

        public GameObject RewardPlayer;
        public GameObject noVideo;
        public TextMeshPro Text;
        private int opt = 0;
        public bool ballShop = false;
        public bool gamePlay = false;

        private bool videoWatched = false;

        public void Start()
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-6462995892334491~8830862545";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#endif
            MobileAds.Initialize(appId);

            this.rewardVideo = RewardBasedVideoAd.Instance;

            this.RequestRewardBasedVideo();
            this.rewardVideo = RewardBasedVideoAd.Instance;

            RequestRewardBasedVideo();
            RequestBanner();
            RequestInterstitial();

            ShowBannerAd();
        }

        #region Request Ads
        private void RequestBanner()
        {
#if UNITY_ANDROID
            //string BannerAdId = "ca-app-pub-6462995892334491/3557945447"; //Real One
            string BannerAdIdTest = "ca-app-pub-3940256099942544/6300978111"; //Test One

#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

            bannerView = new BannerView(BannerAdIdTest, AdSize.SmartBanner, AdPosition.Bottom);

            //AdRequest request = new AdRequest.Builder().Build(); //Real One
            AdRequest requestTest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //Test One  

            bannerView.LoadAd(requestTest);
        }


        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            //string InterstitialAdId = "ca-app-pub-6462995892334491/2772720651"; //Real One
            string InterstitialAdIdTest = "ca-app-pub-3940256099942544/1033173712"; //Test One

#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#endif

            interstitial = new InterstitialAd(InterstitialAdIdTest);

            //AdRequest request = new AdRequest.Builder().Build(); //Real One
            AdRequest requestTest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //Test One

            interstitial.LoadAd(requestTest);
        }

        private void RequestRewardBasedVideo()
        {
#if UNITY_ANDROID
            //string RewardVideo = "ca-app-pub-6462995892334491/4959900291"; // Real One
            string RewardVideoTest = "ca-app-pub-3940256099942544/5224354917"; //Test One
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif

            //AdRequest request = new AdRequest.Builder().Build(); //Real One
            AdRequest requestTest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //Test One

            rewardVideo.LoadAd(requestTest, RewardVideoTest);
        }

        #endregion

        public void ShowBannerAd()
        {
            bannerView.Show();
        }

        public void ShowIntersitialAd()
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }

        public void ShowRewardVideo()
        {
            if (rewardVideo.IsLoaded())
            {
                rewardVideo.Show();
                rewardVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            }

            if (!rewardVideo.IsLoaded())
            {
                if (ballShop == true)
                {
                    RewardPlayer.SetActive(true);
                    Text.SetText("Unable to load video, try again");
                    opt = 2;
                }

                if (gamePlay == true)
                {
                    noVideo.SetActive(true);
                }
            }
        }


        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            ShowBannerAd();
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            RequestBanner();
            RequestInterstitial();
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Opened");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("Ad Closed");
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeavingApplication event received");
        }


        #region Video

        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
            ShowRewardVideo();
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            RequestRewardBasedVideo();
        }

        public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
        }

        public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {

        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " Coins " + type);

            VideoFinished();
        }

        public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
        {
            RequestRewardBasedVideo();
        }
        #endregion

        #region Handlers

        void HandleAdBanner(bool subscribe)
        {
            if (subscribe)
            {
                bannerView.OnAdLoaded += HandleOnAdLoaded;
                bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                bannerView.OnAdOpening += HandleOnAdOpened;
                bannerView.OnAdClosed += HandleOnAdClosed;
                bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
            }
            else
            {
                bannerView.OnAdLoaded -= HandleOnAdLoaded;
                bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
                bannerView.OnAdOpening -= HandleOnAdOpened;
                bannerView.OnAdClosed -= HandleOnAdClosed;
                bannerView.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
            }
        }

        void HandleAdIntersitial(bool subscribe)
        {
            if (subscribe)
            {
                interstitial.OnAdLoaded += HandleOnAdLoaded;
                interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                interstitial.OnAdOpening += HandleOnAdOpened;
                interstitial.OnAdClosed += HandleOnAdClosed;
                interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
            }
            else
            {
                interstitial.OnAdLoaded -= HandleOnAdLoaded;
                interstitial.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
                interstitial.OnAdOpening -= HandleOnAdOpened;
                interstitial.OnAdClosed -= HandleOnAdClosed;
                interstitial.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
            }
        }

        void HandleAdVideoReward(bool subscribe)
        {
            if (subscribe)
            {
                rewardVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
                rewardVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
                rewardVideo.OnAdOpening += HandleRewardBasedVideoOpened;
                rewardVideo.OnAdStarted += HandleRewardBasedVideoStarted;
                rewardVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
                rewardVideo.OnAdClosed += HandleRewardBasedVideoClosed;
                rewardVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
            }
            else
            {
                rewardVideo.OnAdLoaded -= HandleRewardBasedVideoLoaded;
                rewardVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
                rewardVideo.OnAdOpening -= HandleRewardBasedVideoOpened;
                rewardVideo.OnAdStarted -= HandleRewardBasedVideoStarted;
                rewardVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
                rewardVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
                rewardVideo.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
            }
        }

        private void OnEnable()
        {
            HandleAdBanner(true);
            HandleAdIntersitial(true);
            HandleAdVideoReward(true);
        }

        private void OnDisable()
        {
            HandleAdBanner(true);
            HandleAdIntersitial(false);
            HandleAdVideoReward(false);
        }

        #endregion

        public void RecieveRewards()
        {
            if (opt == 1)
            {
                int coins = PlayerPrefs.GetInt("Your Coins");
                coins = coins + 15;
                PlayerPrefs.SetInt("Your Coins", coins);
                RewardPlayer.SetActive(false);
                opt = 0;
            }
            else if (opt == 2 || opt == 0)
            {
                RewardPlayer.SetActive(false);
                opt = 0;
            }

            RequestRewardBasedVideo();
        }

        void VideoFinished()
        {
            if (ballShop == true)
            {
                RewardPlayer.SetActive(true);
                Text.SetText("You Just Recieved 15 Coins");
                opt = 1;
            }

            if (gamePlay == true)
            {
                 gameManager.WatchedVideo();
            }

            RequestRewardBasedVideo();
        }

        public void noVideoOk()
        {
            noVideo.SetActive(false);
            RequestRewardBasedVideo();
        }
    }
}


