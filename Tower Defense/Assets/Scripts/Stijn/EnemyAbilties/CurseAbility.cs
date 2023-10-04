using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAbility : EnemyMovement
{
    [SerializeField] private GameObject cursedObject;
    private GameObject theCurse;
    private bool isRaining;
    private int curse;


    protected override void Start()
    {
        base.Start();
        curse = Random.Range(1, 4);
        ColorRainWeather.onColorRaining += RainEvent;
        ColorRainWeather.onStopColorRaining += StopRainEvent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ColorRainWeather.onColorRaining -= RainEvent;
        ColorRainWeather.onStopColorRaining -= StopRainEvent;
        SpawnCurse();
    }

    private void SpawnCurse()
    {
        theCurse = Instantiate(cursedObject);
        theCurse.GetComponent<CurseEffect>().DoCurse(curse, isRaining);
    }

    private void RainEvent()
    {
        isRaining = true;
        ColorRainWeather.onColorRaining -= RainEvent;
    }

    private void StopRainEvent()
    {
        isRaining = false;
        ColorRainWeather.onColorRaining += RainEvent;
    }
}
