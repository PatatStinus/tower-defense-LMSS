using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRainWeather : MonoBehaviour
{
    public delegate void BlackRain();
    public static event BlackRain onBlackRaining;

    public delegate void StopBlackRain();
    public static event StopBlackRain onStopBlackRaining;

    [SerializeField] private float rainTime;
    [SerializeField] private GameObject blackRain;
    private Transform allEnemies;
    private GameObject blackRainEffect;
    private bool eventGoing;
    private float time;

    private void Start()
    {
        onStopBlackRaining += DestroyObjects;
    }

    private void Update()
    {
        if (!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

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

    public void StartWeather(Transform enemies)
    {
        allEnemies = enemies;
        time = 0;
        eventGoing = true;
        SpawnBlackRain();
        onBlackRaining?.Invoke();
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
