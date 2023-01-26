using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InfimaGames.LowPolyShooterPack;

public class LevelManager : MonoBehaviour
{
    public enum State
    {
        Game,
        Pause,
        WaveEnd,
        GameOver
    }
    
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image pausePanel;
    [SerializeField] private Image waveEndPanel;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            (StateGame == State.Game || StateGame == State.Pause)) 
            SetActivePausePanel(!(StateGame == State.Pause));
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetActiveWinPanel(bool value)
    {
        gameOverPanel.gameObject.SetActive(value);
        OnPause(value);
    }

    public void SetActivePausePanel(bool value)
    {
        pausePanel.gameObject.SetActive(value);
        OnPause(value);
    }

    public void SetActiveWaveEndPanel(bool value)
    {
        waveEndPanel.gameObject.SetActive(value);
        OnPause(value);
    }

    private void WinGame()
    {
        StateGame = State.GameOver;
        SetActiveWaveEndPanel(false);
        SetActiveWinPanel(true);
    }

    private void EndWave()
    {
        StateGame = State.WaveEnd;
        SetActiveWaveEndPanel(true);
    }

    private void OnPause(bool value)
    {
        if (value) StateGame = State.Pause;
        else StateGame = State.Game;
        Time.timeScale = value ? 0 : 1;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}