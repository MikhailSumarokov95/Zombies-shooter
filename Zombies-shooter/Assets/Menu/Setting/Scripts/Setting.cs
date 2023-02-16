using System;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Action OnSaveSetting;
    public Action OnChangeMusicVolume;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider soundVolumeSlider;
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

    private float soundVolume;
    public float SoundVolume
    {
        get
        {
            return soundVolume;
        }
        set
        {
            if (value < 0) soundVolume = 0;
            else if (value > 1) soundVolume = 1;
            else soundVolume = value;
            AudioListener.volume = soundVolume;
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
            Progress.SaveMusicVolume(musicVolume);
            OnChangeMusicVolume?.Invoke();
        }
    }

    private void OnEnable()
    {
        LoadSettings();
    }

    private void OnDisable()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        Progress.SaveVolume(SoundVolume);
        Progress.SaveSensitivity(Sensitivity);
        OnSaveSetting?.Invoke();
    }

    public void LoadSettings()
    {
        SoundVolume = Progress.LoadVolume();
        MusicVolume = Progress.LoadMusicVolume();
        Sensitivity = Progress.LoadSensitivity();

        soundVolumeSlider.value = SoundVolume;
        musicVolumeSlider.value = MusicVolume;
        sensitivitySlider.value = Sensitivity;
    }
}