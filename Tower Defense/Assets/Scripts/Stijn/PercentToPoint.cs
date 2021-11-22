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

    public static Vector3 PercentToPath(float percent, int path)
    {
        float newValue = EnemyPathMaking.t_Points[path].Length * (percent / 100);
        int waypointIndex = Mathf.FloorToInt(newValue);
        float fDirection = Mathf.Atan2(EnemyPathMaking.t_Points[path][waypointIndex].position.x - EnemyPathMaking.t_Points[path][waypointIndex - 1].position.x, EnemyPathMaking.t_Points[path][waypointIndex].position.y - EnemyPathMaking.t_Points[path][waypointIndex - 1].position.y);
        Vector3 tDirection = new Vector3(0, fDirection, 0);
        float distanceInWaypoint = (newValue - waypointIndex) * EnemyPathMaking.distancePoints[path][waypointIndex - 1];
        
        Vector3 spawnPoint = EnemyPathMaking.t_Points[path][waypointIndex].position - tDirection * distanceInWaypoint;
        return spawnPoint;
    }
}
