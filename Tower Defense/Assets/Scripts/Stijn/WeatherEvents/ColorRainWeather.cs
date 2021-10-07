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
    private Transform allEnemies;
    private GameObject colorRainEffect;
    private bool eventGoing;
    private float time;

    private void Start()
    {
        onStopColorRaining += DestroyObjects;
    }

    private void Update()
    {
        if(!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

        if(time >= rainTime - 2f && colorRainEffect != null)
            colorRainEffect.GetComponent<ParticleSystem>().Stop();

        if(eventGoing && time >= rainTime)
        {
            onStopColorRaining();
            eventGoing = false;
        }

        if(eventGoing)
            onColorRaining?.Invoke();
    }

    public void StartWeather(Transform enemies)
    {
        allEnemies = enemies;
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
