using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAbility : MonoBehaviour
{
    [SerializeField] private List<GameObject> abnormals;
    [SerializeField] private GameObject confusedEffect;
    private List<GameObject> allConfused = new List<GameObject>();
    private GameObject gameManager;
    private GameObject allEnemies;
    private bool isRaining;
    private bool waitingCurse;
    private bool doCurse;
    private EnemyMovement movement;
    private int curse;
    private Vector3 orgPos;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
        movement = GetComponent<EnemyMovement>();
        curse = Random.Range(1, 4);
        GetCurse();
    }

    private void GetCurse()
    {
        switch (curse)
        {
            case 1:
                //Cross
                break;
            case 2:
                //Circle
                break;
            case 3:
                //3 Lines
                break;
        }
    }

    private void DoCurse()
    {
        if(!isRaining)
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

    private void Update()
    {
        if (movement.i_waypoitIndex > EnemyPathMaking.t_Points[movement.pathIndex].Length - 2 && !doCurse)
        {
            doCurse = true;
            Invoke("GetPos", 2f);
            Invoke("StopWaiting", 30f);
            DoCurse();
        }
        if (waitingCurse)
            WaitForCurse();
    }

    private void GetPos()
    {
        orgPos = transform.position;
        WaitForCurse();
    }

    private void WaitForCurse()
    {
        transform.position = orgPos;
        waitingCurse = true;
    }

    private void StopWaiting()
    {
        gameManager.GetComponent<Spells>().enabled = true;
        for (int i = 0; i < allEnemies.transform.childCount; i++)
            allEnemies.transform.GetChild(i).gameObject.GetComponent<EnemyMovement>().isConfused = false;
        for (int i = 0; i < allConfused.Count; i++)
            Destroy(allConfused[i]);
        waitingCurse = false;
    }

    private void SpawnAbnormals()
    {
        gameManager.GetComponent<EnemySpawning>().SpawnEnemy(abnormals[Random.Range(0, abnormals.Count)], 999, Random.Range(0, EnemyPathMaking.t_Points.Count));
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
