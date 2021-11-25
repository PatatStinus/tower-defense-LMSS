using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform allEnemies;
    [SerializeField] private EnemySpawning spawnEnemy;
    [SerializeField] private List<EnemiesInWave> waves;
    [SerializeField] private float moneyFromWaves;
    public static bool finishedWave = true;
    private bool finishedSpawning;
    private bool waveDone;
    private bool moneyFromWave;
    private int currentWave = -1;
    private int spawnedEnemies = 0;
    private int totalWaves;
    private int totalEnemiesInWave;

    private void Start()
    {
        totalWaves = waves.Count;
    }

    private void Update()
    {
        if(finishedSpawning)
        {
            for (int i = 0; i < allEnemies.childCount; i++)
            {
                if (allEnemies.GetChild(i).gameObject.GetComponent<EnemyMovement>().reachedEnd)
                    waveDone = true;
                else
                {
                    waveDone = false;
                    break;
                }
            }
        }
        if(waveDone || allEnemies.childCount == 0 && finishedSpawning)
        {
            finishedWave = true;
            if(!moneyFromWave)
                ManageMoney.GetMoney(Mathf.RoundToInt(moneyFromWaves * (currentWave + 1 * .05f)));
            moneyFromWave = true;
        }
    }

    private void SpawnEnemy()
    {
        if(totalEnemiesInWave > spawnedEnemies) //If an enemy still needs to be spawned
        {
            spawnEnemy.SpawnEnemy(waves[currentWave].enemiesInWave[spawnedEnemies].enemy, spawnedEnemies, waves[currentWave].enemiesInWave[spawnedEnemies].pathIndex); //Spawn Enemy
            Invoke("SpawnEnemy", waves[currentWave].enemiesInWave[spawnedEnemies].timeTillNextSpawn); //Get new enemy after time
            spawnedEnemies++;
        }
        else
        {
            finishedSpawning = true;
        }
    }

    public void StartWaveButton()
    {
        if(finishedWave && currentWave < totalWaves - 1) //Start Wave
        {
            finishedSpawning = false;
            waveDone = false;
            finishedWave = false;
            currentWave++;
            totalEnemiesInWave = waves[currentWave].enemiesInWave.Count;
            spawnedEnemies = 0;
            ManaManager.enemiesLeft = 0;
            moneyFromWave = false;
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
    public int pathIndex;
}