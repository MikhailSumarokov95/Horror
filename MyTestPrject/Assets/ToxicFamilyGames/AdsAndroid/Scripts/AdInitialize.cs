using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace ToxicFamilyGames.AdsAndroid
{
    public class AdInitialize : MonoBehaviour
    {
        void Awake()
        {
            MobileAds.Initialize(initStatus =>
            {
                print("Статус инициализации Google рекламы: " + initStatus);
            });
        }
    }
}
