using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private TMP_Text currentLevelText;
    private int _currentLevel = 1;

    private void Start()
    {
        _currentLevel = Progress.LoadLevel();
        currentLevelText.text = _currentLevel.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_currentLevel);
    }
}
