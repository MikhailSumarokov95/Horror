using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] private TMP_Text countCoinsText;
    [SerializeField] private int countCoinForOpedEscapeMode;
    [SerializeField] private LevelsProgress levelsProgress;
    private int countCoins;

    private void Start()
    {
        countCoins = PlayerPrefs.GetInt("coins", 0);
        countCoinsText.text = countCoins.ToString();
    }

    public void ChangeCountCoins(int value)
    {
        countCoins += value;
        countCoinsText.text = countCoins.ToString();
        PlayerPrefs.SetInt("coins", countCoins);
        if (countCoins >= countCoinForOpedEscapeMode) levelsProgress.OpenLevel(4);
    }

    public void PickUpCoin() => ChangeCountCoins(1);
}
