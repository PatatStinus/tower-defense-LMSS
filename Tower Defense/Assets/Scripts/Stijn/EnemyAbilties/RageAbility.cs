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

    protected override void Update()
    {
        base.Update();
        if (hp.hp <= hp.startHealth / 2 && !raged) //If enemy is less then half, give hp && triple movement speed
        {
            hp.hp *= 1.5f;
            divideSpeed = 1f/3f;
            StartCoroutine(Revv(transform.position));
            raged = true;
        }
    }

    private IEnumerator Revv(Vector3 freezePos)
    {
        gameObject.layer = 0;
        float time = 0;
        while (time < 2.5f)
        {
            time += Time.deltaTime;
            transform.position = freezePos;
            if(time > 2 && gameObject.layer != 14)
                gameObject.layer = 14;
            yield return null;
        }
    }
}
