using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ToxicFamilyGames.AdsAndroid
{
    public class AdsFinder : MonoBehaviour
    {
        [SerializeField]
        private bool isStartAd = false;
        public UnityEvent[] rewards;

        private AdsMobile ads;
        // Start is called before the first frame update
        void Start()
        {
            ads = GameObject.FindGameObjectWithTag("Ads").GetComponent<AdsMobile>();
            if (isStartAd)
            {
                ads.ShowInterAd();
            }
        }

        public void ShowRewardedAd(int rewardId)
        {
            if (ads != null)
                ads.ShowRewardedAd(rewardId);
        }

        public void ShowInterAd()
        {
            if (ads != null)
                ads.ShowInterAd();
        }
    }
}
