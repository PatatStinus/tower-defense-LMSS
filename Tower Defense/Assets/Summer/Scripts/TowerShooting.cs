using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    [SerializeField] GameObject tower;

    [SerializeField] float fireRate = 2f;

    GameObject[] GO_enemies;
    [SerializeField] Transform[] enemies;

    [SerializeField] Transform closestEnemy;
    [SerializeField] Transform turret;
    [SerializeField] GameObject bullet;

    [SerializeField] float rotationStrength = 2f;

    [SerializeField] float detectionRange = 5f;

    private Quaternion startRot;

    float timer;

    private void Start()
    {
        timer = 0;
        startRot = transform.rotation;
    }

    void Update()
    {
        GO_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (tower.tag == "Placed")
        {
            AssignTarget();
        }
    }

    private void AssignTarget()
    {
        timer -= Time.deltaTime;
        enemies = new Transform[GO_enemies.Length];

        if (enemies.Length == 0)
            return;

        for (int i = 0; i < GO_enemies.Length; i++)
        {
            enemies[i] = GO_enemies[i].transform;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (closestEnemy == null)
            {
                closestEnemy = enemies[i];
                return;
            }

            if (Vector3.Distance(transform.position, enemies[i].position) < Vector3.Distance(transform.position, closestEnemy.position))
            {
                closestEnemy = enemies[i];
            }
        }

        if (Vector3.Distance(transform.position, closestEnemy.position) <= detectionRange)
        {
            RotateToTarget(closestEnemy);
        }
        else
        {
            transform.rotation = startRot;
        }

    }

    private void RotateToTarget(Transform target)
    {
        Vector3 dirAtoB = (target.position - transform.position).normalized;
        float dot = Vector3.Dot(dirAtoB, transform.forward);

        if (dot < 0.99)
        {
            var str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
            var targetRotation = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z) - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
        else if (dot > 0.99)
        {
            ShootAtTarget();
        }
    }

    private void ShootAtTarget()
    {      
        if (timer <= 0)
        {
            bullet.GetComponent<Projectile_Script>().turret = turret;
            bullet.GetComponent<Projectile_Script>().Target = closestEnemy;
            Instantiate(bullet, turret.position, Quaternion.identity);
            timer = fireRate;
        }
    }
}
