using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum State
    {
        Game,
        Pause,
        WaveEnd,
        GameOver
    }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject waveEndPanel;
    [SerializeField] private GameObject lossPanel;
    private SpawnManager _spawnManager;

    [SerializeField] private State _stateGame = State.Game;
    public State StateGame { get { return _stateGame; } set { _stateGame = value; } }

    [SerializeField] private bool isMobile;
    public bool IsMobile { get { return isMobile; } private set { isMobile = value; } }

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
        FindObjectOfType<BattlePassRewarder>(true)?.Awake();
        OnPause(false);
        StateGame = State.Game;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && StateGame == State.Game)
            SetActivePausePanel(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
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
        gameOverPanel.SetActive(value);
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
        OnPause(value);
    }

    private void WinGame()
    {
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(false);
        SetActiveWinPanel(true);
        FindObjectOfType<Level>().NextLevel();
    }

    private void EndWave()
    {
        SetActivePausePanel(false);
        SetActiveWaveEndPanel(true);
    }

    private void OnPause(bool value)
    {
        Time.timeScale = value ? 0 : 1;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}