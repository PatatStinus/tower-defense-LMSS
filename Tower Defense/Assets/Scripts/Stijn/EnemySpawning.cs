using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] private Transform t_EnemyParent;
    [SerializeField] private Transform t_SpawnPoint;
    [SerializeField] private GameObject g_Enemy;
    [SerializeField] private float f_FloorY;
    private Renderer rend;

    private void Start()
    {
        rend = g_Enemy.GetComponent<Renderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            g_Enemy.transform.position = new Vector3(t_SpawnPoint.position.x, rend.bounds.extents.y + f_FloorY, t_SpawnPoint.position.z);
            Instantiate(g_Enemy, t_EnemyParent);
        }
    }
}
