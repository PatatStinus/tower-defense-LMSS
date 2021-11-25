using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAbility : MonoBehaviour
{
    [SerializeField] private GameObject childSpawns;
    [SerializeField] private int totalSpawns;
    private int evens = 3;
    private int odds = -3;
    private List<GameObject> childEnemies = new List<GameObject>();
    private List<EnemyMovement> movementEnemies = new List<EnemyMovement>();
    private GameObject allEnemies;
    private EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnDisable()
    {
        if(!movement.reachedEnd)
        {
            for (int i = 0; i < totalSpawns; i++)
            {
                if (movement.percentAllPaths + evens > 97 || movement.percentAllPaths + odds < 3)
                    break;

                childEnemies.Add(Instantiate(childSpawns, allEnemies.transform));
                movementEnemies.Add(childEnemies[i].GetComponent<EnemyMovement>());
                movementEnemies[i].pathIndex = movement.pathIndex;

                if(i % 2 == 0)
                {
                    movementEnemies[i].i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths + evens, movement.pathIndex);
                    childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                    movementEnemies[i].Target();
                    childEnemies[i].transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths + evens, movement.pathIndex, childEnemies[i].transform.rotation);
                    evens += 3;
                }
                else
                {
                    if(PercentToPoint.WayPointIndex(movement.percentAllPaths + odds, movement.pathIndex) < 1f)
                    {
                        movementEnemies[i].i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths + evens, movement.pathIndex);
                        childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                        movementEnemies[i].Target();
                        childEnemies[i].transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths + evens, movement.pathIndex, childEnemies[i].transform.rotation);
                        evens += 3;
                    }
                    else
                    {
                        movementEnemies[i].i_waypoitIndex = PercentToPoint.WayPointIndex(movement.percentAllPaths + odds, movement.pathIndex);
                        childEnemies[i].transform.position = EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex - 1].transform.position;
                        movementEnemies[i].Target();
                        childEnemies[i].transform.position = PercentToPoint.PercentToPath(movement.percentAllPaths + odds, movement.pathIndex, childEnemies[i].transform.rotation);
                        odds -= 3;
                    }
                }
                childEnemies[i].name = "Child" + i;
            }
        }
    }
}
