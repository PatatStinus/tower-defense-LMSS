using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform allEnemies;
    [SerializeField] private EnemySpawning spawnEnemy;
    [SerializeField] private List<EnemiesInWave> waves;
    [SerializeField] private float moneyFromWaves;
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private Image autoStartButton;
    [SerializeField] private GameObject freePlayPanel;
    public static bool finishedWave = true;
    private bool finishedSpawning;
    private bool waveDone;
    private bool moneyFromWave;
    private bool autoStart;
    private bool freePlay;
    private int maxWave = 10;
    private int currentWave = -1;
    private int spawnedEnemies = 0;
    private int totalWaves;
    private int totalEnemiesInWave;

    private void Start()
    {
        totalWaves = waves.Count;
        wavesText.text = currentWave + 1 + "/" + maxWave;
        finishedWave = true;
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
        if(waveDone || allEnemies.childCount == 0 && finishedSpawning && !moneyFromWave)
        {
            finishedWave = true;
            if(!moneyFromWave)
            {
                if(freePlay)
                    ManageMoney.GetMoney(Mathf.RoundToInt(moneyFromWaves * maxWave * .025f));
                else
                    ManageMoney.GetMoney(Mathf.RoundToInt(moneyFromWaves * (currentWave + 1) * .05f));
            }
            moneyFromWave = true;

            if(currentWave + 1 == maxWave)
            {
                Time.timeScale = 1;
                freePlayPanel.SetActive(true);
                freePlay = true;
            }
            
            if (autoStart && currentWave + 1 != maxWave)
                StartWaveButton();
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
            moneyFromWave = false;
            finishedSpawning = false;
            waveDone = false;
            finishedWave = false;
            currentWave++;
            totalEnemiesInWave = waves[currentWave].enemiesInWave.Count;
            if (currentWave + 1 > maxWave)
                wavesText.text = (currentWave + 1).ToString();
            else
                wavesText.text = currentWave + 1 + "/" + maxWave;
            spawnedEnemies = 0;
            ManaManager.enemiesLeft = 0;
            SpawnEnemy();
        }
    }

    public void AutoStart()
    {
        if(autoStart)
        {
            autoStart = false;
            autoStartButton.color = Color.white;
        }
        else
        {
            autoStartButton.color = Color.green;
            autoStart = true;
        }
    }

    public void Continue()
    {
        freePlayPanel.SetActive(false);
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