using System;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Action OnSaveSetting;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider musicVolumeSlider;

    private bool isActiveMusic;
    public bool IsActiveMusic
    {
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

    private float sensitivity;
    public float Sensitivity
    {
        get
        {
            return sensitivity;
        }
        set
        {
            if (value < 0) sensitivity = 0;
            else sensitivity = value;
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
        //PlayerPrefs.SetFloat("musicVolume", musicVolume);
        Progress.SaveSensitivity(Sensitivity);
        OnSaveSetting?.Invoke();
    }

    public void LoadSettings()
    {
        //MusicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolumeSlider.value);
        Sensitivity = Progress.LoadSensitivity();
        musicVolumeSlider.value = MusicVolume;
        sensitivitySlider.value = Sensitivity;
    }
}
