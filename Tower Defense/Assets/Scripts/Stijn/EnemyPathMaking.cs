using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMaking : MonoBehaviour
{
    [SerializeField] private List<Transform> t_Paths = new List<Transform>();
    public static List<Transform[]> t_Points = new List<Transform[]>();

    private void Awake()
    {
        for (int k = 0; k < t_Paths.Count; k++)
        {
            t_Points.Add(new Transform[t_Paths[k].childCount]);
            for (int i = 0; i < t_Points[k].Length; i++)
            {
                t_Points[k][i] = t_Paths[k].GetChild(i);
            }
        }
    }
}
