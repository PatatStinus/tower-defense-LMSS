using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeDestroy;
    [SerializeField] private bool passTimeAfterWave = false;

    private IEnumerator Start()
    {
        float time = 0;

        while (time < timeDestroy)
        {
            if(!passTimeAfterWave)
                yield return new WaitUntil(() => !WaveSystem.finishedWave);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
