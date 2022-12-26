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
    [SerializeField] private int amountBattery = 3;
    [SerializeField] private int amountPowerEnginner = 3;
    [SerializeField] private int[] timerTimeDependingOnTheLevelNumber;
    private float _timer;
    private TMP_Text _timerTMP;

    private void Start()
    {
        base.Start();
        // сделать пустой объект и в него поместить счетчик и иконку
        _timerTMP = GameObject.FindGameObjectWithTag("TimerSurvivalMode").GetComponent<TMP_Text>();
        Timer = timerTimeDependingOnTheLevelNumber[NumberLevel];
        _map.CreateRandomObjectsOnLevel(battery, amountBattery);
        _map.CreateRandomObjectsOnLevel(powerEngineer, amountPowerEnginner);
    }

    private void OnDestroy()
    {
        _timerTMP.text = "";
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