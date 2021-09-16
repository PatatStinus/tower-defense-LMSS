using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float f_Health = 100f;
    [Range(0f, 50f)][SerializeField] private float f_Speed = 10f;

    private Transform t_Target;
    //private Quaternion q_LookAngle;
    private int i_waypoitIndex = 0;
    //private float f_TimeForRot = 1;

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
                Destroy(gameObject);
                return;
            }

            i_waypoitIndex++;
            t_Target = EnemyPathMaking.t_Points[i_waypoitIndex];

            transform.LookAt(t_Target);

            /*f_TimeForRot = 0;
            q_LookAngle = Quaternion.LookRotation(t_Target.position, transform.position);*/
        }

        /*if (f_TimeForRot < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_TimeForRot);
            f_TimeForRot += Time.deltaTime * 3; //Dit werkt niet :(
        } */
    }
}
