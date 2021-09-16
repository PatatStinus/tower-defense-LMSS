using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float f_Health = 100f;
    [Range(0f, 50f)][SerializeField] private float f_Speed = 10f;

    private Transform t_Target;
    private int i_waypoitIndex = 0;

    void Start()
    {
        t_Target = EnemyPathMaking.t_Points[0];
    }

    void Update()
    {
        Vector3 dir = t_Target.position - transform.position;
        transform.Translate(dir.normalized * f_Speed * Time.deltaTime, Space.World);
    }
}
