using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform t_EnemyParent;
    [SerializeField] private Transform t_SpawnPoint;
    [SerializeField] private GameObject g_Enemy;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            g_Enemy.transform.position = new Vector3(t_SpawnPoint.position.x, g_Enemy.transform.localScale.y / 2f, t_SpawnPoint.position.z);
            Instantiate(g_Enemy, t_EnemyParent);
        }
    }
}
