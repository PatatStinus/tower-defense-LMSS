using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRainWeather : WeatherParent
{
    public delegate void ColorRain();
    public static event ColorRain onColorRaining;

    public delegate void StopColorRain();
    public static event StopColorRain onStopColorRaining;

    protected override void Update()
    {
        base.Update();
        if (!eventGoing) return;

        if(time >= weatherTime - 2f && spawnedWeatherEffect != null)
            spawnedWeatherEffect.GetComponent<ParticleSystem>().Stop();

        if(time >= weatherTime)
        {
            Destroy(spawnedWeatherEffect);
            eventGoing = false;
            onStopColorRaining?.Invoke();
        }

        //Will call event all the time because enemy health is only done once during each event call?????????????
        //This makes it so every other function that only needs to be called once, has to remove itself from the event.
        //Het werkt wel dus ik hou het erin, maar het kan weg als het echt moet.
        onColorRaining?.Invoke();
    }

    public override void StartWeather(Transform enemies)
    {
        base.StartWeather(enemies);
        onColorRaining?.Invoke();
    }
}
