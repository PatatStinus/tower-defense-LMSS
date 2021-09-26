using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0f, 50f)] public float f_Speed = 10f;
    [Range(0f, 2f)] public float f_RotateSpeed = 10f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;

    [HideInInspector] public bool isConfused;
    [HideInInspector] public float divideSpeed = 1;
    private Transform t_Target;
    private Quaternion q_LookAngle;
    private int i_waypoitIndex = 0;
    private float f_TimeForRot = 1;

    void OnEnable()
    {
        t_Target = EnemyPathMaking.t_Points[i_waypoitIndex];
        transform.LookAt(t_Target);
    }

    void Update()
    {
        Vector3 dir = t_Target.position - transform.position;
        transform.Translate(dir.normalized * (f_Speed / divideSpeed) * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, t_Target.position) <= .5f)
        {
            if (i_waypoitIndex >= EnemyPathMaking.t_Points.Length - 1)
            {
                ManaManager.LoseMana(i_ManaWhenKilled); //Remove mana from unicorn
                Destroy(gameObject); //Enemy reached the end
                return;
            }

            if (!isConfused)
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
                    if (i_waypoitIndex >= EnemyPathMaking.t_Points.Length - 2)
                        i_waypoitIndex--;
                    else
                        i_waypoitIndex++;
                }
            }

            t_Target = EnemyPathMaking.t_Points[i_waypoitIndex];

            f_TimeForRot = 0;
            Vector3 lookDir = t_Target.position - transform.position;
            q_LookAngle = Quaternion.LookRotation(lookDir, transform.up);
        }

        if (f_TimeForRot < 1)
        {
            f_TimeForRot += Time.deltaTime * f_RotateSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_TimeForRot);
        }
    }
}
