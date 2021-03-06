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
        Invoke("NewSpawn", Random.Range(5f, 10f)); //Random
    }

    private void Update()
    {
        if(!spawnedInPath && movement.percentAllPaths + 7 < 100) //Spawn 2 enemies if not reached end
        {
            orgPos = transform.position;
            freeze = true;
            GameObject enemyOne = Instantiate(spawnables[Random.Range(0, spawnables.Count)], allEnemies.transform); //Spawn
            GameObject enemyTwo = Instantiate(spawnables[Random.Range(0, spawnables.Count)], allEnemies.transform);
            EnemyMovement moveOne = enemyOne.GetComponent<EnemyMovement>();
            EnemyMovement moveTwo = enemyTwo.GetComponent<EnemyMovement>();
            moveOne.i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths + 3, movement.pathIndex); //Get waypoint for enemy
            moveTwo.i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths - 3, movement.pathIndex);
            moveOne.pathIndex = movement.pathIndex;
            moveTwo.pathIndex = movement.pathIndex;
            enemyOne.transform.position = EnemyPathMaking.t_Points[moveOne.pathIndex][moveOne.i_waypoitIndex - 1].transform.position; //Set position to waypoint
            enemyTwo.transform.position = EnemyPathMaking.t_Points[moveTwo.pathIndex][moveTwo.i_waypoitIndex - 1].transform.position;
            moveOne.Target();
            moveTwo.Target();
            enemyOne.transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths + 3, movement.pathIndex, enemyOne.transform.rotation); //Set position in % of road
            enemyTwo.transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths - 3, movement.pathIndex, enemyTwo.transform.rotation);
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
