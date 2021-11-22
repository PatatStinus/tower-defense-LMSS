using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAbility : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    private GameObject allEnemies;
    private EnemyMovement movement;
    private bool spawnedInPath;
    private bool freeze;
    private Vector3 orgPos;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
        spawnedInPath = true;
        Invoke("NewSpawn", Random.Range(5f, 10f));
    }

    private void Update()
    {
        if(!spawnedInPath)
        {
            orgPos = transform.position;
            freeze = true;
            GameObject enemyOne = Instantiate(spawnables[Random.Range(0, spawnables.Count)], allEnemies.transform);
            GameObject enemyTwo = Instantiate(spawnables[Random.Range(0, spawnables.Count)], allEnemies.transform);
            enemyOne.transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths + 5, movement.pathIndex);
            enemyTwo.transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths - 5, movement.pathIndex);
            EnemyMovement moveOne = enemyOne.GetComponent<EnemyMovement>();
            EnemyMovement moveTwo = enemyTwo.GetComponent<EnemyMovement>();
            moveOne.i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths + 5, movement.pathIndex);
            moveTwo.i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths - 5, movement.pathIndex);
            moveOne.pathIndex = movement.pathIndex;
            moveTwo.pathIndex = movement.pathIndex;
            moveOne.NewTarget(EnemyPathMaking.t_Points[moveOne.pathIndex][moveOne.i_waypoitIndex].position);
            moveOne.NewTarget(EnemyPathMaking.t_Points[moveTwo.pathIndex][moveTwo.i_waypoitIndex].position);
            enemyOne.name = "Clone";
            enemyTwo.name = "Clone";
            spawnedInPath = true;
            Invoke("NewSpawn", Random.Range(5f, 10f));
            Invoke("StopFreeze", 2f);

        }
        if(freeze)
            transform.position = orgPos;
    }

    private void NewSpawn()
    {
        spawnedInPath = false;
    }

    private void StopFreeze()
    {
        freeze = false;
    }
}
