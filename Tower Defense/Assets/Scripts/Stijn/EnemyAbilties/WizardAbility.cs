using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAbility : EnemyMovement
{
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    private GameObject allEnemies;
    private bool freeze;
    private Vector3 orgPos;

    protected override void Start()
    {
        base.Start();
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
        Invoke(nameof(NewSpawn), Random.Range(5f, 10f)); //Random time to spawn enemy
    }

    protected override void Update()
    {
        base.Update();
        if(freeze)
            transform.position = orgPos;
    }

    private void NewSpawn()
    {
        if(percentAllPaths + 7 < 100) //Spawn 2 enemies if not reached end
        {
            for (int i = 0; i < 2; i++)
            {
                int morePercent = i == 0 ? 3 : -3;
                orgPos = transform.position;
                freeze = true;
                GameObject enemy = Instantiate(spawnables[Random.Range(0, spawnables.Count)], allEnemies.transform); //Spawn
                EnemyMovement move = enemy.GetComponent<EnemyMovement>();
                move.i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + morePercent, pathIndex); //Get waypoint for enemy
                move.pathIndex = pathIndex;
                enemy.transform.position = EnemyPathMaking.t_Points[move.pathIndex][move.i_waypoitIndex - 1].transform.position; //Set position to waypoint
                move.Target();
                enemy.transform.position = PercentToPoint.PercentToPath(percentAllPaths + morePercent, pathIndex, enemy.transform.rotation); //Set position in % of road
                enemy.name = "Clone";
            }
            Invoke(nameof(NewSpawn), Random.Range(5f, 10f));
            Invoke(nameof(StopFreeze), 2f);
        }
    }

    private void StopFreeze()
    {
        freeze = false;
    }
}
