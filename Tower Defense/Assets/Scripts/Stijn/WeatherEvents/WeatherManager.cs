using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Transform allEnemies;
    [SerializeField] private GameObject allWeathers;
    private ColorRainWeather colorRain;
    private BlackRainWeather blackRain;
    private ThunderWeather thunder;
    private int totalWeathers = 0;

    [SerializeField] private float mxTime;
    [SerializeField] private float mnTime;
    private float time;
    private float maxTime;

    private void Start()
    {
        colorRain = allWeathers.GetComponent<ColorRainWeather>();
        totalWeathers++;
        blackRain = allWeathers.GetComponent<BlackRainWeather>();
        totalWeathers++;
        thunder = allWeathers.GetComponent<ThunderWeather>();
        totalWeathers++;
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
            switch (Random.Range(0, totalWeathers))
            {
                case 0:
                    colorRain.StartWeather(allEnemies);
                    break;
                case 1:
                    blackRain.StartWeather(allEnemies);
                    break;
                case 2:
                    thunder.StartWeather(allEnemies);
                    break;
            }
            time = 0;
            GetNewTime();
        }
    }
}
