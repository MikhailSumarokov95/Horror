using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

namespace ToxicFamilyGames.AdsAndroid
{
    public class StartAd : MonoBehaviour
    {
        [SerializeField]
        private string appOpenAdUnitIdGoogle = "";
        private void Start()
        {
            if (PlayerPrefs.GetInt("removeads", 0) == 1) return;
            AppOpenAd.LoadAd(appOpenAdUnitIdGoogle, ScreenOrientation.LandscapeLeft, new AdRequest.Builder().Build(), HandleLoad);
        }

        #region handles
        public void HandleLoad(object sender, EventArgs e)
        {
            print("Реклама при старте загружена!");
        }
        #endregion
    }
}