using UnityEngine;
using ToxicFamilyGames.FirstPersonController;

public class Level : MonoBehaviour
{
    public int NumberLevel { get; set; }
    [SerializeField] private int numberTopCalculatedDifficultyLevel;
    [SerializeField] private float[] VisibilityDistanceMonsterDependingOnTheLevelNumber;
    public bool IsGameOver;
    private BackRoundMusic _backRoundMusic;
    protected Map _map;
    protected int _difficultyLevelNumber;

    protected void Start()
    {
        _difficultyLevelNumber = _difficultyLevelNumber = NumberLevel - numberTopCalculatedDifficultyLevel;
        _map = FindObjectOfType<Map>();
        _backRoundMusic = FindObjectOfType<BackRoundMusic>();
        SetToMonstersVisibilityDistance();
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

    public void SetToMonstersVisibilityDistance()
    {
        FindObjectOfType<Monster>().VisibilityDistance = 
            VisibilityDistanceMonsterDependingOnTheLevelNumber[NumberLevel - numberTopCalculatedDifficultyLevel];
    }

    protected void SetActivateChildTransform(Transform transform, bool value)
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(value);
    }
}
