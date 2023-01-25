using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    private int _sumKilled;

    private void Start()
    {
        _sumKilled = PlayerPrefs.GetInt("sumKilled", 0);
        counterText.text = _sumKilled.ToString();
    }

    public void AddKilled()
    {
        _sumKilled++;
        counterText.text = _sumKilled.ToString();
        PlayerPrefs.SetInt("sumKilled", _sumKilled);
    }
}
