using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAbility : MonoBehaviour
{
    [HideInInspector] public bool ability = true;
    private bool isJumping = false;
    private EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if(!isJumping && ability)
        {
            Invoke("Jump", Random.Range(2f, 4f));
            isJumping = true;
        }
    }

    private void Jump()
    {

    }
}
