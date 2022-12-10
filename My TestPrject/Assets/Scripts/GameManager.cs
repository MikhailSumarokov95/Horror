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
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject eyes;
    [SerializeField] private GeneralSetting generalSetting;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private KeyCode keyGoMenu;
    private LevelsCreator levelCreator;

    public bool IsPause { get; private set; }

    private void Start()
    {
        gameInput.Awake();
        generalSetting.LoadSettings();
        levelCreator = FindObjectOfType<LevelsCreator>();
        goPauseButton.SetActive(IsMobile);
        StartMenu();
    }

    private void Update()
    {
        if (!IsMobile && Input.GetKeyDown(keyGoMenu) && !menuRoom.activeInHierarchy) 
            OnPausePanel(!IsPause);
    }

    public void StartMenu()
    {
        menuTable.SetActive(true);
        gameTable.SetActive(false);
        flashlight.SetActive(false);
        eyes.SetActive(false);
        OnPausePanel(false);
    }

    public void StartLevel()
    {
        menuTable.SetActive(false);
        gameTable.SetActive(true);
        flashlight.SetActive(true);
        eyes.SetActive(true);
        OnPause(false);
    }

    public void RestartLevel()
    {
        levelCreator.CreateLevel(levelCreator.NumberCurrentLevel);
        OnPause(false);
    }

    public void OnWin()
    {
        gameTable.SetActive(false);
        winTable.SetActive(true);
        OnPause(true);
    }

    public void OnLoss()
    {
        gameTable.SetActive(false);
        lossTable.SetActive(true);
        OnPause(true);
    }

    public void OnPausePanel(bool value)
    {
        pauseTable.SetActive(value);
        OnPause(value);
    }

    private void OnPause(bool value)
    {
        IsPause = value;
        Time.timeScale = value ? 0 : 1;
        if (!IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
