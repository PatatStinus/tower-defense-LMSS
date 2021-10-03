using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAbility : MonoBehaviour
{
    private EnemyHealth hp;
    private EnemyMovement movement;
    private bool raged;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        hp = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (hp.hp <= hp.startHealth / 2 && !raged)
        {
            hp.hp *= 2;
            movement.divideSpeed = 0.5f;
            raged = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hp.hp -= 10;
        }
    }
}
