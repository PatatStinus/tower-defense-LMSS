using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthParticles : MonoBehaviour
{
    private Quaternion orgRot;

    private void Start()
    {
        orgRot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = orgRot;
    }
}
