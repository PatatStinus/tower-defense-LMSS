using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform t_EnemyParent;
    [SerializeField] private Transform t_SpawnPoint;

    public void SpawnEnemy(GameObject enemy, int indexEnemy, int indexPath, float difficulty)
    {
        GameObject spawnedEnemy = Instantiate(enemy, t_EnemyParent);
        spawnedEnemy.transform.position = new Vector3(t_SpawnPoint.position.x, enemy.transform.position.y, t_SpawnPoint.position.z);
        spawnedEnemy.name = $"Enemy ({indexEnemy})";
        spawnedEnemy.GetComponent<EnemyMovement>().pathIndex = indexPath;
        if(spawnedEnemy.TryGetComponent(out EnemyHealth health))
            health.startHealth *= difficulty;
    }
}
