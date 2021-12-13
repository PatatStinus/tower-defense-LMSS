using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEnemy : MonoBehaviour
{
    //BOSS ENEMY
    [SerializeField] private float timeSkipValue = 1f;
    [SerializeField] private GameObject timeSkipEffect;
    private GameObject canvas;
    private GameObject enemyParent;
    private List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
    private EnemyMovement movement;
    private bool doneTime;

    private void Start()
    {
        enemyParent = GameObject.FindGameObjectWithTag("Enemy");
        movement = GetComponent<EnemyMovement>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
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
        float orgRot = movement.f_RotateSpeed;
        movement.f_RotateSpeed = 0f;

        yield return new WaitForSeconds(3f);

        GameObject effect = Instantiate(timeSkipEffect, canvas.transform);

        for (int i = 0; i < enemyParent.transform.childCount; i++)
            enemyMovements.Add(enemyParent.transform.GetChild(i).GetComponent<EnemyMovement>());

        for (int i = 0; i < enemyMovements.Count; i++)
        {
            enemyMovements[i].i_waypoitIndex = PercentToPoint.WayPointIndex(enemyMovements[i].percentAllPaths + (enemyMovements[i].f_Speed.Evaluate(0f) * timeSkipValue), enemyMovements[i].pathIndex);
            enemyMovements[i].transform.position = EnemyPathMaking.t_Points[enemyMovements[i].pathIndex][enemyMovements[i].i_waypoitIndex - 1].transform.position;
            enemyMovements[i].Target();
            enemyMovements[i].transform.position = PercentToPoint.PercentToPath(enemyMovements[i].percentAllPaths + (enemyMovements[i].f_Speed.Evaluate(0f) * timeSkipValue), enemyMovements[i].pathIndex, enemyMovements[i].transform.rotation);
        }

        movement.divideSpeed = 1f;
        movement.f_RotateSpeed = orgRot;

        yield return new WaitForSeconds(0.5f);

        Destroy(effect);

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
