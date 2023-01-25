using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InfimaGames.LowPolyShooterPack;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Image gameOverPanel;
    private SpawnManager _spawnManager;

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
        FindObjectOfType<Movement>().IsBlockedMove = true;
    }
}
