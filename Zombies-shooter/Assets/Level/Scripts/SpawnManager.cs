using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
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
        _isAllEnemiesKilled = CheckForkKilledEnemies();
    }

    private IEnumerator StartWaves()
    {
        for (var i = 0; i < waviesSpawn.Length; i++)
        {
            _isAllEnemiesKilled = false;
            numberWaveText.text = "Волна" + " " + i.ToString();
            _currentEnemyLife = SpawnEnemies(waviesSpawn[i].Enemies);
            yield return new WaitUntil(() => _isAllEnemiesKilled);
        }
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

    private bool CheckForkKilledEnemies()
    {
        foreach (var enemy in _currentEnemyLife)
            if (!enemy.IsDid) return false;

        return true;
    }
}