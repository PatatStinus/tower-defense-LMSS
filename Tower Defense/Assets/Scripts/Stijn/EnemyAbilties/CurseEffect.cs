using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseEffect : MonoBehaviour
{
    [SerializeField] private List<GameObject> abnormals;
    [SerializeField] private GameObject confusedEffect;
    private List<GameObject> allConfused = new List<GameObject>();
    private GameObject gameManager;
    private GameObject allEnemies;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void DoCurse(int curse, bool isRaining)
    {
        if (!isRaining)
        {
            switch (curse)
            {
                case 1: //Turn off spells 30sec
                    gameManager.GetComponent<Spells>().enabled = false;
                    break;
                case 2: //Double health of all enemies on field
                    for (int i = 0; i < allEnemies.transform.childCount; i++)
                        allEnemies.transform.GetChild(i).gameObject.GetComponent<EnemyHealth>().hp *= 2f;
                    break;
                case 3: //Spawn 3 Random Abnormal(Ability) Enemies
                    SpawnAbnormals();
                    Invoke("SpawnAbnormals", 1f);
                    Invoke("SpawnAbnormals", 2f);
                    break;
            }
        }
        else if (isRaining)
        {
            switch (curse)
            {
                case 1: //Next 10 enemies in wave that make it to the end won't do damage to the unicorn (Curse Enemies won't release their curse)
                    ManaManager.enemiesLeft += 10;
                    break;
                case 2: //Half health of all enemies on field
                    for (int i = 0; i < allEnemies.transform.childCount; i++)
                        allEnemies.transform.GetChild(i).gameObject.GetComponent<EnemyHealth>().hp /= 2f;
                    break;
                case 3: //All enemies on field become confused
                    ConfuseEnemies();
                    break;
            }
        }
    }

    private void OnDisable()
    {
        gameManager.GetComponent<Spells>().enabled = true;
        for (int i = 0; i < allEnemies.transform.childCount; i++)
            allEnemies.transform.GetChild(i).gameObject.GetComponent<EnemyMovement>().isConfused = false;
        for (int i = 0; i < allConfused.Count; i++)
            Destroy(allConfused[i]);
    }

    private void SpawnAbnormals()
    {
        gameManager.GetComponent<EnemySpawning>().SpawnEnemy(abnormals[Random.Range(0, abnormals.Count)], 999, Random.Range(0, EnemyPathMaking.t_Points.Count), 2);
    }

    private void ConfuseEnemies()
    {
        for (int i = 0; i < allEnemies.transform.childCount; i++)
        {
            allEnemies.transform.GetChild(i).gameObject.GetComponent<EnemyMovement>().isConfused = true;
            allConfused.Add(Instantiate(confusedEffect, allEnemies.transform.GetChild(i).transform));
            allConfused[i].transform.position = new Vector3(allEnemies.transform.GetChild(i).transform.position.x, allEnemies.transform.GetChild(i).transform.position.y + 2, allEnemies.transform.GetChild(i).transform.position.z);
        }
    }
}
