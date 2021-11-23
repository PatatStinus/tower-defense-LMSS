using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentToPoint : MonoBehaviour
{
    public static int WayPointIndex(float percent, int path)
    {
        float newValue = EnemyPathMaking.t_Points[path].Length * (percent / 100);
        int waypointIndex = Mathf.FloorToInt(newValue);
        return waypointIndex;
    }

    public static Vector3 PercentToPath(float percent, int path, Vector3 direction, Quaternion rot)
    {
        float newValue = EnemyPathMaking.t_Points[path].Length * (percent / 100);
        int waypointIndex = Mathf.FloorToInt(newValue);
        Vector3 tDirection = Vector3.Scale(direction, rot.eulerAngles);
        
        float distanceInWaypoint = (newValue - waypointIndex) * EnemyPathMaking.distancePoints[path][waypointIndex - 1];
        
        Vector3 spawnPoint = EnemyPathMaking.t_Points[path][waypointIndex].position + tDirection * distanceInWaypoint;
        return spawnPoint;
    }

    public void AAAAAA()
    {
        Vector3 AAAA = transform.rotation * transform.forward;
    }
}
