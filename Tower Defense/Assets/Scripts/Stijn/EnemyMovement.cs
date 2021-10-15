using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0f, 50f)] public float f_Speed = 10f;
    [Range(0f, 20f)] [SerializeField] private float f_RotateSpeed = 0.5f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;

    [HideInInspector] public double percentAllPaths;
    [HideInInspector] public bool isConfused = false;
    [HideInInspector] public bool usingAbility = false;
    [HideInInspector] public bool isZapped = false;
    [HideInInspector] public bool doubledHealth;
    [HideInInspector] public bool reachedEnd = false;
    [HideInInspector] public int pathIndex;
    [HideInInspector] public int i_waypoitIndex = 0;
    [HideInInspector] public float percentToPoint;
    [HideInInspector] public float progressPath;
    [HideInInspector] public float divideSpeed = 1;
    private Vector3 t_Target;
    private Quaternion q_LookAngle;

    private void Start()
    {
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position; 
        transform.LookAt(t_Target);
    }

    private void Update()
    {
        MoveTarget();

        if (Vector3.Distance(transform.position, t_Target) <= .5f && !usingAbility) //If enemy reached target
            GetNewTarget();

        if (i_waypoitIndex > 0)
            GetPercentWaypoint();
    }

    private void GetNewTarget()
    {
        if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 1) //Enemy Reached End out of bounds
        {
            Destroy(gameObject);
            return;
        }

        if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 2) //Enemy Reached End in bounds
        {
            reachedEnd = true;
            ManaManager.LoseMana(i_ManaWhenKilled); //Remove mana from unicorn
            gameObject.layer = 0;
        }

        if (!isConfused) //If confused spell is active, give the enemy a random target
            i_waypoitIndex++;
        else
        {
            if(Random.Range(1, 3) == 1)
            {
                if (i_waypoitIndex != 0)
                    i_waypoitIndex--;
                else
                    i_waypoitIndex++;
            }
            else
            {
                if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 3)
                    i_waypoitIndex--;
                else
                    i_waypoitIndex++;
            }
        }

        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
    }

    private void MoveTarget()
    {
        Vector3 dir = t_Target - transform.position;
        transform.Translate(dir.normalized * (f_Speed / divideSpeed) * Time.deltaTime, Space.World); //Move enemy to target

        q_LookAngle = Quaternion.LookRotation(dir, transform.up); //Get enemy to target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_RotateSpeed * Time.deltaTime); //Rotate enemy to target
    }

    private void GetPercentWaypoint()
    {
        percentToPoint = Vector3.Distance(transform.position, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 1].position) / EnemyPathMaking.distancePoints[pathIndex][i_waypoitIndex - 1];
        progressPath = i_waypoitIndex + percentToPoint;
        percentAllPaths = progressPath / EnemyPathMaking.t_Points[pathIndex].Length * 100;
    }

    public void GetNewWayPoint()
    {
        i_waypoitIndex++;
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
    } 

    public void NewTarget(Vector3 newTarget)
    {
        t_Target = newTarget;
    }
}
