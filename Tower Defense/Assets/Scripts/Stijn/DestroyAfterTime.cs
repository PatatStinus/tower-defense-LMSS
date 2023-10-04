using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeDestroy;

    private IEnumerator Start()
    {
        float time = 0;

        while (time < timeDestroy)
        {
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
