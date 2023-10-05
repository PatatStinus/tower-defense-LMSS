using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAbility : EnemyMovement
{
    [SerializeField] private GameObject childSpawns;
    [SerializeField] private int totalSpawns;
    private int evens = 3;
    private int odds = -3;
    private List<GameObject> childEnemies = new List<GameObject>();
    private List<EnemyMovement> movementEnemies = new List<EnemyMovement>();
    private GameObject allEnemies;

    protected override void Start()
    {
        base.Start();
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
    }

    protected override void OnDisable() //if Enemy died
    {
        base.OnDisable();
        if(!reachedEnd)
        {
            for (int i = 0; i < totalSpawns; i++)
            {
                if(i % 2 == 0) //If enemy index is divisible by 2 do: Spawn enemy in front of tank enemy
                {
                    if(percentAllPaths + evens < 95) //If tank is in frontline keep spawning in back
                        SpawnFront(i);
                    else if(percentAllPaths + odds > 5)
                        SpawnBack(i);
                    else
                        break;
                }
                else //Spawn new enemy at the back of tank enemy
                {
                    if (percentAllPaths + odds > 5) //If tank is in backline keep spawning in front
                        SpawnBack(i);
                    else if (percentAllPaths + evens < 95)
                        SpawnFront(i);
                    else
                        break;
                }
                childEnemies[i].name = "Child" + i;
            }
        }
    }

    private void SpawnBack(int index)
    {
        childEnemies.Add(Instantiate(childSpawns, allEnemies.transform));
        movementEnemies.Add(childEnemies[index].GetComponent<EnemyMovement>());
        movementEnemies[index].pathIndex = pathIndex;
        movementEnemies[index].i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + odds, pathIndex);
        childEnemies[index].transform.position = EnemyPathMaking.t_Points[movementEnemies[index].pathIndex][movementEnemies[index].i_waypoitIndex - 1].transform.position;
        movementEnemies[index].Target();
        childEnemies[index].transform.position = PercentToPoint.PercentToPath(percentAllPaths + odds, pathIndex, childEnemies[index].transform.rotation);
        odds -= 3;
    }

    private void SpawnFront(int index)
    {
        childEnemies.Add(Instantiate(childSpawns, allEnemies.transform));
        movementEnemies.Add(childEnemies[index].GetComponent<EnemyMovement>());
        movementEnemies[index].pathIndex = pathIndex;
        movementEnemies[index].i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + evens, pathIndex);
        childEnemies[index].transform.position = EnemyPathMaking.t_Points[movementEnemies[index].pathIndex][movementEnemies[index].i_waypoitIndex - 1].transform.position;
        movementEnemies[index].Target();
        childEnemies[index].transform.position = PercentToPoint.PercentToPath(percentAllPaths + evens, pathIndex, childEnemies[index].transform.rotation);
        evens += 3;
    }
}
