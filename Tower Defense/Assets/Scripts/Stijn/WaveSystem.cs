using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform allEnemies;
    [SerializeField] private EnemySpawning spawnEnemy;
    [SerializeField] private List<EnemiesInWave> waves;
    [SerializeField] private float moneyFromWaves;
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private Image autoStartButton;
    [SerializeField] private GameObject freePlayPanel;
    [SerializeField] private List<GameObject> towers;
    public static bool finishedWave = true;
    private bool finishedSpawning;
    private bool waveDone;
    private bool autoStart;
    private bool freePlay;
    private float floatedDifficulty;
    private int loadedGame;
    private int maxWave = 10;
    private int difficulty = 2;
    private int currentWave = -1;
    private int spawnedEnemies = 0;
    private int totalWaves;
    private int totalEnemiesInWave;
    private SaveSystem saveFiles;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Difficulty"))
            difficulty = 2;
        else
            difficulty = PlayerPrefs.GetInt("Difficulty");

        if (!PlayerPrefs.HasKey("LoadedGame"))
            loadedGame = 1;
        else
            loadedGame = PlayerPrefs.GetInt("LoadedGame");

        saveFiles = GetComponent<SaveSystem>();
        if(loadedGame == 1) //Save file not found
        {
            saveFiles.gameData.difficulty = difficulty;
            UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            saveFiles.gameData.mapID = scene.buildIndex;
            saveFiles.gameData.wave = -1;
        }
        else if(loadedGame == 0) //Save file found
        {
            difficulty = saveFiles.gameData.difficulty;
            currentWave = saveFiles.gameData.wave;
            for (int i = 0; i < saveFiles.gameData.towers.Count; i++)
            {
                GameObject tower = Instantiate(towers[saveFiles.gameData.towers[i].id]);
                tower.transform.position = saveFiles.gameData.towers[i].pos;
            }
        }

        totalWaves = waves.Count;
        switch(difficulty)
        {
            case 1: //Easy
                floatedDifficulty = 0.8f;
                maxWave = 40;
                break;
            case 2: //Normal
                floatedDifficulty = 1f;
                maxWave = 60;
                break;
            case 3: //Hard
                floatedDifficulty = 1.2f;
                maxWave = 80;
                break;
        }
        finishedWave = true;
        wavesText.text = currentWave + 1 + "/" + maxWave;
    }

    private void Update()
    {
        if(finishedSpawning)
            waveDone = allEnemies.childCount == 0;
        if(waveDone && !finishedWave)
        {
            finishedWave = true;

            if(freePlay)
            {
                ManageMoney.GetMoney(Mathf.RoundToInt(moneyFromWaves * maxWave * .025f));
                floatedDifficulty += (currentWave - maxWave) * .001f;
            }
            else
                ManageMoney.GetMoney(Mathf.RoundToInt(moneyFromWaves * (currentWave + 1) * .05f));

            saveFiles.gameData.wave = currentWave;

            saveFiles.SaveGame();

            if (currentWave + 1 == maxWave)
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
            spawnEnemy.SpawnEnemy(waves[currentWave].enemiesInWave[spawnedEnemies].enemy, spawnedEnemies, waves[currentWave].enemiesInWave[spawnedEnemies].pathIndex, floatedDifficulty); //Spawn Enemy
            Invoke(nameof(SpawnEnemy), waves[currentWave].enemiesInWave[spawnedEnemies].timeTillNextSpawn); //Get new enemy after time
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
            autoStart = true;
            autoStartButton.color = Color.green;
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