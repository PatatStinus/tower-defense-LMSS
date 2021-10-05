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
    [SerializeField] private Transform allEnemies;
    [SerializeField] private GameObject colorRain;
    [SerializeField] private float mxTime;
    [SerializeField] private float mnTime;
    private GameObject colorRainEffect;
    private bool eventGoing;
    private float time;
    private float maxTime;

    private void Start()
    {
        GetNewTime();
        onStopColorRaining += DestroyObjects;
    }

    private void Update()
    {
        if(!WaveSystem.finishedWave)
            time += Time.deltaTime;

        if(time >= maxTime)
        {
            time = 0;
            eventGoing = true;
            SpawnColorRain();
            onColorRaining();
            GetNewTime();
        }

        if(time >= rainTime - 2f && colorRainEffect != null)
            colorRainEffect.GetComponent<ParticleSystem>().Stop();

        if(eventGoing && time >= rainTime)
        {
            onStopColorRaining();
            eventGoing = false;
        }

        if(eventGoing)
        {
            onColorRaining?.Invoke();
            for (int i = 0; i < allEnemies.childCount; i++)
                allEnemies.GetChild(i).gameObject.GetComponent<EnemyHealth>().hp -= Time.deltaTime;
        }
    }

    private void GetNewTime()
    {
        maxTime = Random.Range(mnTime, mxTime);
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
