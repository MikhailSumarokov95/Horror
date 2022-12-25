using UnityEngine;
using TMPro;

public class EscapeMode : Level
{
    const int NUMBERTOCALCULATEDIFFICULTYLEVEL = 4;
    [SerializeField] private GameObject key;
    [SerializeField] private int[] numberOfKeysToWinDependingOnTheLevelNumber;
    private int _numberFoundKeys;
    private TMP_Text _numberFoundKeysText;
    private int _difficultyLevelNumber;

    private void Awake()
    {
        // сделать пустой объект и в него поместить счетчик и иконку
        _numberFoundKeysText = GameObject.FindGameObjectWithTag("NumberFoundKeysText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        base.Start();
        _difficultyLevelNumber = NumberLevel - NUMBERTOCALCULATEDIFFICULTYLEVEL;
        _numberFoundKeysText.text = "0";
        _map.CreateRandomObjectsOnLevel(key, numberOfKeysToWinDependingOnTheLevelNumber[_difficultyLevelNumber]);
    }

    private void OnDestroy()
    {
        _numberFoundKeysText.text = "";
    }

    [ContextMenu("PickUpKey")]
    public void PickUpKey()
    {
        _numberFoundKeys++;
        _numberFoundKeysText.text = _numberFoundKeys.ToString();
        if (_numberFoundKeys >= numberOfKeysToWinDependingOnTheLevelNumber[_difficultyLevelNumber]) WinLevel(); 
    }
}
