using UnityEngine;
using UnityEngine.UI;

public class GeneralSetting : MonoBehaviour
{
    [SerializeField] private Slider mobileTurningSpeedSlider;
    [SerializeField] private Slider mobileMusicVolumeSlider;
    [SerializeField] private Slider pcTurningSpeedSlider;
    [SerializeField] private Slider pcMobileMusicVolumeSlider;
    private Slider turningSpeedSlider;
    private Slider musicVolumeSlider;

    private bool isActiveMusic;
    public bool IsActiveMusic { 
        get 
        { 
            return IsActiveMusic;
        } 
        set
        {
            isActiveMusic = value;
            AudioListener.pause = !isActiveMusic;
        } 
    }

    private float turningSpeed;
    public float TurningSpeed 
    {
        get
        {
            return turningSpeed;
        }
        set
        {
            if (value < 0) turningSpeed = 0;
            else turningSpeed = value;
        }
    }

    private float musicVolume;
    public float MusicVolume 
    { 
        get 
        {
            return musicVolume;
        } 
        set
        {
            if (value < 0) musicVolume = 0;
            else if (value > 1) musicVolume = 1;
            else musicVolume = value;
            AudioListener.volume = musicVolume;
        } 
    }

    private void OnDisable()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("turningSpeed", turningSpeed);
    }

    public void LoadSettings()
    {
        PlatformDefinition();
        MusicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolumeSlider.value);
        TurningSpeed = PlayerPrefs.GetFloat("turningSpeed", turningSpeedSlider.value);
        musicVolumeSlider.value = MusicVolume;
        turningSpeedSlider.value = TurningSpeed;
    }

    private void PlatformDefinition()
    {
        if (FindObjectOfType<GameManager>().IsMobile)
        {
            turningSpeedSlider = mobileTurningSpeedSlider;
            musicVolumeSlider = mobileMusicVolumeSlider;
        }

        else
        {
            turningSpeedSlider = pcTurningSpeedSlider;
            musicVolumeSlider = pcMobileMusicVolumeSlider;
        }
    }
}