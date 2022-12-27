using UnityEngine;
using TMPro;

public class EscapeMode : Level
{
    [SerializeField] private GameObject key;
    [SerializeField] private int[] numberOfKeysToWinDependingOnTheLevelNumber;
    private int _numberFoundKeys;
    private TMP_Text _numberFoundKeysText;

    private void Awake()
    {
        // сделать пустой объект и в него поместить счетчик и иконку, а лучше GUI
        _numberFoundKeysText = GameObject.FindGameObjectWithTag("NumberFoundKeysText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        base.Start();
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
