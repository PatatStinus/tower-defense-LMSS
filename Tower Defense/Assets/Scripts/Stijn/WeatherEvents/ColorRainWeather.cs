using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRainWeather : MonoBehaviour
{
    public delegate void ColorRain();
    public static event ColorRain onColorRaining;

    public delegate void StopColorRain();
    public static event StopColorRain onStopColorRaining;

    [SerializeField] private float rainTime;
    [SerializeField] private GameObject colorRain;
    private GameObject colorRainEffect;
    private bool eventGoing;
    private float time;

    private void OnEnable()
    {
        onStopColorRaining += DestroyObjects;
    }

    private void OnDisable()
    {
        onStopColorRaining -= DestroyObjects;
    }

    private void Update()
    {
        if(!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

        if(time >= rainTime - 2f && colorRainEffect != null)
            colorRainEffect.GetComponent<ParticleSystem>().Stop();

        if(eventGoing && time >= rainTime)
        {
            onStopColorRaining?.Invoke();
            eventGoing = false;
        }

        //Will call event all the time because enemy health is only done once during each event call?????????????
        //This makes it so every other function that only needs to be called once, has to remove itself from the event.
        //Het werkt wel dus ik hou het erin, maar het kan weg als het echt moet.
        if (eventGoing) 
            onColorRaining?.Invoke();
    }

    public void StartWeather(Transform enemies)
    {
        time = 0;
        eventGoing = true;
        SpawnColorRain();
        onColorRaining?.Invoke();
    }

    private void SpawnColorRain()
    {
        colorRainEffect = Instantiate(colorRain);
    }

    private void DestroyObjects()
    {
        Destroy(colorRainEffect);
    }
}
