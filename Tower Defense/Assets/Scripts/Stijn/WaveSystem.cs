using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private EnemySpawning spawnEnemy;
    [SerializeField] private List<EnemiesInWave> waves;
    private int currentWave = -1;
    private int spawnedEnemies = 0;
    private int totalWaves;
    private int totalEnemiesInWave;
    private bool finishedWave = true;

    private void Start()
    {
        totalWaves = waves.Count;
    }

    private void SpawnEnemy()
    {
        if(totalEnemiesInWave > spawnedEnemies)
        {
            spawnEnemy.SpawnEnemy(waves[currentWave].enemiesInWave[spawnedEnemies].enemy);
            Invoke("SpawnEnemy", waves[currentWave].enemiesInWave[spawnedEnemies].timeTillNextSpawn);
            spawnedEnemies++;
        }
        else
        {
            finishedWave = true;
        }
    }

    public void StartWaveButton()
    {
        if(finishedWave && currentWave < totalWaves - 1)
        {
            finishedWave = false;
            currentWave++;
            totalEnemiesInWave = waves[currentWave].enemiesInWave.Count;
            spawnedEnemies = 0;
            SpawnEnemy();
        }
    }
}

[System.Serializable]
public class EnemiesInWave
{
    public List<TypeOfEnemy> enemiesInWave;
}

[System.Serializable]
public class TypeOfEnemy
{
    public GameObject enemy;
    public float timeTillNextSpawn;
}