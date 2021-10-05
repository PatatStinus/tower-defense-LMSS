using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRainWeather : MonoBehaviour
{
    public delegate void ColorRain();
    public static event ColorRain onBlackRaining;

    public delegate void StopColorRain();
    public static event StopColorRain onStopBlackRaining;

    [SerializeField] private float rainTime;
    [SerializeField] private Transform allEnemies;
    [SerializeField] private GameObject blackRain;
    [SerializeField] private float mxTime;
    [SerializeField] private float mnTime;
    private GameObject blackRainEffect;
    private bool eventGoing;
    private float time;
    private float maxTime;

    private void Start()
    {
        GetNewTime();
        onStopBlackRaining += DestroyObjects;
    }

    private void Update()
    {
        if (!WaveSystem.finishedWave)
            time += Time.deltaTime;

        if (time >= maxTime)
        {
            time = 0;
            eventGoing = true;
            SpawnBlackRain();
            onBlackRaining();
            GetNewTime();
        }

        if (time >= rainTime - 2f && blackRainEffect != null)
            blackRainEffect.GetComponent<ParticleSystem>().Stop();

        if (eventGoing && time >= rainTime)
        {
            onStopBlackRaining();
            eventGoing = false;
        }

        if (eventGoing)
        {
            onBlackRaining?.Invoke();
            for (int i = 0; i < allEnemies.childCount; i++)
                allEnemies.GetChild(i).gameObject.GetComponent<EnemyHealth>().hp += Time.deltaTime;
        }
    }

    private void GetNewTime()
    {
        maxTime = Random.Range(mnTime, mxTime);
    }

    private void SpawnBlackRain()
    {
        blackRainEffect = Instantiate(blackRain);
    }

    private void DestroyObjects()
    {
        Destroy(blackRainEffect);
    }
}
