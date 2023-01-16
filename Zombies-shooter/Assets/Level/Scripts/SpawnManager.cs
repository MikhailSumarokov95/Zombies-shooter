using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private WaveSpawn[] waviesSpawn;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private TMP_Text numberWaveText;
    private AIBotController[] _currentEnemy;
    private bool _isAllEnemiesKilled;

    private void Start()
    {
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        _isAllEnemiesKilled = CheckForkKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < waviesSpawn.Length; i++)
        {
            _isAllEnemiesKilled = false;
            numberWaveText.text = "Волна" + " " + i.ToString();
            _currentEnemy = SpawnEnemies(waviesSpawn[i].Enemies);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
        }
        numberWaveText.text = "победа";
    }

    private AIBotController[] SpawnEnemies(EnemySpawn[] enemies)
    {
        var enemy = new List<AIBotController>();

        var numberSpawnPoint = 0;
        for (var i = 0; i < enemies.Length; i++)
        {
            for (var j = 0; j < enemies[i].SpawnCount; j++)
            {
                enemy.Add(Instantiate(enemies[i].Enemy.gameObject, enemySpawnPoints[numberSpawnPoint].position, enemySpawnPoints[numberSpawnPoint].rotation)
                    .GetComponent<AIBotController>());
                numberSpawnPoint++;
                numberSpawnPoint = MathPlus.SawChart(numberSpawnPoint, 0, enemySpawnPoints.Length - 1);
            }
        }
        return enemy.ToArray();
    }

    private bool CheckForkKilledEnemies()
    {
        foreach (var enemy in _currentEnemy)
            if (enemy != null) return false;

        return true;
    }
}