using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Range(0f, 50f)] [SerializeField] private float f_Speed = 10f;
    [Range(0f, 2f)] [SerializeField] private float f_RotateSpeed = 10f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;

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
        transform.Translate(dir.normalized * f_Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, t_Target.position) <= .5f)
        {
            if (i_waypoitIndex >= EnemyPathMaking.t_Points.Length - 1)
            {
                ManaManager.GetMana(i_ManaWhenKilled);
                Destroy(gameObject); //Enemy reached the end
                return;
            }

            i_waypoitIndex++;
            t_Target = EnemyPathMaking.t_Points[i_waypoitIndex];

            f_TimeForRot = 0;
            Vector3 lookDir = t_Target.position - transform.position;
            q_LookAngle = Quaternion.LookRotation(lookDir, transform.up);
        }

        if (f_TimeForRot < 1)
        {
            f_TimeForRot += Time.deltaTime * f_RotateSpeed; //Dit werkt :))))))))))))
            transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_TimeForRot);
        }
    }
}
