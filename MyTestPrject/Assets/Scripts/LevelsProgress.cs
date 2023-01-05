using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelsProgress : MonoBehaviour
{
    [SerializeField] private Level[] level;
    [SerializeField] private int[] levelsOpenAtBeginningOfGame;

    private void Awake()
    {
        foreach (var numberLevel in levelsOpenAtBeginningOfGame)
            PlayerPrefs.SetInt(level[numberLevel].NameLevels, 1);
    }

    private void Start()
    {
        ActivationOfOpenLevelButtons();
    }

    public void OpenLevel(int levelNumber)
    {
        if (levelNumber >= level.Length) return;
        PlayerPrefs.SetInt(level[levelNumber].NameLevels, 1);
        ActivationOfOpenLevelButtons();
    }

    [ContextMenu("ResetProgress")]
    public void ResetAllProgress()
    {
        for (var i = 1; i < level.Length; i++)
            PlayerPrefs.SetInt(level[i].NameLevels, 0);
        DisableAllLevelButtons();
    }

    private void DisableAllLevelButtons()
    {
        for (var i = 1; i < level.Length; i++)
        {
            level[i].Opened.gameObject.SetActive(false);
            level[i].Closed.gameObject.SetActive(false);
        }
    }

    private void ActivationOfOpenLevelButtons()
    {
        DisableAllLevelButtons();
        foreach (var level in this.level)
            if (PlayerPrefs.GetInt(level.NameLevels, 0) == 0)
                level.Closed.gameObject.SetActive(true);
            else if (PlayerPrefs.GetInt(level.NameLevels, 0) == 1)
                level.Opened.gameObject.SetActive(true);
            else Debug.LogError("Wrong level save loaded");
    }

    [Serializable]
    public class Level
    {
        public string NameLevels;
        public Button Opened;
        public Button Closed;
    }
}