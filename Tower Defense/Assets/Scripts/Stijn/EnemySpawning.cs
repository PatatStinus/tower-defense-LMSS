using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform t_EnemyParent;
    [SerializeField] private Transform t_SpawnPoint;

    public void SpawnEnemy(GameObject enemy, int indexEnemy, int indexPath)
    {
        enemy.transform.position = new Vector3(t_SpawnPoint.position.x, enemy.transform.position.y, t_SpawnPoint.position.z);
        GameObject spawnedEnemy = Instantiate(enemy, t_EnemyParent);
        spawnedEnemy.name = $"Enemy ({indexEnemy})";
        spawnedEnemy.GetComponent<EnemyMovement>().pathIndex = indexPath;
    }
}
