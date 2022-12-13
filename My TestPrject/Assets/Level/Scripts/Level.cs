using UnityEngine;
using ToxicFamilyGames.FirstPersonController;

public class Level : MonoBehaviour
{
    public int NumberLevel { get; set; }

    public bool IsGameOver;

    protected Map _map;

    protected void Start()
    {
        _map = FindObjectOfType<Map>();
    }

    public void WinLevel()
    {
        FindObjectOfType<LevelsProgress>().OpenLevel(NumberLevel + 1);
        FindObjectOfType<GameManager>().OnWin();
    }

    public void LossLevel()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        FindObjectOfType<GameManager>().OnLoss();
        FindObjectOfType<Character>().IsBrokenNeck = true;
    }
}
