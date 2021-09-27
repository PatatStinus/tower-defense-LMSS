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
        if(totalEnemiesInWave > spawnedEnemies) //If an enemy still needs to be spawned
        {
            spawnEnemy.SpawnEnemy(waves[currentWave].enemiesInWave[spawnedEnemies].enemy, spawnedEnemies); //Spawn Enemy
            Invoke("SpawnEnemy", waves[currentWave].enemiesInWave[spawnedEnemies].timeTillNextSpawn); //Get new enemy after time
            spawnedEnemies++;
        }
        else //If all enemies have spawned
        {
            finishedWave = true;
        }
    }

    public void StartWaveButton()
    {
        if(finishedWave && currentWave < totalWaves - 1) //Start Wave
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