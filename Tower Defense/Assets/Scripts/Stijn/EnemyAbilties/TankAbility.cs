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
                if (percentAllPaths + evens > 95 || percentAllPaths + odds < 5)
                    continue;

                childEnemies.Add(Instantiate(childSpawns, allEnemies.transform));
                movementEnemies.Add(childEnemies[i].GetComponent<EnemyMovement>());
                movementEnemies[i].pathIndex = pathIndex;

                if(i % 2 == 0) //If enemy index is divisible by 2 do: Spawn enemy in front of tank enemy
                {
                    movementEnemies[i].i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + evens, pathIndex);
                    childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                    movementEnemies[i].Target();
                    childEnemies[i].transform.position = PercentToPoint.PercentToPath(percentAllPaths + evens, pathIndex, childEnemies[i].transform.rotation);
                    evens += 3;
                }
                else //Spawn new enemy at the back of tank enemy
                {
                    if(PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + odds, pathIndex) < 1) //If tank is in backline keep spawning in front
                    {
                        movementEnemies[i].i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + evens, pathIndex);
                        childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                        movementEnemies[i].Target();
                        childEnemies[i].transform.position = PercentToPoint.PercentToPath(percentAllPaths + evens, pathIndex, childEnemies[i].transform.rotation);
                        evens += 3;
                    }
                    else
                    {
                        movementEnemies[i].i_waypoitIndex = PercentToPoint.GetWayPointIndexFromPercent(percentAllPaths + odds, pathIndex);
                        childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                        movementEnemies[i].Target();
                        childEnemies[i].transform.position = PercentToPoint.PercentToPath(percentAllPaths + odds, pathIndex, childEnemies[i].transform.rotation);
                        odds -= 3;
                    }
                }
                childEnemies[i].name = "Child" + i;
            }
        }
    }
}
