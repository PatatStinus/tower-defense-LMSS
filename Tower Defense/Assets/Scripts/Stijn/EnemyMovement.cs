using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0f, 50f)] public float f_Speed = 10f;
    [Range(0f, 20f)] public float f_RotateSpeed = 0.5f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;

    [HideInInspector] public bool isConfused;
    [HideInInspector] public float divideSpeed = 1;
    [HideInInspector] public bool isZapped = false;
    [HideInInspector] public int pathIndex;
    [HideInInspector] public int i_waypoitIndex = 0;
    private Transform t_Target;
    private Quaternion q_LookAngle;

    private void Start()
    {
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex]; 
        transform.LookAt(t_Target);
    }

    private void Update()
    {
        MoveTarget();

        if (Vector3.Distance(transform.position, t_Target.position) <= .5f) //If enemy reached target
            GetNewTarget();
    }

    private void GetNewTarget()
    {
        if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 1) //Get new target
        {
            ManaManager.LoseMana(i_ManaWhenKilled); //Remove mana from unicorn
            Destroy(gameObject); //Enemy reached the end
            return;
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
                if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 2)
                    i_waypoitIndex--;
                else
                    i_waypoitIndex++;
            }
        }

        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex];
    }

    private void MoveTarget()
    {
        Vector3 dir = t_Target.position - transform.position;
        transform.Translate(dir.normalized * (f_Speed / divideSpeed) * Time.deltaTime, Space.World); //Move enemy to target

        Vector3 lookDir = t_Target.position - transform.position;
        q_LookAngle = Quaternion.LookRotation(lookDir, transform.up); //Get enemy to target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_RotateSpeed * Time.deltaTime); //Rotate enemy to target
    }
    public void GetNewWayPoint()
    {
        i_waypoitIndex++;
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex];
    } 
}
