using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour
{
    public Transform Target;
    public Projectile_Script(Transform t)
    {
        Target = t;
    }

    private void Update()
    {
        Vector3 direction = Target.position - transform.position;
        transform.Translate(direction.normalized * 0.02f, Space.World);
    }
}
