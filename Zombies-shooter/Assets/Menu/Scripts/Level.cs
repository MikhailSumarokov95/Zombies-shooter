using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private TMP_Text currentLevelText;
    public int CurrentLevel { get { return Progress.LoadLevel(); } set { Progress.SaveLevel(value); } }

    private void Start()
    {
        if (currentLevelText != null) 
            currentLevelText.text = CurrentLevel.ToString();
    }

    [ContextMenu("NextLevel")]
    public void NextLevel()
    {
        CurrentLevel++;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
}
