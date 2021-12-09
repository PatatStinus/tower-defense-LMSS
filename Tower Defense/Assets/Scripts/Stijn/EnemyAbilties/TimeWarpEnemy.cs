using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEnemy : MonoBehaviour
{
    //BOSS ENEMY
    private GameObject enemyParent;
    private List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
    private EnemyMovement movement;
    private bool doneTime;

    private void Start()
    {
        enemyParent = GameObject.FindGameObjectWithTag("Enemy");
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if(movement.percentAllPaths > 20 && !doneTime)
        {
            StartCoroutine(TimeSkip());
            doneTime = true;
        }
    }

    private IEnumerator TimeSkip()
    {
        movement.divideSpeed = 1000f;
        movement.f_RotateSpeed *= .1f;

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < enemyParent.transform.childCount; i++)
            enemyMovements.Add(enemyParent.transform.GetChild(i).GetComponent<EnemyMovement>());

        for (int i = 0; i < enemyMovements.Count; i++)
        {
            enemyMovements[i].divideSpeed = 1/3f;
            enemyMovements[i].f_RotateSpeed *= 3f;
        }

        movement.divideSpeed = 1f;
        movement.f_RotateSpeed *= 3f + 1/3f;

        yield return null;
    }

    private void OnDisable()
    {
        if(doneTime)
        {
            for (int i = 0; i < enemyMovements.Count; i++)
            {
                if(enemyMovements[i] != null)
                {
                    enemyMovements[i].divideSpeed = 1f;
                    enemyMovements[i].f_RotateSpeed *= 2f;
                }
            }
        }
    }
}
