using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentToPoint : MonoBehaviour
{

    //Volgorde voor percent in path

    //1. PercentToPoint.WaypointIndex 
    //2. Set Enemy.Transform.Position to waypointindex - 1
    //3. Enemy.Target();
    //4. PercentToPoint.PercentToPath

    public static int WayPointIndex(float percent, int path)
    {
        float newValue = EnemyPathMaking.t_Points[path].Length * (percent / 100); //Getal word WaypointIndex + % Distance tussen waypoints. Bijv: 8.32
        int waypointIndex = Mathf.FloorToInt(newValue); //WaypointIndex in vorige voorbeeld is dan 8
        return waypointIndex;
    }

    public static Vector3 PercentToPath(float percent, int path, Quaternion rotation)
    {
        float newValue = EnemyPathMaking.t_Points[path].Length * (percent / 100); //WaypointIndex + % Tussen points
        int waypointIndex = Mathf.FloorToInt(newValue); //Haalt WaypointIndex uit volledig nummer
        
        float distanceInWaypoint = (newValue - waypointIndex) * EnemyPathMaking.distancePoints[path][waypointIndex - 1]; //% terugzetten naar distance
        Vector3 tDirection = rotation * Vector3.forward; //Waar kijkt de enemy naar

        Vector3 spawnPoint = EnemyPathMaking.t_Points[path][waypointIndex - 1].position + tDirection * distanceInWaypoint; //OMG (Ga het aantal distance naar voren + de startpoint van de huidige waypoint)
        return spawnPoint;
    }
}
