using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEnemy : MonoBehaviour
{
    //BOSS ENEMY
    [SerializeField] private float timeSkipValue = 1f;
    [SerializeField] private GameObject timeSkipEffect;
    [SerializeField] private Vector2 attackRange;
    private GameObject canvas;
    private GameObject enemyParent;
    private List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
    private EnemyMovement movement;
    private bool doneTime;
    private float attackPercent;

    private void Start()
    {
        enemyParent = GameObject.FindGameObjectWithTag("Enemy");
        movement = GetComponent<EnemyMovement>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        attackPercent = Random.Range(attackRange.x, attackRange.y + 1); //Random spot to use ability
    }

    private void Update()
    {
        if(movement.percentAllPaths > attackPercent && !doneTime)
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
            float percentPath = enemyMovements[i].isConfused ? Random.Range(0, 2) == 0 ? enemyMovements[i].percentAllPaths + (enemyMovements[i].f_Speed.Evaluate(0f) * timeSkipValue) : enemyMovements[i].percentAllPaths - (enemyMovements[i].f_Speed.Evaluate(0f) * timeSkipValue) : enemyMovements[i].percentAllPaths + (enemyMovements[i].f_Speed.Evaluate(0f) * timeSkipValue);
            //Caculation to move enemy certain amount of % after timeskip ^
            if (percentPath > 99)
                percentPath = 99;

            enemyMovements[i].i_waypoitIndex = PercentToPoint.WayPointIndex(percentPath, enemyMovements[i].pathIndex); //Percent To Point script
            enemyMovements[i].transform.position = EnemyPathMaking.t_Points[enemyMovements[i].pathIndex][enemyMovements[i].i_waypoitIndex - 1].transform.position;
            enemyMovements[i].Target();
            enemyMovements[i].transform.position = PercentToPoint.PercentToPath(percentPath, enemyMovements[i].pathIndex, enemyMovements[i].transform.rotation);
            
            if (enemyMovements[i].TryGetComponent(out JumpingAbility jump))
                jump.canAbility = false;
        }

        movement.divideSpeed = 1f;
        movement.f_RotateSpeed = orgRot;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < enemyMovements.Count; i++)
        {
            if (enemyMovements[i] == null)
                continue;
            if (enemyMovements[i].TryGetComponent(out JumpingAbility jump))
                jump.canAbility = true;
        }

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
