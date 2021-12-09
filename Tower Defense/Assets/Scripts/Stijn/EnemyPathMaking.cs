using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMaking : MonoBehaviour
{
    [SerializeField] private List<Transform> t_Paths = new List<Transform>();
    public static List<Transform[]> t_Points = new List<Transform[]>();
    public static List<float[]> distancePoints = new List<float[]>();

    private void Awake()
    {
        t_Points.Clear();
        distancePoints.Clear();
        for (int k = 0; k < t_Paths.Count; k++)
        {
            t_Points.Add(new Transform[t_Paths[k].childCount]);
            distancePoints.Add(new float[t_Paths[k].childCount - 1]);
            for (int i = 0; i < t_Points[k].Length; i++)
                t_Points[k][i] = t_Paths[k].GetChild(i);
            for (int i = 0; i < distancePoints[k].Length; i++)
                distancePoints[k][i] = Vector3.Distance(t_Points[k][i].position, t_Points[k][i + 1].position);
        }
    }
}
