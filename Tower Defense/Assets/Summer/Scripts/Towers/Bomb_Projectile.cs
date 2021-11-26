using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Projectile : Projectile_Script
{
    [SerializeField] ParticleSystem explosion;

    [SerializeField] float blastRadius;

    [SerializeField] LayerMask enemyLayer;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 || other.gameObject.layer == 14)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            foreach (var col in Physics.OverlapSphere(transform.position, blastRadius, enemyLayer))
            {
                if (col.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                    enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
