using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyMovement : MonoBehaviour
{
    public AnimationCurve f_Speed;
    [Range(-1f, 20f)] public float f_RotateSpeed = 0.5f;
    [Range(0f, 500f)] [SerializeField] private int i_ManaWhenKilled = 10;
    [Range(0f, 500f)] [SerializeField] private int moneyWhenKilled = 10;
    [SerializeField] private float timeOfMovCurve;

    [HideInInspector] public bool isConfused = false;
    [HideInInspector] public bool usingAbility = false;
    [HideInInspector] public bool isZapped = false;
    [HideInInspector] public bool reachedEnd = false;
    [HideInInspector] public int pathIndex;
    [HideInInspector] public int i_waypoitIndex = 0;
    [HideInInspector] public float divideSpeed = 1;
    [ReadOnly] public float percentAllPaths;
    private float progressPath;
    private float percentToPoint;
    private float time;
    private Vector3 t_Target;
    private Quaternion q_LookAngle;

    private void Start()
    {
        Target();
    }

    private void Update()
    {
        MoveTarget();

        if (Vector3.Distance(transform.position, t_Target) <= .5f && !usingAbility) //If enemy reached target
            GetNewTarget();

        if (i_waypoitIndex > 0)
            GetPercentWaypoint();
    }

    private void GetNewTarget()
    {
        if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 1) //Enemy Reached End out of bounds
        {
            reachedEnd = true;
            ManaManager.LoseMana(i_ManaWhenKilled); //Remove mana from unicorn
            Destroy(gameObject);
            return;
        }

        if (!isConfused) //If confused spell is active, give the enemy a random target
            i_waypoitIndex++;
        else
        {
            if(Random.Range(1, 3) == 1)
            {
                if (i_waypoitIndex != 0)
                    i_waypoitIndex--;
                else
                    i_waypoitIndex++;
            }
            else
            {
                if (i_waypoitIndex >= EnemyPathMaking.t_Points[pathIndex].Length - 3)
                    i_waypoitIndex--;
                else
                    i_waypoitIndex++;
            }
        }

        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
    }

    private void MoveTarget()
    {
        time += Time.deltaTime;
        if (time > timeOfMovCurve)
            time = 0;
        Vector3 dir = t_Target - transform.position;
        transform.Translate(dir.normalized * (f_Speed.Evaluate(time) / divideSpeed) * Time.deltaTime, Space.World); //Move enemy to target

        q_LookAngle = Quaternion.LookRotation(dir, transform.up); //Get enemy to target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, q_LookAngle, f_RotateSpeed * Time.deltaTime); //Rotate enemy to target
    }

    private void GetPercentWaypoint()
    {
        percentToPoint = Vector3.Distance(transform.position, EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex - 1].position) / EnemyPathMaking.distancePoints[pathIndex][i_waypoitIndex - 1];
        progressPath = i_waypoitIndex + percentToPoint; //Dus Enemy op waypoint 1 - 2 die op de helft van het pad is zou 1.5 uitkomen
        percentAllPaths = progressPath / EnemyPathMaking.t_Points[pathIndex].Length * 100; 
    }

    public void GetNewWayPoint(bool confused)
    {
        if(confused)
        {
            i_waypoitIndex--;
            t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
        }
        else
        {
            i_waypoitIndex++;
            t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position;
        }
    } 

    public void NewTarget(Vector3 newTarget)
    {
        t_Target = newTarget;
    }

    public void Target()
    {
        t_Target = EnemyPathMaking.t_Points[pathIndex][i_waypoitIndex].position; 
        transform.LookAt(t_Target);
    }

    private void OnDisable()
    {
        if(!reachedEnd)
            ManageMoney.GetMoney(moneyWhenKilled);
    }
}

public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}