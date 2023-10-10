using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeatherParent : MonoBehaviour
{
    protected float time;
    protected bool eventGoing;
    protected GameObject spawnedWeatherEffect;
    [SerializeField] protected GameObject weatherEffect;
    [SerializeField] protected float weatherTime;

    protected virtual void Update()
    {
        if (!eventGoing) return;

        if (!WaveSystem.finishedWave)
            time += Time.deltaTime;
    }


    public virtual void StartWeather(Transform allEnemies) 
    {
        time = 0;
        eventGoing = true;
        spawnedWeatherEffect = Instantiate(weatherEffect);
    }

    public virtual void StopWeather()
    {
        if (!WaveSystem.finishedWave)
            time = weatherTime - 2f;
        else
            time = weatherTime;
    }

    
}

[CanEditMultipleObjects, CustomEditor(typeof(WeatherParent), true)]
public class WeatherParentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeatherParent weather = (WeatherParent)target;
        if (GUILayout.Button("Start Weather Event"))
            weather.StartWeather(GameObject.FindGameObjectWithTag("Enemy").transform);
        if (GUILayout.Button("Stop Weather Event"))
            weather.StopWeather();
    }
}
