using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float regen;
    [HideInInspector] public bool isRaining = false;
    private Collider[] collisionsInRange;

    private void FixedUpdate()
    {
        collisionsInRange = Physics.OverlapSphere(transform.position, radius);
        foreach (var obj in collisionsInRange)
        {
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) != 0) //Why distance check??
            {
                enemy.hp = isRaining ? enemy.hp -= regen * Time.fixedDeltaTime : enemy.hp += regen * Time.fixedDeltaTime; //isRaining can be optimised through subscribing
                if (enemy.hp >= enemy.startHealth) //Should be done in enemyhealth
                    enemy.hp = enemy.startHealth;
            }
        }
    }

    private void OnDisable()
    {
        if (isRaining)
            ManaManager.GetMana(100);
    }
}