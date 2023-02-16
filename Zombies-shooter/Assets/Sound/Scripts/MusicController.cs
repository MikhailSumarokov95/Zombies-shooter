using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private GameObject backGroundPref;
    private AudioSource _backGroundAudioS;
    private Setting _setting;

    private void Start()
    {
        _backGroundAudioS = Instantiate(backGroundPref).GetComponent<AudioSource>();
        _backGroundAudioS.volume = Progress.LoadMusicVolume();

        AudioListener.volume = Progress.LoadVolume();
    }

    private void OnEnable()
    {
        _setting = FindObjectOfType<Setting>(true);
        _setting.OnChangeMusicVolume += ChangeVolumeBackGround;
    }

    private void OnDisable()
    {
        _setting.OnChangeMusicVolume -= ChangeVolumeBackGround;
    }

    private void ChangeVolumeBackGround()
    {
        _backGroundAudioS.volume = Progress.LoadMusicVolume();
    }
}
