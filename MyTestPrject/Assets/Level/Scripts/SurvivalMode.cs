using UnityEngine;
using TMPro;

public class SurvivalMode : Level
{
    public float Timer 
    { 
        get 
        { 
            return _timer; 
        } 
        private set 
        {
            _timer = value;
            _timerText.text = _timer.ToString("F0"); 
        } 
    }

    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject powerEngineer;
    [SerializeField] private int amountBattery = 3;
    [SerializeField] private int amountPowerEnginner = 3;
    [SerializeField] private int[] timerTimeDependingOnTheLevelNumber;
    private float _timer;
    private TMP_Text _timerText;
    private GameObject _timerGO;

    private void Start()
    {
        base.Start();
        _timerGO = GameObject.FindGameObjectWithTag("TimerSurvivalMode");
        SetActivateChildTransform(_timerGO.transform, true);
        _timerText = _timerGO.transform.GetChild(0).GetComponent<TMP_Text>();
        _timerText.text = "0";
        Timer = timerTimeDependingOnTheLevelNumber[NumberLevel];
        _map.CreateRandomObjectsOnLevel(battery, amountBattery);
        _map.CreateRandomObjectsOnLevel(powerEngineer, amountPowerEnginner);
    }

    private void OnDestroy()
    {
        SetActivateChildTransform(_timerGO.transform, false);
    }

    private void Update()
    {
        if (IsGameOver) return;
        if (Timer < 0)
        {
            WinLevel();
            IsGameOver = true;
        }
        Timer -= Time.deltaTime;
    }
}