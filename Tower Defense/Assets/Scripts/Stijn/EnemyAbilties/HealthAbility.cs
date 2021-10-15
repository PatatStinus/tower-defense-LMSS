using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float regen;
    [HideInInspector] public bool isRaining = false;
    private Collider[] collisionsInRange;

    private void Update()
    {
        collisionsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in collisionsInRange)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) != 0)
            {
                enemy.hp = isRaining ? enemy.hp -= regen * Time.deltaTime : enemy.hp += regen * Time.deltaTime;
                if (enemy.hp >= enemy.startHealth)
                    enemy.hp = enemy.startHealth;
            }
        }
    }

    private void OnDisable()
    {
        if (isRaining && !GetComponent<EnemyMovement>().reachedEnd)
            ManaManager.GetMana(100);
    }
}