using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpawnManager : MonoBehaviour
{
    public Action<Life[]> OnWaveSpawned;
    public Action OnWavesOver;
    public Action OnWaveEnd;
    [SerializeField] private WaveSpawn[] waviesSpawn;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private TMP_Text numberWaveText;
    [SerializeField] private int delayAfterEndWave = 6;
    private Life[] _currentEnemyLife;
    private bool _isAllEnemiesKilled;
    private LevelManager _levelManager;

    private int _numberWave;
    public int NumberWave { get { return _numberWave; } set { _numberWave = value; } }

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        if (_currentEnemyLife != null)
            _isAllEnemiesKilled = CheckForKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < waviesSpawn.Length; i++)
        {
            yield return new WaitUntil(() => _levelManager.StateGame == LevelManager.State.Game);
            _isAllEnemiesKilled = false;
            numberWaveText.text = "Волна" + " " + (i + 1).ToString();
            _currentEnemyLife = SpawnEnemies(waviesSpawn[i].Enemies);
            OnWaveSpawned?.Invoke(_currentEnemyLife);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
            yield return new WaitForSeconds(delayAfterEndWave);
            OnWaveEnd?.Invoke();
        }
        OnWavesOver?.Invoke();
        numberWaveText.text = "Победа";
    }

    private Life[] SpawnEnemies(EnemySpawn[] enemies)
    {
        var enemy = new List<Life>();

        var numberSpawnPoint = 0;
        for (var i = 0; i < enemies.Length; i++)
        {
            for (var j = 0; j < enemies[i].SpawnCount; j++)
            {
                enemy.Add(Instantiate(enemies[i].Enemy.gameObject, enemySpawnPoints[numberSpawnPoint].position, enemySpawnPoints[numberSpawnPoint].rotation)
                    .GetComponent<Life>());
                numberSpawnPoint++;
                numberSpawnPoint = MathPlus.SawChart(numberSpawnPoint, 0, enemySpawnPoints.Length - 1);
            }
        }
        return enemy.ToArray();
    }

    private bool CheckForKilledEnemies()
    {
        foreach (var enemy in _currentEnemyLife)
            if (!enemy.IsDid) return false;

        return true;
    }
}