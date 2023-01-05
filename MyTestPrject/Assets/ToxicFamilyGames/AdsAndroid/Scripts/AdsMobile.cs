using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using YandexMobileAds;
using System;
using YandexMobileAds.Base;
using UnityEngine.Events;

namespace ToxicFamilyGames.AdsAndroid
{
    public class AdsMobile : MonoBehaviour
    {
        public static AdsMobile instance;

        [Header("Межстраничная Google")]
        [SerializeField]
        private string unitIdGoogleInterAd = "ca-app-pub-3940256099942544/1033173712";
        [Header("Вознаграждение Google")]
        [SerializeField]
        private string unitIdGoogleRewardAd = "ca-app-pub-3940256099942544/5224354917";

        [Header("Межстраничная Yandex")]
        [SerializeField]
        private string unitIdYandexInterAd = "";
        [Header("Вознаграждение Yandex")]
        [SerializeField]
        private string unitIdYandexRewardAd = "";
        [Header("Тип рекламы")]
        [SerializeField]
        private bool isRewardAd;
        [SerializeField]
        private bool isInterAd;

        [Header("Проверка Wifi")]
        [SerializeField]
        private bool checkWifi;
        [SerializeField]
        private GameObject wifiIcon;

        [Space]
        [SerializeField]
        private bool isAdsDelay;
        [SerializeField]
        private float adsDelay = 180;

        public void Start()
        {
            instance = this;
            RequestInterAdGoogle();
            RequestInterAdYandex();
            RequestRewardedAdGoogle();
            RequestRewardedAdYandex();
        }

        #region GoogleInterAd
        private GoogleMobileAds.Api.InterstitialAd interstitialAdGoogle;
        private void RequestInterAdGoogle()
        {
            if (!isInterAd || PlayerPrefs.GetInt("removeads", 0) == 1) return;
            CheckWifi();
            if (interstitialAdGoogle != null)
            {
                interstitialAdGoogle.Destroy();
            }

            interstitialAdGoogle = new GoogleMobileAds.Api.InterstitialAd(unitIdGoogleInterAd);
            interstitialAdGoogle.OnAdLoaded += HanldeOnAdLoadedGoogle;
            interstitialAdGoogle.OnAdFailedToLoad += HanldeOnAdFailedToLoadGoogle;
            interstitialAdGoogle.OnAdOpening += HandleOnAdOpeningGoogle;
            interstitialAdGoogle.OnAdClosed += HandleOnAdClosedGoogle;

            LoadInterAdGoogle();
        }

        private void LoadInterAdGoogle()
        {
            GoogleMobileAds.Api.AdRequest request = new GoogleMobileAds.Api.AdRequest.Builder().Build();
            interstitialAdGoogle.LoadAd(request);
        }
        #endregion
        #region YandexInterAd
        private YandexMobileAds.Interstitial interstitialAdYandex;

        private void RequestInterAdYandex()
        {
            if (!isInterAd || PlayerPrefs.GetInt("removeads", 0) == 1) return;
            CheckWifi();
            if (interstitialAdYandex != null)
            {
                interstitialAdYandex.Destroy();
            }

            interstitialAdYandex = new YandexMobileAds.Interstitial(unitIdYandexInterAd);
            interstitialAdYandex.OnInterstitialLoaded += HandleOnAdLoadedYandex;
            interstitialAdYandex.OnInterstitialFailedToLoad += HanldeOnAdFailedToLoadYandex;
            interstitialAdYandex.OnInterstitialShown += HandleOnAdShownYandex;
            interstitialAdYandex.OnReturnedToApplication += HandleOnAdClosedYandex;

            LoadInterAdYandex();
        }

        private void LoadInterAdYandex()
        {
            YandexMobileAds.Base.AdRequest request = new YandexMobileAds.Base.AdRequest.Builder().Build();
            interstitialAdYandex.LoadAd(request);
        }
        #endregion
        #region GoogleRewardAd

        private GoogleMobileAds.Api.RewardedAd rewardedAdGoogle;
        private void RequestRewardedAdGoogle()
        {
            if (!isRewardAd || PlayerPrefs.GetInt("removeads", 0) == 1) return;
            CheckWifi();
            if (rewardedAdGoogle != null)
            {
                rewardedAdGoogle.Destroy();
            }

            rewardedAdGoogle = new GoogleMobileAds.Api.RewardedAd(unitIdGoogleRewardAd);
            rewardedAdGoogle.OnAdLoaded += HandleRewardedAdLoadedGoogle;
            rewardedAdGoogle.OnAdFailedToLoad += HandleRewardedAdFailedToLoadGoogle;
            rewardedAdGoogle.OnAdOpening += HandleRewardedAdOpeningGoogle;
            rewardedAdGoogle.OnAdFailedToShow += HandleRewardedAdFailedToShowGoogle;
            rewardedAdGoogle.OnUserEarnedReward += HandleRewardedAdUserEarnedRewardGoogle;
            rewardedAdGoogle.OnAdClosed += HandleRewardedAdClosedGoogle;

            LoadRewardedAdGoogle();
        }

        private void LoadRewardedAdGoogle()
        {
            GoogleMobileAds.Api.AdRequest request = new GoogleMobileAds.Api.AdRequest.Builder().Build();
            rewardedAdGoogle.LoadAd(request);
        }

        #endregion
        #region YandexRewardAd

