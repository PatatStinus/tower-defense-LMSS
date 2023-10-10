using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRainWeather : WeatherParent
{
    public delegate void BlackRain();
    public static event BlackRain onBlackRaining;

    public delegate void StopBlackRain();
    public static event StopBlackRain onStopBlackRaining;

    protected override void Update()
    {
        base.Update();
        if (!eventGoing) return;

        if (time >= weatherTime - 2f && spawnedWeatherEffect != null)
            spawnedWeatherEffect.GetComponent<ParticleSystem>().Stop();

        if (time >= weatherTime)
        {
            Destroy(spawnedWeatherEffect);
            eventGoing = false;
            onStopBlackRaining?.Invoke();
        }

        //Will call event all the time because enemy health is only done once during each event call?????????????
        //This makes it so every other function that only needs to be called once, has to remove itself from the event.
        //Het werkt wel dus ik hou het erin, maar het kan weg als het echt moet.
        onBlackRaining?.Invoke();
    }

    public override void StartWeather(Transform enemies)
    {
        base.StartWeather(enemies);
        onBlackRaining?.Invoke();
    }
}
