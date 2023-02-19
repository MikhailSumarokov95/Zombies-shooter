using UnityEngine;
using UnityEngine.SceneManagement;
using GameScore;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public enum State
    {
        Game,
        Pause,
        WaveEnd,
        GameOver
    }

    [SerializeField] private GameObject winGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject waveEndPanel;
    [SerializeField] private GameObject lossPanel;
    [SerializeField] private GameObject shopBanner;
    [SerializeField] private TMP_Text currentWaveText;
    private SpawnManager _spawnManager;

    [SerializeField] private State _stateGame = State.Game;
    public State StateGame { get { return _stateGame; } set { _stateGame = value; } }

    [SerializeField] private bool isMobile;
    public bool IsMobile { get { return isMobile; } private set { isMobile = value; } }

    private void Awake()
    {
        if (!Application.isEditor) IsMobile = GS_Device.IsMobile();
    }

    private void OnEnable()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _spawnManager.OnWavesOver += WinGame;
        _spawnManager.OnWaveEnd += EndWave;
    }

    private void OnDisable()
    {
        _spawnManager.OnWavesOver -= WinGame;
        _spawnManager.OnWaveEnd -= EndWave;
    }

    private void Start()
    {
        OnPause(false);
        StateGame = State.Game;
        currentWaveText.text = 1.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && StateGame == State.Game)
            SetActivePausePanel(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnPause(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnPause(false);
    }

    public void Did()
    {
        OnPause(true);
        StateGame = State.Pause;
        lossPanel.SetActive(true);
        shopBanner.SetActive(true);
    }

    public void Respawn()
    {
        OnPause(false);
        StateGame = State.Game;
        lossPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Life>().Respawn();
    }

    public void SetActiveWinPanel(bool value)
    {
        if (value) StateGame = State.GameOver;
        else StateGame = State.Game;
        winGamePanel.SetActive(value);
        shopBanner.SetActive(true);
        OnPause(value);
    }

    public void SetActivePausePanel(bool value)
    {
        if (value) StateGame = State.Pause;
        else StateGame = State.Game;
        pausePanel.SetActive(value);
        OnPause(value);
    }

    public void SetActiveWaveEndPanel(bool value)
    {
        if (value) StateGame = State.WaveEnd;
        else StateGame = State.Game;
        waveEndPanel.SetActive(value);
        if (!value) currentWaveText.text = (int.Parse(currentWaveText.text) + 1).ToString();
        OnPause(value);
    }

    public void TryRewardRespawn()
    {
        GSConnect.ShowRewardedAd(GSConnect.ContinueReward);
    }

    private void WinGame()
    {
        GSConnect.ShowMidgameAd();
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(false);
        SetActiveWinPanel(true);
        FindObjectOfType<Level>().NextLevel();
        FindObjectOfType<BattlePassRewarder>(true).RewardPerLevel();
    }

    private void EndWave()
    {
        GSConnect.ShowMidgameAd();
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(true);
    }

    private void OnPause(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        
        if (!IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}