using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Transform allEnemies;
    [SerializeField] private Transform allWeathers;

    [SerializeField] private float mxTime;
    [SerializeField] private float mnTime;
    private float time;
    private float maxTime;

    private void Start()
    {
        GetNewTime();
    }

    private void GetNewTime()
    {
        maxTime = Random.Range(mnTime, mxTime);
    }

    private void Update()
    {
        if (!WaveSystem.finishedWave)
            time += Time.deltaTime;

        if(time >= maxTime)
        {
            allWeathers.GetComponents<WeatherParent>()[Random.Range(0, 3)].StartWeather(allEnemies);
            time = 0;
            GetNewTime();
        }
    }
}
