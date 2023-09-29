using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentToPoint : MonoBehaviour
{
    //Volgorde voor percent in path

    //1. PercentToPoint.GetWayPointIndexFromPercent | Zodat de enemy een nieuwe waypoint heeft
    //2. Set Enemy.Transform.Position to waypointindex - 1 | - 1 voor de vorige waypoint
    //3. Enemy.Target(); | Zodat de enemy naar de volgende target kijkt, dan kan die vooruit met de benodigde distance vanaf de vorige waypoint
    //4. PercentToPoint.PercentToPath | Bereken de distance naar volgende waypoint

    public static int GetWayPointIndexFromPercent(float percent, int path)
    {
        float newValue = EnemyPathMaking.totalDistancePath[path] * (percent / 100);
        float minDis = 0;
        for (int i = 0; i < EnemyPathMaking.distancePoints[path].Length; i++)
        {
            minDis += EnemyPathMaking.distancePoints[path][i];
            if (minDis >= newValue)
                return i + 1;
        }
        return 1;
    }

    public static Vector3 PercentToPath(float percent, int path, Quaternion rotation)
    {
        int waypointIndex = 0;
        float newValue = EnemyPathMaking.totalDistancePath[path] * (percent / 100);
        float minDis = 0;
        float distanceInWaypoint = 0;
        for (int i = 0; i < EnemyPathMaking.distancePoints[path].Length; i++)
        {
            minDis += EnemyPathMaking.distancePoints[path][i];
            if (minDis >= newValue)
            {
                waypointIndex = i + 1;
                minDis -= EnemyPathMaking.distancePoints[path][i];
                distanceInWaypoint = newValue - minDis;
                break;
            }
        }

        Vector3 tDirection = rotation * Vector3.forward; //Waar kijkt de enemy naar

        Vector3 spawnPoint = EnemyPathMaking.t_Points[path][waypointIndex - 1].position + tDirection * distanceInWaypoint; //OMG (Ga het aantal distance naar voren + de startpoint van de huidige waypoint)
        return spawnPoint;
    }
}
