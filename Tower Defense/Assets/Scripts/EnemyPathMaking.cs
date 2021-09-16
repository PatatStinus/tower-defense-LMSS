using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMaking : MonoBehaviour
{
    [SerializeField] private Transform t_Paths;
    public static Transform[] t_Points;

    private void Awake()
    {
        t_Points = new Transform[t_Paths.childCount];
        for (int i = 0; i < t_Points.Length; i++)
        {
            t_Points[i] = t_Paths.GetChild(i);
        }
    }
}
