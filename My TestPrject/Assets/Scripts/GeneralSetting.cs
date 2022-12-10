using UnityEngine;
using UnityEngine.UI;

public class GeneralSetting : MonoBehaviour
{
    [SerializeField] private Slider turningSpeedSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private bool isActiveMusic;
    public bool IsActiveMusic { 
        get 
        { 
            return IsActiveMusic;
        } 
        set
        {
            isActiveMusic = value;
            AudioListener.pause = isActiveMusic;
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
        MusicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolumeSlider.value);
        TurningSpeed = PlayerPrefs.GetFloat("turningSpeed", turningSpeedSlider.value);
        musicVolumeSlider.value = MusicVolume;
        turningSpeedSlider.value = TurningSpeed;
    }
}