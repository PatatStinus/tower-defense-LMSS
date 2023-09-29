using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAbility : EnemyMovement
{
    [SerializeField] private GameObject cursedObject;
    private GameObject theCurse;
    private bool isRaining;
    private bool doCurse;
    private int curse;


    protected override void Start()
    {
        base.Start();
        curse = Random.Range(1, 4);
        ColorRainWeather.onColorRaining += RainEvent;
        ColorRainWeather.onStopColorRaining += StopRainEvent;
        GetCurse();
    }

    protected override void Update()
    {
        base.Update();
        if (i_waypoitIndex > EnemyPathMaking.t_Points[pathIndex].Length - 2 && !doCurse)
        {
            doCurse = true;
            SpawnCurse();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ColorRainWeather.onColorRaining -= RainEvent;
        ColorRainWeather.onStopColorRaining -= StopRainEvent;
        if (isRaining && !reachedEnd) //DoGoodCurse if it was raining //Wouldn't it be better if it did the curse when it dies always?
            SpawnCurse();
    }

    private void SpawnCurse()
    {
        theCurse = Instantiate(cursedObject);
        theCurse.GetComponent<CurseEffect>().DoCurse(curse, isRaining);
    }

    private void GetCurse()
    {
        switch (curse)
        {
            case 1:
                //Cross
                break;
            case 2:
                //Circle
                break;
            case 3:
                //3 Lines
                break;
        }
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
