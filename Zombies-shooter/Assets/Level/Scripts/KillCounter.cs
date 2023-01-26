using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    private int _sumKilled;
    private SpawnManager _spawnManager;

    private int _sumKilledPerWave;
    public int SumKilledPerWave { get { return _sumKilledPerWave; } set { _sumKilledPerWave = value; } }

    private void Start()
    {
        _sumKilled = PlayerPrefs.GetInt("sumKilled", 0);
        if (counterText != null) counterText.text = _sumKilled.ToString();
    }

    private void OnEnable()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _spawnManager.OnWaveSpawned += StartNewWave;
    }

    private void OnDisable()
    {
        _spawnManager.OnWaveSpawned -= StartNewWave;
    }

    public void AddKilled()
    {
        _sumKilled++;
        SumKilledPerWave++;
        if (counterText != null) counterText.text = _sumKilled.ToString();
        PlayerPrefs.SetInt("sumKilled", _sumKilled);
    }

    private void StartNewWave(Life[] enemyLife)
    {
        SumKilledPerWave = 0;
    }
}
