using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Script : MonoBehaviour
{
    [SerializeField]int damage;

    [SerializeField]ParticleSystem explosion;

    [SerializeField] float blastRadius;

    [SerializeField] LayerMask enemyLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 14)
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
