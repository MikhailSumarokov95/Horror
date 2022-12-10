using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToxicFamilyGames.AdsAndroid
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);

            if (objects.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this);
        }
    }
}