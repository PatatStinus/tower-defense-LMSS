using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float regen;
    private Collider[] collisionsInRange;

    private void Update()
    {
        collisionsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in collisionsInRange)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            //if (enemy != null)
                //enemy.hp += regen * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
