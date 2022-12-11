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
            _timerTMP.text = _timer.ToString("F0"); 
        } 
    }

    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject powerEngineer;
    [SerializeField] private GameObject coin;
    [SerializeField] private int amountBattery = 3;
    [SerializeField] private int amountPowerEnginner = 3;
    [SerializeField] private int amountCoin = 3;
    [SerializeField] private int[] timerTimeDependingOnTheLevelNumber;
    private float _timer;
    private TMP_Text _timerTMP;

    private void Start()
    {
        _timerTMP = GameObject.FindGameObjectWithTag("TimerSurvivalMode").GetComponent<TMP_Text>();
        Timer = timerTimeDependingOnTheLevelNumber[NumberLevel];
        _map.CreateRandomObjectsOnLevel(battery, amountBattery);
        _map.CreateRandomObjectsOnLevel(powerEngineer, amountPowerEnginner);
        _map.CreateRandomObjectsOnLevel(coin, amountCoin);
    }

    private void OnDestroy()
    {
        _timerTMP.text = "";
    }

    private void Update()
    {
        if (isGameOver) return;
        if (Timer < 0)
        {
            WinLevel();
            isGameOver = true;
        }
        Timer -= Time.deltaTime;
    }
}