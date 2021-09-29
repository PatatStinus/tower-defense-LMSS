using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [HideInInspector] public bool isJumping = true;
    private EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if(isJumping)
        {
            //uuuuuuuuuuuuuuuuuuuuu
        }
    }
}
