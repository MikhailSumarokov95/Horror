using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToxicFamilyGames
{
    namespace TutorialEditor
    {
        public class Tutorial : MonoBehaviour
        {
            [SerializeField]
            private GameObject[] scenes;
            void Start()
            {
                if (PlayerPrefs.GetInt("tutorial", 1) == 0) gameObject.SetActive(false);
                scenes[currentScene].SetActive(true);
            }

            private int currentScene = 0;
            public void NextScene()
            {
                scenes[currentScene].SetActive(false);
                currentScene++;
                if (currentScene < scenes.Length) scenes[currentScene].SetActive(true);
                PlayerPrefs.SetInt("tutorial", 0);
            }
        }
    }
}
