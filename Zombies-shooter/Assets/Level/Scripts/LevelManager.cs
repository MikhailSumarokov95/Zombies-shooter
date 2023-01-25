using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InfimaGames.LowPolyShooterPack;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Image pausePanel;
    private SpawnManager _spawnManager;

    private bool _isPause;

    public bool IsPause { get { return _isPause; } private set { _isPause = value; } }

    [SerializeField] private bool isMobile;
    public bool IsMobile { get { return isMobile; } private set { isMobile = value; } }

    private void OnEnable()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _spawnManager.OnWavesOver += GameOver;
    }

    private void OnDisable()
    {
        _spawnManager.OnWavesOver -= GameOver;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SetPause(!IsPause);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //FindObjectOfType<Movement>().IsBlockedMove = true;
        OnPause(true);
    }

    public void SetPause(bool value)
    {
        pausePanel.gameObject.SetActive(value);
        OnPause(value);
    }

    private void OnPause(bool value)
    {
        IsPause = value;
        Time.timeScale = value ? 0 : 1;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
