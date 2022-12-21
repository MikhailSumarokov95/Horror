using UnityEngine;
using ToxicFamilyGames.FirstPersonController;

public class Level : MonoBehaviour
{
    public int NumberLevel { get; set; }

    public bool IsGameOver;
    private BackRoundMusic _backRoundMusic;
    protected Map _map;

    protected void Start()
    {
        _map = FindObjectOfType<Map>();
        _backRoundMusic = FindObjectOfType<BackRoundMusic>();
    }

    public void WinLevel()
    {
        FindObjectOfType<LevelsProgress>().OpenLevel(NumberLevel + 1);
        FindObjectOfType<GameManager>().OnWin();
        _backRoundMusic.IsPause = true;
    }

    public void LossLevel()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        FindObjectOfType<GameManager>().OnLoss();
        FindObjectOfType<Character>().IsBrokenNeck = true;
        _backRoundMusic.IsPause = true;
    }
}
