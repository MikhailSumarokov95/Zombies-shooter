using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpawnManager : MonoBehaviour
{
    public Action<Life[]> OnWaveSpawned;
    public Action OnWavesOver;
    [SerializeField] private WaveSpawn[] waviesSpawn;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private TMP_Text numberWaveText;
    private Life[] _currentEnemyLife;
    private bool _isAllEnemiesKilled;

    private void Start()
    {
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        _isAllEnemiesKilled = CheckForKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < waviesSpawn.Length; i++)
        {
            _isAllEnemiesKilled = false;
            numberWaveText.text = "Волна" + " " + (i + 1).ToString();
            _currentEnemyLife = SpawnEnemies(waviesSpawn[i].Enemies);
            OnWaveSpawned?.Invoke(_currentEnemyLife);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
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