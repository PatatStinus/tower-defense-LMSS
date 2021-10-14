using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAbility : MonoBehaviour
{
    [SerializeField] private GameObject childSpawns;
    [SerializeField] private int totalSpawns;
    private int evens = 1;
    private int odds = 1;
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
                childEnemies.Add(Instantiate(childSpawns, allEnemies.transform));
                if(i % 2 == 0)
                {
                    childEnemies[i].transform.position = transform.forward * evens * 2 + transform.position;
                    evens++;
                }
                else
                {
                    childEnemies[i].transform.position = -transform.forward * odds * 2 + transform.position;
                    odds++;
                }
                childEnemies[i].name = "Child" + i;
                movementEnemies.Add(childEnemies[i].GetComponent<EnemyMovement>());
                movementEnemies[i].i_waypoitIndex = movement.i_waypoitIndex;
                movementEnemies[i].pathIndex = movement.pathIndex;
                movementEnemies[i].NewTarget((EnemyPathMaking.t_Points[movementEnemies[i].pathIndex][movementEnemies[i].i_waypoitIndex].position));
            }
        }
    }
}
