using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpEnemy : EnemyMovement
{
    //BOSS ENEMY
    [SerializeField] private float timeSkipValue = 1f;
    [SerializeField] private GameObject timeSkipEffect;
    [SerializeField] private Vector2 attackRange;
    private GameObject canvas;
    private GameObject enemyParent;
    private List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
    private bool doneTime;
    private float attackPercent;

    public delegate void TimeSkipEvent(float timeSkip);
    public static event TimeSkipEvent OnTimeSkip;

    public delegate void StopTimeSkipEvent(float timeSkip);
    public static event StopTimeSkipEvent OnStopTimeSkip;

    protected override void Start()
    {
        base.Start();
        enemyParent = GameObject.FindGameObjectWithTag("Enemy");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        attackPercent = Random.Range(attackRange.x, attackRange.y); //Random spot to use ability
    }

    protected override void Update()
    {
        base.Update();
        if(percentAllPaths > attackPercent && !doneTime)
        {
            StartCoroutine(TimeSkip());
            doneTime = true;
        }
    }

    private IEnumerator TimeSkip() //NEEDS A REWORK
    {
        divideSpeed = 1000f;
        float orgRot = f_RotateSpeed;
        f_RotateSpeed = 0f;

        yield return new WaitForSeconds(3f);
        gameObject.layer = 0;

        GameObject effect = Instantiate(timeSkipEffect, canvas.transform);

        OnTimeSkip?.Invoke(timeSkipValue);

        divideSpeed = 1f;
        f_RotateSpeed = orgRot;

        yield return new WaitForSeconds(0.5f);
        gameObject.layer = 14;

        OnStopTimeSkip?.Invoke(timeSkipValue);

        Destroy(effect);
    }
}
