using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsMobile;
    [SerializeField] private GameObject menuRoom;
    [SerializeField] private GameObject winTable;
    [SerializeField] private GameObject lossTable;
    [SerializeField] private GameObject pauseTable;
    [SerializeField] private GameObject gameTable;
    [SerializeField] private GameObject menuTable;
    [SerializeField] private GameObject goPauseButton;
    [SerializeField] private GeneralSetting generalSetting;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private KeyCode keyPause;
    private LevelsCreator _levelCreator;
    private bool _pauseKeyLock;

    public bool IsPause { get; private set; }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        gameInput.Awake();
        generalSetting.LoadSettings();
        _levelCreator = FindObjectOfType<LevelsCreator>();
        goPauseButton.SetActive(IsMobile);
        if (PlayerPrefs.GetInt("guade", 0) == 0)
        {
            _levelCreator.CreateGuadeLevel();
            gameTable.SetActive(true);
        }

        else StartMenu();
    }

    private void Update()
    {
        if (!IsMobile && Input.GetKeyDown(keyPause) && !menuRoom.activeInHierarchy && !_pauseKeyLock) 
            OnPausePanel(!IsPause);
    }

    public void StartMenu()
    {
        _pauseKeyLock = true;
        menuTable.SetActive(true);
        gameTable.SetActive(false);
        _levelCreator.ReturnMenu();
        OnPause(false);
        pauseTable.SetActive(false);
    }

    public void StartLevel()
    {
        _pauseKeyLock = false;
        menuTable.SetActive(false);
        gameTable.SetActive(true);
        OnPause(false);
    }

    public void RestartLevel()
    {
        _pauseKeyLock = false;
        _levelCreator.CreateLevel(_levelCreator.NumberCurrentLevel);
        OnPause(false);
    }

    public void OnWin()
    {
        _pauseKeyLock = true;
        gameTable.SetActive(false);
        winTable.SetActive(true);
        OnPause(true);
        if (!IsMobile) Cursor.lockState = CursorLockMode.None;
    }

    public void OnLoss()
    {
        _pauseKeyLock = true;
        gameTable.SetActive(false);
        lossTable.SetActive(true);
        OnPause(true);
        if (!IsMobile) Cursor.lockState = CursorLockMode.None;
    }

    public void OnPausePanel(bool value)
    {
        pauseTable.SetActive(value);
        OnPause(value);
        if (!IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnPause(bool value)
    {
        IsPause = value;
        Time.timeScale = value ? 0 : 1;
    }
}
