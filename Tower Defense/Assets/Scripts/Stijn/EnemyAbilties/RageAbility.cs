using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAbility : EnemyMovement
{
    private EnemyHealth hp;
    private bool raged;

    protected override void Start()
    {
        base.Start();
        hp = GetComponent<EnemyHealth>();
    }

    protected override void Update() //Would be cool if it had a little revv like a bull before enraging
    {
        base.Update();
        if (hp.hp <= hp.startHealth / 2 && !raged) //If enemy is less then half, double hp && triple movement speed
        {
            hp.hp *= 2;
            divideSpeed = 1f/3f;
            raged = true;
        }
    }
}
