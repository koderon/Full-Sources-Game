using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace theGame
{

    public class Admob : TheGameComponent
    {

        // add_mob:    ca-app-pub-4044930938175960~5859163391
        // baner:      ca-app-pub-4044930938175960/7396440491
        // baner_list: ca-app-pub-4044930938175960/7396440491

        private RewardedAd _rewardBasedVideo;
        private BannerView _bannerView;
        private Action<string> OnActionAddRewardVideo = null;

        private string _appId;
        private string _banerId;
        private string _videoId;

        public bool _rewardVideoIsLoaded = false;

        public Action OnActionRewardedUser;

        // Use this for initialization
        void Start()
        { 
        }

        private void TestAdMob(bool test)
        {
            if (!test)
            {
#if UNITY_ANDROID
                _appId = "ca-app-pub-4044930938175960~5859163391"; 
                _banerId = "ca-app-pub-4044930938175960/4415053936";
                _videoId = "ca-app-pub-4044930938175960/7396440491";
#elif UNITY_IPHONE
                _appId = "ca-app-pub-3940256099942544~1458002511";
#else
                _appId = "unexpected_platform";
#endif

                return;
            }

#if UNITY_ANDROID
            _appId = "ca-app-pub-3940256099942544~3347511713";
            _banerId = "ca-app-pub-3940256099942544/6300978111";
            _videoId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            _appId = "ca-app-pub-3940256099942544~1458002511";
#else
            _appId = "unexpected_platform";
#endif
        }

        public override void Init()
        {
            base.Init();
        }

        public void Loading()
        {
            TestAdMob(Startup.TestADS);

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(_appId);

            
        }

        public static void LoadingRewardVideo()
        {
            var ads = TheGame.GetComponent<Admob>();
            if (ads == null)
                return;

            ads.RequestRewardBasedVideo();
            ads.RequestBanner();
        }

        public static void ShowRewardVideo()
        {
            var ads = TheGame.GetComponent<Admob>();
            if (ads == null)
                return;

            ads.ShowRewardedAd();
        }

        public static void AddActionForRewardVideo(Action onActionGetReward)
        {
            var ads = TheGame.GetComponent<Admob>();
            if (ads == null)
                return;

            ads.OnActionRewardedUser = onActionGetReward;
        }

        public static void LoadingBanner()
        {
            var ads = TheGame.GetComponent<Admob>();
            if (ads == null)
                return;

            ads.RequestBanner();
        }

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddKeyword("game")
                .SetGender(Gender.Male)
                .SetBirthday(new DateTime(2000, 1, 1))
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();
        }

        #region Baner

        public void RequestBanner()
        {
            if (_bannerView != null)
                return;

            // Create a 320x50 banner at the top of the screen.
            _bannerView = new BannerView(_banerId, AdSize.Banner, AdPosition.Bottom );
            _bannerView.OnAdLoaded += this.HandleAdLoaded;
            _bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            _bannerView.OnAdOpening += this.HandleAdOpened;
            _bannerView.OnAdClosed += this.HandleAdClosed;
            _bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

            // Load the banner with the request.
            _bannerView.LoadAd(CreateAdRequest());
        }

        public void DestroyBanner()    
        {
            if(_bannerView != null)
                _bannerView.Destroy();

            _bannerView = null;
        }

        #endregion

        #region Reward video

        private void DeleteRewardVideo()
        {
            if (_rewardBasedVideo == null)
                return;

            _rewardBasedVideo.OnAdLoaded -= HandleRewardedAdLoaded;
            _rewardBasedVideo.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
            _rewardBasedVideo.OnAdOpening -= HandleRewardedAdOpening;
            _rewardBasedVideo.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
            _rewardBasedVideo.OnUserEarnedReward -= HandleUserEarnedReward;
            _rewardBasedVideo.OnAdClosed -= HandleRewardedAdClosed;
            
            _rewardVideoIsLoaded = false;
            _rewardBasedVideo = null;
        }

        public void RequestRewardBasedVideo()
        {
            if (_rewardBasedVideo != null)
                return;

            // Create new rewarded ad instance.
            _rewardBasedVideo = new RewardedAd(_videoId);

            // Called when an ad request has successfully loaded.
            _rewardBasedVideo.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            _rewardBasedVideo.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            _rewardBasedVideo.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            _rewardBasedVideo.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            _rewardBasedVideo.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            _rewardBasedVideo.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            var request = CreateAdRequest();
            // Load the rewarded ad with the request.
            _rewardBasedVideo.LoadAd(request);
        }

        public void ShowRewardedAd()
        {
#if UNITY_EDITOR
            OnActionRewardedUser?.Invoke();
#endif

            if (_rewardBasedVideo.IsLoaded())
            {
                _rewardBasedVideo.Show();
            }
        }

        #endregion

        #region Banner callback handlers

        public void HandleAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
        }

        public void HandleAdOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdOpened event received");
        }

        public void HandleAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdClosed event received");

            DestroyBanner();
            RequestBanner();
        }

        public void HandleAdLeftApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLeftApplication event received");

            DestroyBanner();
            RequestBanner();
        }

        #endregion

        #region Interstitial callback handlers

        public void HandleInterstitialLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleInterstitialLoaded event received");
        }

        public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print(
                "HandleInterstitialFailedToLoad event received with message: " + args.Message);
        }

        public void HandleInterstitialOpened(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleInterstitialOpened event received");
        }

        public void HandleInterstitialClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleInterstitialClosed event received");
        }

        public void HandleInterstitialLeftApplication(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleInterstitialLeftApplication event received");
        }

        #endregion

        #region RewardedAd callback handlers

        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            _rewardVideoIsLoaded = true;
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: " + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");

            DeleteRewardVideo();
            RequestRewardBasedVideo();
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            /*
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);*/

            if (OnActionRewardedUser != null)
                OnActionRewardedUser();

            DeleteRewardVideo();
            RequestRewardBasedVideo();
        }

        #endregion
    }
}