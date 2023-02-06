using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public Action<Life[]> OnWaveSpawned;
    public Action OnWavesOver;
    public Action OnWaveEnd;
    [SerializeField] private int delayAfterEndWave = 6;
    [SerializeField] private EnemySpawn[] enemiesSpawn;
    [SerializeField] private AIBotController boss;
    [SerializeField] private int countWave = 3;
    [SerializeField] private int plusEnemyWithLevel = 1;
    [SerializeField] private Transform[] enemySpawnPoints;
    private Life[] _currentEnemyLife;
    private bool _isAllEnemiesKilled;
    private LevelManager _levelManager;
    private Level _level;

    private int _numberWave = 0;
    public int NumberWave { get { return _numberWave; } private set { _numberWave = value; } }

    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _level = FindObjectOfType<Level>();
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        if (_currentEnemyLife != null)
            _isAllEnemiesKilled = CheckForKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < countWave; i++)
        {
            yield return new WaitUntil(() => _levelManager.StateGame == LevelManager.State.Game);
            NumberWave ++;
            _isAllEnemiesKilled = false;
            _currentEnemyLife = SpawnEnemies(enemiesSpawn);

            OnWaveSpawned?.Invoke(_currentEnemyLife);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
            yield return new WaitForSeconds(delayAfterEndWave);
            OnWaveEnd?.Invoke();
        }
        OnWavesOver?.Invoke();
    }

    private Life[] SpawnEnemies(EnemySpawn[] enemies)
    {
        var enemy = new List<Life>();

        var numberSpawnPoint = 0;
        for (var i = 0; i < enemies.Length; i ++)
        {
            for (var j = 0; j < enemies[i].SpawnCount + _level.CurrentLevel * plusEnemyWithLevel; j++)
            {
                enemy.Add(Instantiate(enemies[i].Enemy.gameObject, enemySpawnPoints[numberSpawnPoint].position, enemySpawnPoints[numberSpawnPoint].rotation)
                    .GetComponent<Life>());
                numberSpawnPoint++;
                numberSpawnPoint = MathPlus.SawChart(numberSpawnPoint, 0, enemySpawnPoints.Length - 1);
            }
        }
        numberSpawnPoint++;

        if (NumberWave == countWave)
            enemy.Add(SpawnBoss(numberSpawnPoint));

        return enemy.ToArray();
    }

    private Life SpawnBoss(int numberPointSpawn)
    { 
        return Instantiate(boss.gameObject, enemySpawnPoints[numberPointSpawn].position, enemySpawnPoints[numberPointSpawn].rotation)
                    .GetComponent<Life>();
    }

    private bool CheckForKilledEnemies()
    {
        foreach (var enemy in _currentEnemyLife)
            if (!enemy.IsDid) return false;

        return true;
    }
}