        private YandexMobileAds.RewardedAd rewardedAdYandex;
        private void RequestRewardedAdYandex()
        {
            if (!isRewardAd || PlayerPrefs.GetInt("removeads", 0) == 1) return;
            CheckWifi();
            if (rewardedAdYandex != null)
            {
                rewardedAdYandex.Destroy();
            }

            rewardedAdYandex = new YandexMobileAds.RewardedAd(unitIdYandexRewardAd);
            rewardedAdYandex.OnRewardedAdShown += HandleRewardedAdUserEarnedRewardYandex;
            rewardedAdYandex.OnRewardedAdLoaded += HandleRewardedAdLoadedYandex;
            rewardedAdYandex.OnRewardedAdDismissed += HandleRewardedAdClosedYandex;
            rewardedAdYandex.OnRewardedAdFailedToLoad += HandleRewardedAdFailedToLoadYandex;
            rewardedAdYandex.OnRewardedAdFailedToShow += HandleRewardedAdFailedToShowYandex;

            LoadRewardedAdYandex();
        }

        private void LoadRewardedAdYandex()
        {
            YandexMobileAds.Base.AdRequest request = new YandexMobileAds.Base.AdRequest.Builder().Build();
            rewardedAdYandex.LoadAd(request);
        }

        #endregion
        public void ShowInterAd()
        {
            if (interstitialAdGoogle.IsLoaded())
            {
                interstitialAdGoogle.Show();
                return;
            }

            if (interstitialAdYandex.IsLoaded())
            {
                interstitialAdYandex.Show();
                return;
            }
        }

        private int rewardId;
        public void ShowRewardedAd(int rewardId)
        {
            this.rewardId = rewardId;
            if (rewardedAdGoogle.IsLoaded())
            {
                rewardedAdGoogle.Show();
                return;
            }

            if (rewardedAdYandex.IsLoaded())
            {
                rewardedAdYandex.Show();
                return;
            }
        }

        private void CheckWifi()
        {
            if (checkWifi && !((Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            || (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)))
            {
                if (wifiIcon != null)
                    wifiIcon.SetActive(true);
            }
        }

        private float time = 0;
        private void FixedUpdate()
        {
            if (!isAdsDelay) return;
            time += Time.deltaTime;
            if (time >= adsDelay)
            {
                time = 0;
                ShowInterAd();
            }
        }
        #region HandlesInterAdGoogle

        private void HanldeOnAdLoadedGoogle(object sender, EventArgs e)
        {
            print("Реклама загружена Google!");
        }

        private void HanldeOnAdFailedToLoadGoogle(object sender, GoogleMobileAds.Api.AdFailedToLoadEventArgs e)
        {
            print("Ошибка загрузки гугл рекламы!");
        }

        private void HandleOnAdOpeningGoogle(object sender, EventArgs e)
        {
            print("Реклама Google показана!");
        }

        private void HandleOnAdClosedGoogle(object sender, EventArgs e)
        {
            RequestInterAdGoogle();
        }

        #endregion
        #region HanldesInterAdYandex

        private void HandleOnAdLoadedYandex(object sender, EventArgs e)
        {
            print("Реклама Yandex загружена!");
        }

        private void HanldeOnAdFailedToLoadYandex(object sender, AdFailureEventArgs e)
        {
            print("Ошибка загрузки Yandex рекламы!");
        }

        private void HandleOnAdShownYandex(object sender, EventArgs e)
        {
            print("Реклама Yandex показана!");
        }

        private void HandleOnAdClosedYandex(object sender, EventArgs e)
        {
            RequestInterAdYandex();
        }

        #endregion
        #region HandlesRewardAdGoogle
        private void HandleRewardedAdClosedGoogle(object sender, EventArgs e)
        {
            print("Реклама Google закрыта");
            RequestRewardedAdGoogle();
        }

        private void HandleRewardedAdUserEarnedRewardGoogle(object sender, GoogleMobileAds.Api.Reward e)
        {
            GameObject.FindGameObjectWithTag("Reward").GetComponent<AdsFinder>().rewards[rewardId].Invoke();
            print("Игрок получил вознаграждение Google");
        }

        private void HandleRewardedAdFailedToShowGoogle(object sender, AdErrorEventArgs e)
        {
            print("Ошибка отображения рекламы Google");
        }

        private void HandleRewardedAdOpeningGoogle(object sender, EventArgs e)
        {
            print("Реклама отображается Google");
        }

        private void HandleRewardedAdFailedToLoadGoogle(object sender, AdFailedToLoadEventArgs e)
        {
            print("Ошибка загрузки рекламы Google");
        }

        private void HandleRewardedAdLoadedGoogle(object sender, EventArgs e)
        {
            print("Реклама Google загружена");
        }

        #endregion
        #region HandlesRewardAdYandex
        private void HandleRewardedAdClosedYandex(object sender, EventArgs e)
        {
            print("Реклама Yandex закрыта");
            RequestRewardedAdGoogle();
        }
        private void HandleRewardedAdUserEarnedRewardYandex(object sender, EventArgs e)
        {
            GameObject.FindGameObjectWithTag("Reward").GetComponent<AdsFinder>().rewards[rewardId].Invoke();
            print("Игрок получил вознаграждение Yandex");
        }

        private void HandleRewardedAdFailedToShowYandex(object sender, EventArgs e)
        {
            print("Ошибка отображения рекламы Yandex");
        }

        private void HandleRewardedAdOpeningYandex(object sender, EventArgs e)
        {
            print("Реклама Yandex отображается");
        }

        private void HandleRewardedAdFailedToLoadYandex(object sender, EventArgs e)
        {
            print("Ошибка загрузки рекламы Yandex");
        }

        private void HandleRewardedAdLoadedYandex(object sender, EventArgs e)
        {
            print("Реклама Yandex загружена");
        }

        #endregion
    }
}