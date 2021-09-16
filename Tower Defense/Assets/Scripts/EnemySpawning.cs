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
            g_Enemy.transform.position = t_SpawnPoint.position;
            Instantiate(g_Enemy, t_EnemyParent);
        }
    }
}
