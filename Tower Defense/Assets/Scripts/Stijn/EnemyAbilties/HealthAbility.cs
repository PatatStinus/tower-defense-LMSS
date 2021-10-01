using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float regen;
    private Collider[] collisionsInRange;

    private void Start()
    {
        GetComponent<EnemyHealth>().hp -= 20;
    }

    private void Update()
    {
        collisionsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in collisionsInRange)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.hp += regen * Time.deltaTime;
                if (enemy.hp >= enemy.startHealth)
                    enemy.hp = enemy.startHealth;
                Debug.Log(enemy.hp);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
