using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform t_EnemyParent;
    [SerializeField] private Transform t_SpawnPoint;
    [SerializeField] private float f_FloorY;
    private Renderer rend;

    public void SpawnEnemy(GameObject enemy)
    {
        rend = enemy.GetComponent<Renderer>();
        enemy.transform.position = new Vector3(t_SpawnPoint.position.x, rend.bounds.extents.y + f_FloorY, t_SpawnPoint.position.z);
        Instantiate(enemy, t_EnemyParent);
    }
}
