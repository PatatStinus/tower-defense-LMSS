using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeDestroy;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeDestroy)
            Destroy(this.gameObject);
    }
}
