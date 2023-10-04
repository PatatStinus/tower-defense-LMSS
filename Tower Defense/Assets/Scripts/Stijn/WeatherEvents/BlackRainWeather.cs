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
    private GameObject blackRainEffect;
    private bool eventGoing;
    private float time;

    private void OnEnable()
    {
        onStopBlackRaining += DestroyObjects;
    }

    private void OnDisable()
    {
        onStopBlackRaining -= DestroyObjects;
    }

    private void Update()
    {
        if (!WaveSystem.finishedWave && eventGoing)
            time += Time.deltaTime;

        if (time >= rainTime - 2f && blackRainEffect != null)
            blackRainEffect.GetComponent<ParticleSystem>().Stop();

        if (eventGoing && time >= rainTime)
        {
            onStopBlackRaining?.Invoke();
            eventGoing = false;
        }

        //Will call event all the time because enemy health is only done once during each event call?????????????
        //This makes it so every other function that only needs to be called once, has to remove itself from the event.
        //Het werkt wel dus ik hou het erin, maar het kan weg als het echt moet.
        if (eventGoing)
            onBlackRaining?.Invoke();
    }

    public void StartWeather(Transform enemies)
    {
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
