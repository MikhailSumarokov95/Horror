using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YandexMobileAds;
using YandexMobileAds.Base;
using GoogleMobileAds;
using GoogleMobileAds.Api;
namespace ToxicFamilyGames.AdsAndroid
{
    public class BannerAd : MonoBehaviour
    {
        [SerializeField]
        private bool Google, Yandex;

        [SerializeField]
        private string adGoogleUnitId = "";
        [SerializeField]
        private string adYandexUnitId = "";

        private Banner yandexBanner;
        private BannerView googleBanner;

        public void OnEnable()
        {
            RequestAds();
        }
        public void RequestAds()
        {
            if (PlayerPrefs.GetInt("removeads", 0) == 1) return;
            if (yandexBanner != null)
            {
                yandexBanner.Destroy();
            }

            if (googleBanner != null)
            {
                googleBanner.Destroy();
            }

            if (!Google)
            {
                HandleFailedLoad(null, null);
                return;
            }
            googleBanner = new BannerView(adGoogleUnitId, GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Bottom);
            googleBanner.OnAdFailedToLoad += HandleFailedLoad;
            googleBanner.OnAdLoaded += HandleGoogleLoaded;
            googleBanner.LoadAd(new GoogleMobileAds.Api.AdRequest.Builder().Build());

        }

        #region handle
        private void HanldeYandexLoaded(object sender, EventArgs e)
        {
            print("Яндекс Баннер загружен!");
            yandexBanner.Show();
        }

        private void HandleGoogleLoaded(object sender, EventArgs e)
        {
            print("Google Баннер загружен!");
            googleBanner.Show();
        }

        private void HandleFailedLoad(object sender, AdFailedToLoadEventArgs e)
        {
            if (!Yandex) return;
            yandexBanner = new Banner(adYandexUnitId, YandexMobileAds.Base.AdSize.BANNER_320x50, YandexMobileAds.Base.AdPosition.BottomCenter);
            yandexBanner.OnAdLoaded += HanldeYandexLoaded;
            yandexBanner.LoadAd(new YandexMobileAds.Base.AdRequest.Builder().Build());

        }

        #endregion
    }
}
