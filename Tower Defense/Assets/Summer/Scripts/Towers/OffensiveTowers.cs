using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveTowers : MonoBehaviour
{
    [SerializeField] protected GameObject tower;

    [SerializeField] protected float fireRate = 2f;

    protected List<Transform> GO_enemies = new List<Transform>();
    protected GameObject allEnemies;
    protected Transform[] enemies;

    protected Transform closestEnemy;
    [SerializeField] protected Transform turret;
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected float rotationStrength = 2f;

    [SerializeField] protected float detectionRange = 5f;

    [SerializeField] protected LayerMask obstacleLayer;

    protected float timer;

    protected float damage;

    public virtual void Start()
    {
        timer = 0;
        allEnemies = GameObject.FindGameObjectWithTag("Enemy");
    }

    public virtual void Update()
    {
        GO_enemies.Clear();

        for (int i = 0; i < allEnemies.transform.childCount; i++)
            GO_enemies.Add(allEnemies.transform.GetChild(i));

        if (tower.tag == "Placed")
        {
            AssignTarget();
        }
    }

    public virtual void AssignTarget()
    {
        timer -= Time.deltaTime;
        enemies = new Transform[GO_enemies.Count];

        if (enemies.Length == 0)
            return;

        for (int i = 0; i < GO_enemies.Count; i++)
            enemies[i] = GO_enemies[i].transform;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].position) <= detectionRange
                && !Physics.Linecast(turret.position, enemies[i].position, obstacleLayer))
            {
                closestEnemy = enemies[i];
            }
        }

        if (closestEnemy == null)
            return;

        if (Physics.Linecast(turret.position, closestEnemy.position, obstacleLayer))
        {
            closestEnemy = null;
            return;
        }

        if (Vector3.Distance(transform.position, closestEnemy.position) <= detectionRange)
            RotateToTarget(closestEnemy);
    }

    public virtual void RotateToTarget(Transform target)
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

    public virtual void ShootAtTarget()
    {
        if (timer <= 0)
        {
            bullet.GetComponent<Projectile_Script>().damage = damage;
            bullet.GetComponent<Projectile_Script>().turret = turret;
            bullet.GetComponent<Projectile_Script>().Target = closestEnemy;
            Instantiate(bullet, turret.position, Quaternion.identity);
            timer = fireRate;
        }
    }
}
