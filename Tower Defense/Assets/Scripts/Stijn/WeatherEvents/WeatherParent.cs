using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParent : MonoBehaviour
{
    protected float time;
    protected bool eventGoing;
    protected GameObject spawnedWeatherEffect;
    [SerializeField] protected GameObject weatherEffect;
    [SerializeField] protected float weatherTime;

    public virtual void StartWeather(Transform allEnemies) 
    {
        time = 0;
        eventGoing = true;
        spawnedWeatherEffect = Instantiate(weatherEffect);
    }

    
}
