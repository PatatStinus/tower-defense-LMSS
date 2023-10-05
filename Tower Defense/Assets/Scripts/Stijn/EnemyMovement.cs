using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public AnimationCurve f_Speed;
    [Range(-1f, 20f)] public float f_RotateSpeed = 0.5f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;
    [Range(0f, 500f)] [SerializeField] private int moneyWhenKilled = 10;
    [SerializeField] private float timeOfMovCurve;

    [HideInInspector] public bool isConfused = false;
    [HideInInspector] public bool usingAbility = false;
    [HideInInspector] public bool isZapped = false;
    [HideInInspector] public bool reachedEnd = false;
    [HideInInspector] public int pathIndex;
    [HideInInspector] public int i_waypoitIndex = 0;
    [HideInInspector] public float divideSpeed = 1;
    [ReadOnly] public float percentAllPaths;
    private float time;
    private Vector3 t_Target;
    private Quaternion q_LookAngle;
    private int activeCurses;

    protected virtual void Start()
    {
        Target(); //Look at target
    }

    protected virtual void Update()
    {
        MoveTarget();

        if (Vector3.Distance(transform.position, t_Target) <= .5f && !usingAbility) //If enemy reached target
            GetNewTarget();

        if (i_waypoitIndex > 0)
            GetPercentWaypoint();
    }

    private void GetNewTarget()
    {
        if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 1 && !isConfused) //Enemy Reached End out of bounds
        {
            reachedEnd = true;
            ManaManager.LoseMana(i_ManaWhenKilled); //Remove mana from unicorn
            Destroy(gameObject);
            return;
        }

        if (!isConfused) //If confused spell is active, give the enemy a random target
            i_waypoitIndex++;
        else
        {
            if(Random.Range(1, 3) == 1)
                i_waypoitIndex = i_waypoitIndex != 0 ? i_waypoitIndex-1 : i_waypoitIndex+1;
            else
                i_waypoitIndex = i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 2 ? i_waypoitIndex-1 : i_waypoitIndex+1;
        }

        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
    }

    private void MoveTarget()
    {
        time += Time.deltaTime;
        if (time > timeOfMovCurve)
            time = 0;
        Vector3 dir = t_Target - transform.position;
        transform.Translate(dir.normalized * (f_Speed.Evaluate(time) / divideSpeed) * Time.deltaTime, Space.World); //Move enemy to target

        q_LookAngle = Quaternion.LookRotation(dir, transform.up); //Get enemy to target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_RotateSpeed * Time.deltaTime); //Rotate enemy to target
    }

    private void GetPercentWaypoint()
    {
        float distanceToNextWaypoint = EnemyPathMaking.distancePoints[pathIndex][i_waypoitIndex - 1] - Vector3.Distance(transform.position, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position);
        float totalDistanceInPath = 0;
        for (int i = 0; i < EnemyPathMaking.distancePoints[pathIndex].Length; i++)
        {
            if(i_waypoitIndex - 1 <= i)
            {
                totalDistanceInPath += distanceToNextWaypoint;
                break;
            }
            totalDistanceInPath += EnemyPathMaking.distancePoints[pathIndex][i];
        }
        percentAllPaths = totalDistanceInPath / EnemyPathMaking.totalDistancePath[pathIndex] * 100f;
    }

    protected void NewTarget(Vector3 newTarget) //Custom Target
    {
        t_Target = newTarget;
    }

    public void Target() //Target point on field (Instant rotations)
    {
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position; 
        transform.LookAt(t_Target);
    }

    protected virtual void OnDisable()
    {
        if(!reachedEnd)
            ManageMoney.GetMoney(moneyWhenKilled);

        CurseEffect.onConfuseCurse -= Confuse;
        CurseEffect.onDisableConfuseCurse -= StopConfuse;
    }

    protected virtual void OnEnable()
    {
        CurseEffect.onConfuseCurse += Confuse;
        CurseEffect.onDisableConfuseCurse += StopConfuse;
    }

    private void Confuse()
    {
        activeCurses++;
        isConfused = true;
    }

    private void StopConfuse()
    {
        activeCurses--;
        if(activeCurses <= 0)
        {
            isConfused = false;
            activeCurses = 0;
        }
    }
}